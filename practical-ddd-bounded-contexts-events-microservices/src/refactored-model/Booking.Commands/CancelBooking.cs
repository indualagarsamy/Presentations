namespace Booking.Commands
{
    using NServiceBus;

    public class CancelBooking :
        ICommand
    {
        public CancelBooking(string bookingReferenceId, string customerId)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }
    }
}
