using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.formatter;
using Logger.model;

namespace Logger.appenders
{
    public class ConsoleAppender : ILogAppender
    {

        private readonly ILogFormatter _formatter;

        public ConsoleAppender(ILogFormatter logFormatter)
        {
            _formatter = logFormatter;
        }

        public void Append(LogMessage message)
        {
            Console.WriteLine(_formatter.Format(message));
        }
    }
}