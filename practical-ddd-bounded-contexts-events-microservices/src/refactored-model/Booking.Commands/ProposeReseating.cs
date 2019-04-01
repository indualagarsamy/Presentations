namespace Booking.Commands
{
    using NServiceBus;

    public class ProposeReseating :
        ICommand
    {
        public ProposeReseating(string bookingReferenceId, string customerId, string reasonForReseating, string newSeat)
        {
            BookingReferenceId = bookingReferenceId;
            CustomerId = customerId;
            ReasonForReseating = reasonForReseating;
            NewSeat = newSeat;
        }
        public string BookingReferenceId { get; }
        public string CustomerId { get; }
        public string NewSeat { get; }

        public string ReasonForReseating { get; }
    }
}
