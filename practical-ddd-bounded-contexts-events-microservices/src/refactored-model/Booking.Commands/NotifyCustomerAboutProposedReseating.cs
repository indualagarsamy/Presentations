namespace Booking.Commands
{
    using NServiceBus;

    public class NotifyCustomerAboutProposedReseating :
        ICommand
    {
        public NotifyCustomerAboutProposedReseating(
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