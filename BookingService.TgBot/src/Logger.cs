using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace BookingService.TgBot
{
    public static class Logger
    {
        private static ILogger _logger;

        public static ILogger Get()
        {
            return _logger ??= Initialize();
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
