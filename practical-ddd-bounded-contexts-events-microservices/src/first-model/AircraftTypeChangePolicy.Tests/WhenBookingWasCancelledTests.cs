using System.Threading.Tasks;
using Messages;
using NServiceBus.Testing;
using NUnit.Framework;

[TestFixture]
public class WhenBookingWasCancelledTests
{
    BookingChangePolicy saga;
    TestableMessageHandlerContext context;
    BookingWasCancelled bookingWasCancelled;

    const string bookingReferenceId = "XYZ123";

    [SetUp]
    public void Setup()
    {
        saga = new BookingChangePolicy()
        {
            Data = new BookingChangePolicyData()
        };
        context = new TestableMessageHandlerContext();
        bookingWasCancelled = new BookingWasCancelled
        {
            BookingReferenceId = bookingReferenceId,
            CancellationReason = "Aircraft type was changed from Boeing 787 to Boeing 777"
        };
    }

    [Test]
    public async Task ShouldWaitUntilBookedFlightWasChangedEventToArrive()
    {
        await saga.Handle(bookingWasCancelled, context)
            .ConfigureAwait(false);
        Assert.IsFalse(saga.Completed);
    }

    [Test]
    public async Task ShouldNotNotifyCustomers()
    {
        await saga.Handle(bookingWasCancelled, context)
            .ConfigureAwait(false);

        var bookingFlightWasChanged = new BookedFlightWasChanged
        {
            BookingReferenceId = bookingReferenceId,
            FlightNumber = "UA890",
            ReasonForChange = "Aircraft type was changed from Boeing 787 to Boeing 777"
        };

        await saga.Handle(bookingFlightWasChanged, context)
            .ConfigureAwait(false);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(0, context.SentMessages.Length);
        Assert.AreEqual(0, context.PublishedMessages.Length);
    }
}