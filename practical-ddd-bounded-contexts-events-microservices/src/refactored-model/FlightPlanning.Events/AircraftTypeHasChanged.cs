
namespace FlightPlanning.Events
{
    using System;
    using NServiceBus;

    public class AircraftTypeHasChanged : IEvent
    {
        public AircraftTypeHasChanged(string oldAirCraftTypeId, 
            string newAircraftTypeId, 
            string flightId,
            DateTime effectiveFrom)
        {
            OldAirCraftTypeId = oldAirCraftTypeId;
            NewAircraftTypeId = newAircraftTypeId;
            FlightId = flightId;
            EffectiveFrom = effectiveFrom;
        }

        public string OldAirCraftTypeId { get;} // Eg. B38M (Boeing 737 Max 8) B39M
        public string NewAircraftTypeId { get;} // Eg. B739 (Boeing 737-900)

        public string FlightId { get; }
        public DateTime EffectiveFrom { get;}
    }
}
