using System;
using Issue77.Commands;
using NServiceBus;

namespace Issue77.TargetEndpoint
{
    public class DoSomethingHandler : IHandleMessages<DoSomething>
    {
        public void Handle(DoSomething message)
        {
            Console.WriteLine("DoSomething handled");
        }
    }
}
