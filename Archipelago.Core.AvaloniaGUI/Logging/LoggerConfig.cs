using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Logging
{
    public static class LoggerConfig
    {
        private static ILogger _logger;
        private static Action<string, LogEventLevel> _outputAction;
        private static Action<APMessageModel, LogEventLevel> _archipelagoEventLogHandler;
        private static LogEventLevel _minimumLevel = LogEventLevel.Information;
        private static LoggerConfiguration _loggerConfiguration;
        public static void Initialize(Action<string, LogEventLevel> mainFormWriter,Action<APMessageModel, LogEventLevel> archipelagoEventLogHandler)
        {
            _outputAction = mainFormWriter;
            _archipelagoEventLogHandler = archipelagoEventLogHandler;
            _loggerConfiguration = new LoggerConfiguration()
                .WriteTo.ArchipelagoGuiSink(_outputAction, archipelagoEventLogHandler);
            SetLoggerConfiguration(_loggerConfiguration);
        }
        public static LogEventLevel GetMinimumLevel()
        {
            return _minimumLevel;
        }
        public static void SetLogLevel(LogEventLevel level)
        {
            _minimumLevel = level;
            _loggerConfiguration.MinimumLevel.Is(level);
            SetLoggerConfiguration(_loggerConfiguration);
        }
        public static void SetLoggerConfiguration(LoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
            _logger = _loggerConfiguration.CreateLogger();
            Log.Logger = _logger;
        }
        public static LoggerConfiguration GetLoggerConfiguration(Action<string, LogEventLevel> mainFormWriter, Action<APMessageModel, LogEventLevel> archipelagoEventLogHandler)
        {
            return _loggerConfiguration;
        }
    }
}
