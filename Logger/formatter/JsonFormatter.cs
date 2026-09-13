using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Logger.model;

namespace Logger.formatter
{
    public class JsonFormatter : ILogFormatter
    {
        private static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public string Format(LogMessage message)
        {
            string formattedTime = DateTimeOffset.FromUnixTimeMilliseconds(message.Timestamp).ToLocalTime().ToString(DateTimeFormat);

            var logEntry = new
            {
                timestamp = formattedTime,
                level = message.Level.ToString(),
                message = message.Message
            };

            return JsonSerializer.Serialize(logEntry);
        }
    }
}