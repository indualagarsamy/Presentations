using System;

namespace Loyalty.Events
{
    public class CustomerWasPromotedToGoldStatus
    {
        public CustomerWasPromotedToGoldStatus(string customerId, 
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
