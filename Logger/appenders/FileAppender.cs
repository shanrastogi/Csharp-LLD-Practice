using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.formatter;
using Logger.model;

namespace Logger.appenders
{
    public class FileAppender : ILogAppender, IDisposable
    {
        private readonly ILogFormatter _formatter;
        private readonly StreamWriter _writer;
        private readonly object _lock = new object();
        public FileAppender(ILogFormatter logFormatter, string fileName)
        {
            _formatter = logFormatter;

            try
            {
                _writer = new StreamWriter(fileName, append: true);
            }
            catch (IOException e)
            {
                throw new Exception("Failed to open file", e);
            }
        }

        public void Append(LogMessage message)
        {
            lock (_lock)
            {
                try
                {
                    _writer.WriteLine(_formatter.Format(message));
                    _writer.Flush();
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.ToString());
                }
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                try
                {
                    _writer.Close();
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.ToString());
                }
            }
        }

        public void Dispose()
        {
            Close();
        }
    }
}