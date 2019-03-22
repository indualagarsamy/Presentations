namespace Messages.Commands
{
    using NServiceBus;
    public class NotifyCustomerAboutFlightChange : ICommand
    {
        public string BookingReferenceId { get; set; }
        public string ReasonForChange { get; set; }

    }
}
