namespace Booking.Events
{
    using NServiceBus;

    public class ReseatingWasProposed :
        IEvent
    {
        public ReseatingWasProposed(string bookingReferenceId, string reasonForReseating)
        {
            BookingReferenceId = bookingReferenceId;
            ReasonForReseating = reasonForReseating;
        }
        public string BookingReferenceId { get; }

        public string ReasonForReseating { get; }
    }
}