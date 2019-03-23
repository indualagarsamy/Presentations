using System;

namespace BookingContext.Events
{
    public class BookingWasCancelled
    {
        public BookingWasCancelled(string bookingReferenceId)
        {
            BookingReferenceId = bookingReferenceId;
        }
        public string BookingReferenceId { get; }
    }
}
