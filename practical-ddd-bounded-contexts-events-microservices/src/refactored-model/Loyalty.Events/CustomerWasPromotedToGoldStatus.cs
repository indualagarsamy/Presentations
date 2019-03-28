namespace Loyalty.Events
{
    using NServiceBus;
    using System;

    public class CustomerWasPromotedToGoldStatus :
        IEvent
    {
        public CustomerWasPromotedToGoldStatus(
            string customerId,
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
