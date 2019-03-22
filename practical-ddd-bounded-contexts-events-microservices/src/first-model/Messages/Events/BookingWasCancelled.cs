namespace Messages
{
    using NServiceBus;
    public class BookingWasCancelled : IEvent
    {
        public string BookingReferenceId { get; set; }
        public string CancellationReason { get; set; }
    }
}
