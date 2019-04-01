using NServiceBus;

public class GracePeriodForAcceptingReseatingData :
    ContainSagaData
{
    public string BookingReferenceId { get; set; }

    public bool IsBookingCancelled { get; set; }

    public bool IsReseatingProposed { get; set; }

    public bool CanCompleteSaga()
    {
        return IsBookingCancelled && IsReseatingProposed;
    }
}