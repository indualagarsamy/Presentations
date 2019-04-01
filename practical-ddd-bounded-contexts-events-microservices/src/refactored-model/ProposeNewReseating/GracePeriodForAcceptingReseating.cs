using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus;

public class GracePeriodForAcceptingReseating :
    Saga<GracePeriodForAcceptingReseatingData>,
    IAmStartedByMessages<ReseatingWasProposed>,
    IAmStartedByMessages<BookingWasCancelled>,
    IHandleTimeouts<GracePeriodForAcceptingReseating.CancellationGracePeriodElapsed>
{
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GracePeriodForAcceptingReseatingData> mapper)
    {
        mapper.ConfigureMapping<ReseatingWasProposed>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);

        mapper.ConfigureMapping<BookingWasCancelled>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);
    }

    public async Task Handle(ReseatingWasProposed message, IMessageHandlerContext context)
    {
        logger.Info("Received ReseatingWasProposed event");
        Data.IsReseatingProposed = true;
        Data.BookingReferenceId = message.BookingReferenceId;

        if (Data.CanCompleteSaga())
        {
            logger.Info("Saga is now complete");
            MarkAsComplete();
            return;
        }

        await context.SendLocal(new NotifyCustomerAboutProposedReseating(
            message.BookingReferenceId,
            message.ReasonForReseating)).ConfigureAwait(false);

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
        logger.Info("Received CancellationGracePeriodElapsed timeout message, publishing ReseatingWasAccepted event ");
        await context.Publish(new ReseatingWasAccepted(Data.BookingReferenceId))
            .ConfigureAwait(false);

        logger.Info("Saga is now complete");
        MarkAsComplete();

    }

    static ILog logger = LogManager.GetLogger<GracePeriodForAcceptingReseating>();

    public class CancellationGracePeriodElapsed
    {
    }
}