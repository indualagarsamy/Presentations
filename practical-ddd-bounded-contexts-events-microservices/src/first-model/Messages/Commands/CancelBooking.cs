namespace Messages.Commands
{
    using NServiceBus;
    public class CancelBooking : ICommand
    {
        public string BookingReferenceId { get; set; }
        public string CustomerId { get; set; }
    }
}
