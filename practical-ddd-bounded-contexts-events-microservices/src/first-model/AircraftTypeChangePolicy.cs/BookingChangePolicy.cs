
namespace AircraftTypeChangePolicy
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using Messages.Commands;
    using Messages.Events;
    using NServiceBus;

    public class BookingChangePolicy : Saga<BookingChangePolicyData>, 
        IAmStartedByMessages<BookedFlightWasChanged>,
        IAmStartedByMessages<BookingWasCancelled>,
        IHandleTimeouts<CancellationGracePeriodElapsed>

    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BookingChangePolicyData> mapper)
        {
            mapper.ConfigureMapping<BookedFlightWasChanged>(message => message.BookingReferenceId)
                .ToSaga(data => data.BookingReferenceId);
            
            mapper.ConfigureMapping<BookingWasCancelled>(message => message.BookingReferenceId)
                .ToSaga(data => data.BookingReferenceId);

        }

        public async Task Handle(BookedFlightWasChanged message, IMessageHandlerContext context)
        {
            Data.IsFlightChanged = true;
            Data.BookingReferenceId = message.BookingReferenceId;

            if (Data.CanCompleteSaga())
            {
                MarkAsComplete();
                return;
            }

            await context.Send(new NotifyCustomerAboutFlightChange()
            {
                BookingReferenceId = message.BookingReferenceId,
                ReasonForChange = message.ReasonForChange
            }).ConfigureAwait(false);

            await RequestTimeout(context, TimeSpan.FromSeconds(15), new CancellationGracePeriodElapsed()).ConfigureAwait(false);
        }

        public Task Handle(BookingWasCancelled message, IMessageHandlerContext context)
        {
            Data.IsCancelled = true;

            if (Data.CanCompleteSaga())
            {
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }

        public async Task Timeout(CancellationGracePeriodElapsed state, IMessageHandlerContext context)
        {
            MarkAsComplete();
            await context.Publish(new BookingWasConfirmed()
            {
                BookingReferenceId = Data.BookingReferenceId,
                ConfirmationDate = DateTime.UtcNow
            }).ConfigureAwait(false);
        }
    }

    public class CancellationGracePeriodElapsed
    {
    }
}
