using System;
using NServiceBus;
using NServiceBus.Features;

namespace Issue77.TargetEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Target";
            var busConfiguration = new BusConfiguration();
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.DisableFeature<SecondLevelRetries>();
            busConfiguration.Conventions().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Issue77") && t.Namespace.EndsWith("Commands"));
            busConfiguration.EndpointName("Issue77.Target");
            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
