using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTypeChangePolicy.Handlers
{
    using System.Threading.Tasks;
    using Messages;
    using Messages.Commands;
    using NServiceBus;
    class AircraftTypeWasChangedHandler : IHandleMessages<AircraftTypeWasChanged>
    {
        public async Task Handle(AircraftTypeWasChanged message, IMessageHandlerContext context)
        {
            // Find all the relevant booking references

            // Rebook the flight
            // foreach (affected flight in the list of bookings)
            //{ send RebookFlight command

                await context.Send(new RebookFlight
                {
                    BookingReferenceId = "XYZ123",
                    ReasonForRebooking = $"Aircraft type was changed from {message.OldAirCraftTypeId} to {message.NewAircraftTypeId}"
                }).ConfigureAwait(false);

            //}
           
        }
    }
}
