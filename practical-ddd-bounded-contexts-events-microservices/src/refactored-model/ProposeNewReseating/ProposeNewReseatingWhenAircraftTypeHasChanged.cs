using NServiceBus.Logging;
using FlightPlanning.Events;
using Booking.Commands;
using NServiceBus;
using System.Threading.Tasks;
using Booking.Events;

public class ProposeNewReseatingWhenAircraftTypeHasChanged :
    IHandleMessages<AircraftTypeHasChanged>,
    IHandleMessages<ProposeReseating>
{
    public async Task Handle(AircraftTypeHasChanged message, IMessageHandlerContext context)
    {
        // Find all the relevant booking references

        // Propose to reseat customers for each of the bookings found.
        // foreach (affected flight in the list of bookings)
        //{

        // Find new seat for customer
        var newSeat = "13D";
        var cmd = new ProposeReseating(
            bookingReferenceId: "QAZ123",
            customerId: "1",
            reasonForReseating: $"Aircraft type was changed from {message.OldAircraftTypeId} to {message.NewAircraftTypeId} on flight {message.FlightId}",
            newSeat: newSeat);

        logger.Info("Received AircraftTypeHasChanged event, sending ProposeReseating command");
        await context.SendLocal(cmd).ConfigureAwait(false);
        //}
    }

    public Task Handle(ProposeReseating message, IMessageHandlerContext context)
    {
        // Find a new route and once you find a good itinerary, publish event

        logger.Info("Received ProposeReseating command, publishing ReseatingWasProposed event");
        return context.Publish(
            new ReseatingWasProposed(
                message.BookingReferenceId,
                message.ReasonForReseating));
    }

    static ILog logger = LogManager.GetLogger<ProposeNewReseatingWhenAircraftTypeHasChanged>();
}