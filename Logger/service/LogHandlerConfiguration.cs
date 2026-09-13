using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.appenders;
using Logger.enums;
using Logger.handlers;

namespace Logger.service
{
    public class LogHandlerConfiguration
    {
        private static readonly LogHandler info = new InfoHandler();
        private static readonly LogHandler warn = new WarnHandler();
        private static readonly LogHandler error = new ErrorHandler();

        public static LogHandler Build()
        {
            info.Next = warn;
            warn.Next = error;
            return info;
        }

        public static void AddAppenderForLevel(LogLevel level, ILogAppender appender)
        {
            switch (level)
            {

                case LogLevel.INFO:
                    info.Subscribe(appender);
                    break;
                case LogLevel.WARN:
                    warn.Subscribe(appender);
                    break;
                case LogLevel.ERROR:
                    error.Subscribe(appender);
                    break;
            }
        }
    }
}