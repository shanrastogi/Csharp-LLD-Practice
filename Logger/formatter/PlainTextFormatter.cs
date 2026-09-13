using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.model;

namespace Logger.formatter
{
    public class PlainTextFormatter : ILogFormatter
    {
        private static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public string Format(LogMessage message)
        {
            string formattedTime = DateTimeOffset.FromUnixTimeMilliseconds(message.Timestamp).ToLocalTime().ToString(DateTimeFormat);

            return $"{formattedTime} [{message.Level}] - {message.Message}";
        }
    }
}