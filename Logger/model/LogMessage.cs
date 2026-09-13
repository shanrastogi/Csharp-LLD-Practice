using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.enums;

namespace Logger.model
{
    public class LogMessage
    {
        public LogLevel Level { get; }
        public string Message { get; }
        public long Timestamp { get; }

        public LogMessage(LogLevel _level, string _message, long _timestamp)
        {
            Level = _level;
            Message = _message;
            Timestamp = _timestamp;
        }
    }
}