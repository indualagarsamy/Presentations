using System;

namespace AircraftTypeChangePolicy.Tests
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using Messages.Commands;
    using NServiceBus.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class WhenBookedFlightWasChangedTests
    {
        [Test]
        public async Task ShouldNotifyCustomers()
        {
            var saga = new BookingChangePolicy()
            {
                Data = new BookingChangePolicyData()
            };
            var context = new TestableMessageHandlerContext();

            var bookingFlightWasChanged = new BookedFlightWasChanged
            {
                BookingReferenceId = "GYT567",
                FlightNumber = "UA890",
                ReasonForChange = "Aircraft type was changed from Boeing 787 to Boeing 777"
            };

            await saga.Handle(bookingFlightWasChanged, context)
                .ConfigureAwait(false);
            Assert.AreEqual(2, context.SentMessages.Length);
            Assert.AreEqual(1, context.TimeoutMessages.Length);
            var processMessage = (NotifyCustomerAboutFlightChange)context.SentMessages[0].Message;
            Assert.AreEqual("Aircraft type was changed from Boeing 787 to Boeing 777", processMessage.ReasonForChange);
        }

         

    }
}
