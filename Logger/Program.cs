using Logger.appenders;
using Logger.enums;
using Logger.formatter;
using Logger.service;
public class Program
{
    public static void Main(string[] args)
    {
        LoggerService _logger = LoggerService._instance;

        LogHandlerConfiguration.AddAppenderForLevel(LogLevel.INFO, new ConsoleAppender(new PlainTextFormatter()));

        LogHandlerConfiguration.AddAppenderForLevel(LogLevel.WARN, new ConsoleAppender(new PlainTextFormatter()));

        LogHandlerConfiguration.AddAppenderForLevel(LogLevel.ERROR, new ConsoleAppender(new PlainTextFormatter()));

        LogHandlerConfiguration.AddAppenderForLevel(LogLevel.ERROR, new FileAppender(new PlainTextFormatter(), "./Logger/logs.txt"));

        _logger.Info("This is some info.");
        _logger.Error("This is some error.");
    }
}