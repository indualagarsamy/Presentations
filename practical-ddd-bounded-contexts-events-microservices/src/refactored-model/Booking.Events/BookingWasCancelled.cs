namespace Booking.Events
{
    using NServiceBus;

    public class BookingWasCancelled :
        IEvent
    {
        public BookingWasCancelled(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
