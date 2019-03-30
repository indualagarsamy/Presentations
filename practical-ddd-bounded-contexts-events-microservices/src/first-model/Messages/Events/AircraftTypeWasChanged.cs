namespace Messages
{
    using System;
    using NServiceBus;

    public class AircraftTypeWasChanged :
        IEvent
    {
        // Eg. Boeing 787 Dreamliner
        public string OldAircraftTypeId { get; set; }

        // Boeing 777-300ER
        public string NewAircraftTypeId { get; set; }

        public string FlightId { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}