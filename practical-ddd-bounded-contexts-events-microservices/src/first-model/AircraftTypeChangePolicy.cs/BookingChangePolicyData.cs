namespace AircraftTypeChangePolicy
{
    using NServiceBus;
    public class BookingChangePolicyData : ContainSagaData
    {
        public string BookingReferenceId { get; set; }
        
        public bool IsCancelled { get; set; }

        public bool IsFlightChanged { get; set; }


        public bool CanCompleteSaga()
        {
            return IsCancelled && IsFlightChanged;
        }
    }
}