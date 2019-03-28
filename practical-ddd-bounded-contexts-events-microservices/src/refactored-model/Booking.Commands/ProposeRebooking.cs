namespace Booking.Commands
{
    using NServiceBus;

    public class ProposeRebooking :
        ICommand
    {
        public ProposeRebooking(string bookingReferenceId, string customerId, string reasonForRebooking)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
            ReasonForRebooking = reasonForRebooking;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }

        public string ReasonForRebooking { get; }
    }
}
