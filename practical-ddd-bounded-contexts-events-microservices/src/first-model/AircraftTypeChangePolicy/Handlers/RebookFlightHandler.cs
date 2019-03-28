using System.Threading.Tasks;
using Messages;
using Messages.Commands;
using NServiceBus;

class RebookFlightHandler : IHandleMessages<RebookFlight>
{
    public async Task Handle(RebookFlight message, IMessageHandlerContext context)
    {
        await context.Publish(new BookedFlightWasChanged
        {
            BookingReferenceId = message.BookingReferenceId,
            ReasonForChange = message.ReasonForRebooking
        }).ConfigureAwait(false);
    }
}