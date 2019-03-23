namespace Booking.Events
{
    public class BookingWasCancelled
    {
        public BookingWasCancelled(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
