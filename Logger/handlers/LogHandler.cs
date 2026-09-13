using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Logger.appenders;
using Logger.enums;
using Logger.model;

namespace Logger.handlers
{
    public abstract class LogHandler
    {
        public LogHandler? Next { get; set; }

        private readonly List<ILogAppender> _appenders = new();
        private readonly object _lock = new();

        public void Subscribe(ILogAppender observer)
        {
            lock (_lock)
            {
                _appenders.Add(observer);
            }
        }

        public void Unsubscribe(ILogAppender observer)
        {
            lock (_lock)
            {
                _appenders.Remove(observer);
            }
        }

        public void NotifyObservers(LogMessage message)
        {
            List<ILogAppender> snapshot;

            // Take a snapshot of the list to ensure thread-safe iteration
            // without keeping the lock open during the actual append operations.
            lock (_lock)
            {
                snapshot = new List<ILogAppender>(_appenders);
            }

            foreach (var appender in snapshot)
            {
                appender.Append(message);
            }
        }

        public void Handle(LogMessage message)
        {
            if (CanHandle(message.Level))
            {
                NotifyObservers(message);
            }
            else if (Next != null)
            {
                Next.Handle(message);
            }
        }

        protected abstract bool CanHandle(LogLevel level);
    }
}