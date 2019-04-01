using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus.Testing;
using NUnit.Framework;

[TestFixture]
public class WhenReseatingWasProposedTests
{
    GracePeriodForAcceptingReseating saga;
    TestableMessageHandlerContext context;
    ReseatingWasProposed _reseatingWasProposed;
    const string bookingReferenceId = "XYZ123";

    [SetUp]
    public void Setup()
    {
        saga = new GracePeriodForAcceptingReseating
        {
            Data = new GracePeriodForAcceptingReseatingData()
        };
        context = new TestableMessageHandlerContext();

        _reseatingWasProposed = new ReseatingWasProposed(
            bookingReferenceId,
            "Aircraft type was changed from Boeing 787 to Boeing 777");
    }

    [Test]
    public async Task ShouldNotifyCustomers()
    {
        await saga.Handle(_reseatingWasProposed, context);
        Assert.AreEqual(2, context.SentMessages.Length);
        Assert.AreEqual(1, context.TimeoutMessages.Length);
        var processMessage = (NotifyCustomerAboutProposedReseating) context.SentMessages[0].Message;
        Assert.AreEqual("Aircraft type was changed from Boeing 787 to Boeing 777", processMessage.ReasonForChange);
    }

    [Test]
    public async Task ShouldAcceptTheReseating()
    {
        await saga.Handle(_reseatingWasProposed, context);
        await saga.Timeout(new GracePeriodForAcceptingReseating.CancellationGracePeriodElapsed(), context);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(1, context.PublishedMessages.Length);
        var publishedMessage = (ReseatingWasAccepted) context.PublishedMessages[0].Message;
        Assert.AreEqual(bookingReferenceId, publishedMessage.BookingReferenceId);
    }

    [Test]
    public async Task ShouldNotAcceptTheReseating()
    {
        await saga.Handle(_reseatingWasProposed, context);

        var bookingWasCancelled = new BookingWasCancelled(bookingReferenceId);

        await saga.Handle(bookingWasCancelled, context);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(0, context.PublishedMessages.Length);
    }
}