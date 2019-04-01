namespace Booking.Commands
{
    using NServiceBus;

    public class AcceptReseating :
        ICommand
    {
        public AcceptReseating(string bookingReferenceId, string customerId)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }
    }
}
