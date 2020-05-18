using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BookingService.TgBot
{
    public static class AppSettings
    {
        private static IConfigurationRoot _configuration;

        public static string GetEntry(string entry)
        {
            _configuration ??= Initialize();
            return _configuration[entry];
        }

        private static IConfigurationRoot Initialize()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
