using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.enums;

namespace Logger.handlers
{
    public class WarnHandler : LogHandler
    {
        protected override bool CanHandle(LogLevel level)
        {
            return level == LogLevel.WARN;
        }
    }
}