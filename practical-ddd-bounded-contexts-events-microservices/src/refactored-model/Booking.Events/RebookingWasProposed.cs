namespace Booking.Events
{
    using NServiceBus;

    public class RebookingWasProposed :
        IEvent
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