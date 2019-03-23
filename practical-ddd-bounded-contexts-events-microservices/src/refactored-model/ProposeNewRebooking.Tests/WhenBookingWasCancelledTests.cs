namespace ProposeNewRebooking.Tests
{
    using System.Threading.Tasks;
    using Booking.Events;
    using NServiceBus.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class WhenBookingWasCancelledTests
    {
        GracePeriodForAcceptingRebookings saga;
        TestableMessageHandlerContext context;
        BookingWasCancelled bookingWasCancelled;

        const string bookingReferenceId = "XYZ123";

        [SetUp]
        public void Setup()
        {
            saga = new GracePeriodForAcceptingRebookings()
            {
                Data = new GracePeriodForAcceptingRebookingsData()
            };
            context = new TestableMessageHandlerContext();
            bookingWasCancelled = new BookingWasCancelled(bookingReferenceId);
        }

        [Test]
        public async Task ShouldWaitUntilRebookingWasProposed()
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

            var rebookingWasProposed = new RebookingWasProposed(
                bookingReferenceId,
                "Aircraft type was changed from Boeing 787 to Boeing 777");

            await saga.Handle(rebookingWasProposed, context)
                .ConfigureAwait(false);

            Assert.IsTrue(saga.Completed);
            Assert.AreEqual(0, context.SentMessages.Length);
            Assert.AreEqual(0, context.PublishedMessages.Length);
        }


    }
}
