
namespace FlightPlanning.Events
{
    using System;
    using NServiceBus;

    public class AircraftTypeHasChanged : IEvent
    {
        public AircraftTypeHasChanged(
            string oldAircraftTypeId,
            string newAircraftTypeId,
            string flightId,
            DateTime effectiveFrom)
        {
            OldAircraftTypeId = oldAircraftTypeId;
            NewAircraftTypeId = newAircraftTypeId;
            FlightId = flightId;
            EffectiveFrom = effectiveFrom;
        }

        // Eg. Boeing 787 Dreamliner
        public string OldAircraftTypeId { get;}

        // Boeing 777-300ER
        public string NewAircraftTypeId { get;}

        public string FlightId { get; }
        public DateTime EffectiveFrom { get;}
    }
}
