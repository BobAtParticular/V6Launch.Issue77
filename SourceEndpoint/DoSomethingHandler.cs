using System;
using Issue77.Commands;
using NServiceBus;

namespace Issue77.SourceEndpoint
{
    public class DoSomethingHandler : IHandleMessages<DoSomething>
    {
        private readonly IBus _bus;

        public DoSomethingHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(DoSomething message)
        {
            if (_bus.CurrentMessageContext.Headers.ContainsKey("ServiceControl.Retry.UniqueMessageId"))
            {
                Console.WriteLine("DoSomething has been returned from ServiceControl to SourceEndpoint");
                return;
            }

            throw new Exception("Simulated exception");
        }
    }
}
