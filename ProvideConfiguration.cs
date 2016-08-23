using System;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Issue77
{
    class ProvideConfiguration :
        IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>,
        IProvideConfiguration<AuditConfig>
    {
        AuditConfig IProvideConfiguration<AuditConfig>.GetConfiguration()
        {
            return new AuditConfig
            {
                QueueName = "audit"
            };
        }

        internal static string ErrorQueue = "error";

        MessageForwardingInCaseOfFaultConfig IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>.GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = ErrorQueue
            };
        }
    }
}