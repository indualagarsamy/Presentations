namespace AircraftTypeChangePolicy.Tests
{
    using System.Threading.Tasks;
    using Messages;
    using Messages.Commands;
    using Messages.Events;
    using NServiceBus.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class WhenBookedFlightWasChangedTests
    {
        BookingChangePolicy saga;
        TestableMessageHandlerContext context;
        BookedFlightWasChanged bookedFlightWasChanged;
        const string bookingReferenceId = "XYZ123";

        [SetUp]
        public void Setup()
        {
            saga = new BookingChangePolicy()
            {
                Data = new BookingChangePolicyData()
            };
            context = new TestableMessageHandlerContext();

            bookedFlightWasChanged = new BookedFlightWasChanged
            {
                BookingReferenceId = bookingReferenceId,
                FlightNumber = "UA890",
                ReasonForChange = "Aircraft type was changed from Boeing 787 to Boeing 777"
            };
        }

        [Test]
        public async Task ShouldNotifyCustomers()
        {
            await saga.Handle(bookedFlightWasChanged, context)
                .ConfigureAwait(false);
            Assert.AreEqual(2, context.SentMessages.Length);
            Assert.AreEqual(1, context.TimeoutMessages.Length);
            var processMessage = (NotifyCustomerAboutFlightChange)context.SentMessages[0].Message;
            Assert.AreEqual("Aircraft type was changed from Boeing 787 to Boeing 777", processMessage.ReasonForChange);
        }

        [Test]
        public async Task ShouldConfirmTheBooking()
        {
            await saga.Handle(bookedFlightWasChanged, context)
                .ConfigureAwait(false);
            await saga.Timeout(new CancellationGracePeriodElapsed(), context)
                .ConfigureAwait(false);

            Assert.IsTrue(saga.Completed);
            Assert.AreEqual(1, context.PublishedMessages.Length);
            var publishedMessage = (BookingWasConfirmed)context.PublishedMessages[0].Message;
            Assert.AreEqual(bookingReferenceId, publishedMessage.BookingReferenceId);
        }

        [Test]
        public async Task ShouldNotConfirmTheBooking()
        {
            await saga.Handle(bookedFlightWasChanged, context)
                .ConfigureAwait(false);

            var bookingWasCancelled = new BookingWasCancelled
            {
                BookingReferenceId = bookingReferenceId,
                CancellationReason = "Aircraft type was changed from Boeing 787 to Boeing 777"
            };

            await saga.Handle(bookingWasCancelled, context)
                .ConfigureAwait(false);
            
            Assert.IsTrue(saga.Completed);
            Assert.AreEqual(0, context.PublishedMessages.Length);
        }
    }
}