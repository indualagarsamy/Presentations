namespace Booking.Events
{
    using NServiceBus;

    public class ReseatingWasAccepted :
        IEvent
    {
        public ReseatingWasAccepted(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
