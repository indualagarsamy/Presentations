using System;

namespace BookingContext.Events
{
    public class RebookingWasAccepted
    {
        public RebookingWasAccepted(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
