namespace Booking.Commands
{
    using NServiceBus;

    public class AcceptRebooking :
        ICommand
    {
        public AcceptRebooking(string bookingReferenceId, string customerId)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }
    }
}
