using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Logger.enums;
using Logger.handlers;
using Logger.model;

namespace Logger.service
{
    public class LoggerService
    {
        public static readonly LoggerService _instance = new();

        public readonly LogHandler handlerChain;

        private LoggerService()
        {
            handlerChain = LogHandlerConfiguration.Build();
        }

        public void Log(LogLevel level, string message)
        {
            LogMessage logMessage = new LogMessage(level, message, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            handlerChain.Handle(logMessage);
        }

        public void Info(String msg)
        {
            Log(LogLevel.INFO, msg);
        }

        public void Warn(String msg)
        {
            Log(LogLevel.WARN, msg);
        }

        public void Error(String msg)
        {
            Log(LogLevel.ERROR, msg);
        }
    }
}