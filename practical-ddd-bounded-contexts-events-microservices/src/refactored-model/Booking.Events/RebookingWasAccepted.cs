namespace Booking.Events
{
    using NServiceBus;

    public class RebookingWasAccepted :
        IEvent
    {
        public RebookingWasAccepted(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
