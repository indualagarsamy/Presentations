using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public class BookingWasConfirmed
    {
        public string BookingReferenceId { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}
