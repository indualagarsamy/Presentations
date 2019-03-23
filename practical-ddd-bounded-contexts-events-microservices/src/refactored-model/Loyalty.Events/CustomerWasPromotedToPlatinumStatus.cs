using System;

namespace Loyalty.Events
{
    public class CustomerWasPromotedToPlatinumStatus
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
