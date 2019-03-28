namespace Loyalty.Events
{
    using System;
    using NServiceBus;

    public class CustomerWasPromotedToPlatinumStatus :
        IEvent
    {
        public CustomerWasPromotedToPlatinumStatus(string customerId,
            DateTime effectiveFrom,
            DateTime effectiveUntil)
        {
            CustomerId = customerId;
            EffectiveFrom = effectiveFrom;
            EffectiveUntil = effectiveUntil;
        }
        public string CustomerId { get; }
        public DateTime EffectiveFrom { get; }
        public DateTime EffectiveUntil { get; }
    }
}