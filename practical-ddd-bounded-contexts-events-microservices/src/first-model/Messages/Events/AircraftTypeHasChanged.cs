
namespace Messages
{
    using System;

    using NServiceBus;
    public class AircraftTypeHasChanged : IEvent
    {
        public string OldAirCraftTypeId { get; set; } // Eg. B38M (Boeing 737 Max 8) B39M
        public string NewAircraftTypeId { get; set; } // Eg. B739 (Boeing 737-900)

        public string FlightId { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}
