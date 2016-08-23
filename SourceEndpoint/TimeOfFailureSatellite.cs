using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.MessageMutator;
using NServiceBus.Satellites;
using NServiceBus.Transports;
using NServiceBus.Unicast;

namespace Issue77.SourceEndpoint
{
    class TimeOfFailureSatellite : ISatellite
    {
        private readonly ISendMessages _sender;
        private readonly Random _random = new Random();

        public TimeOfFailureSatellite(ISendMessages sender)
        {
            _sender = sender;
        }

        public bool Handle(TransportMessage message)
        {
            var mode = _random.Next(0, 3);

            var timeOfFailure = DateTime.UtcNow;

            if (mode >= 3)
            {
                var weeks = (double)_random.Next(1, 3);
                timeOfFailure = timeOfFailure.AddDays(-(weeks * 7));
            }
            if (mode >= 2)
            {
                var days = (double)_random.Next(1, 6);
                timeOfFailure = timeOfFailure.AddDays(-days);
            }
            if (mode >= 1)
            {
                var hours = (double)_random.Next(1, 23);
                timeOfFailure = timeOfFailure.AddHours(-hours);
            }

            var minutes = (double)_random.Next(0, 59);
            timeOfFailure = timeOfFailure.AddMinutes(-minutes);

            message.Headers["NServiceBus.TimeOfFailure"] = DateTimeExtensions.ToWireFormattedString(timeOfFailure);

            _sender.Send(message, new SendOptions(new Address("error",Environment.MachineName)));

            return true;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public Address InputAddress => Address.Parse("toferrors");

        public bool Disabled => false;
    }
}
