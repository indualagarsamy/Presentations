namespace Messages
{
    using System.Net.NetworkInformation;
    using NServiceBus;
    using NServiceBus.Pipeline;

    public class BookedFlightWasChanged : IEvent
    {
        public string BookingReferenceId { get; set; }
        public string FlightNumber { get; set; }
        public Itinerary Itinerary { get; set; }

        public string ReasonForChange { get; set; }
    }

    public class Itinerary
    {
        public Leg[] Legs { get; set; }
    }

    public class Leg
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

}
