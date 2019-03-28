
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

        // Eg. B38M (Boeing 737 Max 8) B39M
        public string OldAircraftTypeId { get;}

        // Eg. B739 (Boeing 737-900)
        public string NewAircraftTypeId { get;}

        public string FlightId { get; }
        public DateTime EffectiveFrom { get;}
    }
}
