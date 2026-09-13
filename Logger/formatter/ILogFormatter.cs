using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.model;

namespace Logger.formatter
{
    public interface ILogFormatter
    {
        public string Format(LogMessage message);
    }
}