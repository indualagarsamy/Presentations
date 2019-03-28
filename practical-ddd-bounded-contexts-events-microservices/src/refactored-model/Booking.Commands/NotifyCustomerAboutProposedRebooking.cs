namespace Booking.Commands
{
    using NServiceBus;

    public class NotifyCustomerAboutProposedRebooking :
        ICommand
    {
        public NotifyCustomerAboutProposedRebooking(
            string bookingReferenceId,
            string reasonForChange)
        {
            BookingReferenceId = bookingReferenceId;
            ReasonForChange = reasonForChange;
        }

        public string BookingReferenceId { get; }
        public string ReasonForChange { get; }
    }
}