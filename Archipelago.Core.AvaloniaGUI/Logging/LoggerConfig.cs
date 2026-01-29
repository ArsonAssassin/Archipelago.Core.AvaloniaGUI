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

        public static void Initialize(Action<string, LogEventLevel> mainFormWriter, Action<APMessageModel, LogEventLevel> archipelagoEventLogHandler)
        {
            _outputAction = mainFormWriter;
            _archipelagoEventLogHandler = archipelagoEventLogHandler;
            CreateNewLogger();
        }

        public static LogEventLevel GetMinimumLevel()
        {
            return _minimumLevel;
        }

        public static void SetLogLevel(LogEventLevel level)
        {
            _minimumLevel = level;
            CreateNewLogger();
        }

        private static void CreateNewLogger()
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(_minimumLevel)
                .WriteTo.ArchipelagoGuiSink(_outputAction, _archipelagoEventLogHandler);

            (_logger as IDisposable)?.Dispose();

            _logger = loggerConfiguration.CreateLogger();
            Log.Logger = _logger;
        }

        public static LoggerConfiguration GetLoggerConfiguration(Action<string, LogEventLevel> mainFormWriter, Action<APMessageModel, LogEventLevel> archipelagoEventLogHandler)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(_minimumLevel)
                .WriteTo.ArchipelagoGuiSink(mainFormWriter, archipelagoEventLogHandler);
        }
    }
}
