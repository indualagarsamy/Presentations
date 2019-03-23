namespace Booking.Events
{
    public class RebookingWasAccepted
    {
        public RebookingWasAccepted(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
