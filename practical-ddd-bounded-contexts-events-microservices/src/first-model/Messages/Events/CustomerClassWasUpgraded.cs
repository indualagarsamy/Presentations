namespace Messages
{
    using NServiceBus;

    public class CustomerClassWasUpgraded :
        IEvent
    {
        public string CustomerId { get; set; }
        public string BookingReferenceId { get; set; }

        public string Class { get; set; }
    }
}