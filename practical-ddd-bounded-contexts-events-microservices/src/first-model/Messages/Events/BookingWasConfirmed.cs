using System;

namespace Messages.Events
{
    public class BookingWasConfirmed
    {
        public string BookingReferenceId { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}