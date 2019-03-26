namespace Booking.ConsoleUI
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using FlightPlanning.Events;
    using NServiceBus;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Booking.ConsoleUI";
            var endpointConfiguration = new EndpointConfiguration("Booking.ConsoleUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

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
                        var changeAircraftType = new AircraftTypeHasChanged("", "", "", DateTime.Today);
                        await endpointInstance.Publish(changeAircraftType).ConfigureAwait(false);
                        break;
                    case ConsoleKey.C:
                        var cancelRebooking = new CancelBooking("QAZ123", "1");
                        await endpointInstance.Send(cancelRebooking).ConfigureAwait(false);
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
