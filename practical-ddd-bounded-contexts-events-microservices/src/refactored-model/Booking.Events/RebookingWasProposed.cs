namespace Booking.Events
{
    public class RebookingWasProposed
    {
        public RebookingWasProposed(string bookingReferenceId, string reasonForRebooking)
        {
            BookingReferenceId = bookingReferenceId;
            ReasonForRebooking = reasonForRebooking;
        }
        public string BookingReferenceId { get; }
        
        public string ReasonForRebooking { get; }
    }
}
