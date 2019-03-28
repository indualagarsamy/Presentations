using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus;

public class GracePeriodForAcceptingRebookings :
    Saga<GracePeriodForAcceptingRebookingsData>,
    IAmStartedByMessages<RebookingWasProposed>,
    IAmStartedByMessages<BookingWasCancelled>,
    IHandleTimeouts<GracePeriodForAcceptingRebookings.CancellationGracePeriodElapsed>
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
        logger.Info("Received RebookingWasProposed event");
        Data.IsRebookingProposed = true;
        Data.BookingReferenceId = message.BookingReferenceId;

        if (Data.CanCompleteSaga())
        {
            logger.Info("Saga is now complete");
            MarkAsComplete();
            return;
        }

        await context.SendLocal(new NotifyCustomerAboutProposedRebooking(
            message.BookingReferenceId,
            message.ReasonForRebooking)).ConfigureAwait(false);

        await RequestTimeout(context, TimeSpan.FromSeconds(15),
            new CancellationGracePeriodElapsed()).ConfigureAwait(false);
    }

    public Task Handle(BookingWasCancelled message, IMessageHandlerContext context)
    {
        logger.Info("Received BookingWasCancelled event");
        Data.IsBookingCancelled = true;

        if (Data.CanCompleteSaga())
        {
            logger.Info("Saga is now complete");
            MarkAsComplete();
        }

        return Task.CompletedTask;
    }

    public async Task Timeout(CancellationGracePeriodElapsed state, IMessageHandlerContext context)
    {
        logger.Info("Received CancellationGracePeriodElapsed timeout message, publishing RebookingWasAccepted event ");
        await context.Publish(new RebookingWasAccepted(Data.BookingReferenceId))
            .ConfigureAwait(false);

        logger.Info("Saga is now complete");
        MarkAsComplete();

    }

    static ILog logger = LogManager.GetLogger<GracePeriodForAcceptingRebookings>();

    public class CancellationGracePeriodElapsed
    {
    }
}