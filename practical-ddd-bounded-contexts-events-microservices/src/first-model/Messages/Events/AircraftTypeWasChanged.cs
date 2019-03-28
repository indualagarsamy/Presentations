namespace Messages
{
    using System;
    using NServiceBus;

    public class AircraftTypeWasChanged :
        IEvent
    {
        // Eg. B38M (Boeing 737 Max 8) B39M
        public string OldAircraftTypeId { get; set; }

        // Eg. B739 (Boeing 737-900)
        public string NewAircraftTypeId { get; set; }

        public string FlightId { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}