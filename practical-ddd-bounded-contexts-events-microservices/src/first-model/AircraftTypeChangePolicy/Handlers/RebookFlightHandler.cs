using System.Threading.Tasks;
using Messages;
using Messages.Commands;
using NServiceBus;

class RebookFlightHandler :
    IHandleMessages<RebookFlight>
{
    public Task Handle(RebookFlight message, IMessageHandlerContext context)
    {
        return context.Publish(new BookedFlightWasChanged
        {
            BookingReferenceId = message.BookingReferenceId,
            ReasonForChange = message.ReasonForRebooking
        });
    }
}