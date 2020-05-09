using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace BookingService.TgBot
{
    public static class Logger
    {
        private static ILogger _logger;

        public static ILogger Get()
        {
            if (_logger == null)
                _logger = Initialize();
            return _logger;
        } 

        private static Serilog.Core.Logger Initialize()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:w3}] {Message}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code
                )
                .CreateLogger();
        }
    }
}
