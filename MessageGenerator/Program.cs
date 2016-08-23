using System;
using Issue77.Commands;
using NServiceBus;

namespace Issue77.MessageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Message Generator";
            var busConfiguration = new BusConfiguration();
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.Conventions().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Issue77") && t.Namespace.EndsWith("Commands"));
            using (var bus = Bus.CreateSendOnly(busConfiguration))
            {
                Console.WriteLine("Press any key to send messages");
                Console.ReadKey();

                for (var i = 0; i < 10; i++)
                {
                    bus.Send("Issue77.Source", new DoSomething());
                }

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
