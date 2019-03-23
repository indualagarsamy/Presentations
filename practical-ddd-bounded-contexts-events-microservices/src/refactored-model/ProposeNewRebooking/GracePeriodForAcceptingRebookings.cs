namespace ProposeNewRebooking
{
    using System;
    using System.Threading.Tasks;
    using Booking.Commands;
    using Booking.Events;
    using NServiceBus;
    public class GracePeriodForAcceptingRebookings : Saga<GracePeriodForAcceptingRebookingsData>,
        IAmStartedByMessages<RebookingWasProposed>,
        IAmStartedByMessages<BookingWasCancelled>,
        IHandleTimeouts<CancellationGracePeriodElapsed>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GracePeriodForAcceptingRebookingsData> mapper)
        {
            mapper.ConfigureMapping<RebookingWasProposed>(message => message.BookingReferenceId)
                .ToSaga(data => data.BookingReferenceId);

            mapper.ConfigureMapping<BookingWasCancelled>(message => message.BookingReferenceId)
                .ToSaga(data => data.BookingReferenceId);
        }

        public async Task Handle(RebookingWasProposed message, IMessageHandlerContext context)
        {
            Data.IsRebookingProposed = true;
            Data.BookingReferenceId = message.BookingReferenceId;

            if (Data.CanCompleteSaga())
            {
                MarkAsComplete();
                return;
            }

            await context.Send(new NotifyCustomerAboutProposedRebooking(
                message.BookingReferenceId,
                message.ReasonForRebooking)).ConfigureAwait(false);

            await RequestTimeout(context, TimeSpan.FromSeconds(15), 
                new CancellationGracePeriodElapsed()).ConfigureAwait(false);
        }

        public Task Handle(BookingWasCancelled message, IMessageHandlerContext context)
        {
            Data.IsBookingCancelled = true;

            if (Data.CanCompleteSaga())
            {
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }

        public async Task Timeout(CancellationGracePeriodElapsed state, IMessageHandlerContext context)
        {
            MarkAsComplete();
            await context.Publish(new RebookingWasAccepted(Data.BookingReferenceId))
                .ConfigureAwait(false);
        }
    }

    public class CancellationGracePeriodElapsed
    {
    }
}
