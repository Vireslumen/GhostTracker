using System;
using System.Diagnostics;
using System.IO;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Http.TextFormatters;

namespace GhostTracker.Services
{
    public static class LoggerConfigurationManager
    {
        private static ILogger _logger;
        private static string logFilePath;

        public static void EnableServerLogging(bool enable)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            logFilePath = Path.Combine("/storage/emulated/0/Download/", "logs", "log-.txt");
            var config = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day);

            if (enable) config.WriteTo.Http("https://a28577-767d.u.d-f.pw/Log", 500000);

            _logger = config.CreateLogger();
            Log.Logger = _logger;
            Log.Information("Серверное логирование сейчас " + (enable ? "включено" : "выключено") +
                            " пользователем.");
        }

        public static void InitializeLogger()
        {
            EnableServerLogging(LoggerHelper.GetServerLogActive());
        }
    }
}