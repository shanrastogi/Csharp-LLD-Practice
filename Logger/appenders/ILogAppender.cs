using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.model;

namespace Logger.appenders
{
    public interface ILogAppender
    {
        public void Append(LogMessage message);
    }
}