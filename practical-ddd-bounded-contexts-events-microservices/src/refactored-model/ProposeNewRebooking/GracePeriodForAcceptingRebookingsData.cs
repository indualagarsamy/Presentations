using NServiceBus;

public class GracePeriodForAcceptingRebookingsData :
    ContainSagaData
{
    public string BookingReferenceId { get; set; }

    public bool IsBookingCancelled { get; set; }

    public bool IsRebookingProposed { get; set; }

    public bool CanCompleteSaga()
    {
        return IsBookingCancelled && IsRebookingProposed;
    }
}