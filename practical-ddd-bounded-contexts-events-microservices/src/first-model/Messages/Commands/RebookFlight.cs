namespace Messages.Commands
{
    using NServiceBus;
    public class RebookFlight : ICommand
    {
        public string BookingReferenceId { get; set; }
        public string ReasonForRebooking { get; set; }
    }
}
