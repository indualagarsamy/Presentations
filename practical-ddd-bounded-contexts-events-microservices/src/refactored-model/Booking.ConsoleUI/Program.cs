namespace Booking.ConsoleUI
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using FlightPlanning.Events;
    using NServiceBus;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Booking.ConsoleUI";
            var endpointConfiguration = new EndpointConfiguration("Booking.ConsoleUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await RunLoop(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                Console.WriteLine("Press A to notify subscribers that an aircraft has changed");
                Console.WriteLine("Press C to cancel a rebooking request");
                Console.WriteLine("Press Q to quit");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.A:
                        var aircraftTypeHasChanged = new AircraftTypeHasChanged("B38M", "B739", "AA4079", DateTime.Today);
                        await endpointInstance.Publish(aircraftTypeHasChanged).ConfigureAwait(false);
                        break;
                    case ConsoleKey.C:
                        var bookingWasCancelled = new BookingWasCancelled("QAZ123");
                        await endpointInstance.Publish(bookingWasCancelled).ConfigureAwait(false);
                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}
