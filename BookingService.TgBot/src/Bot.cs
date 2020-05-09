using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BookingService.TgBot
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command> _commands;

        public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }

        public static void Start()
        {
            if (_client == null)
                Initialize();               

            _client.StartReceiving();
            Logger.Get().Information($"{AppSettings.GetEntry("BotName")} started...");
            // System.Console.ReadLine();
            // Logger.Get().Information($"{AppSettings.GetEntry("BotName")} stopped");
        
            while (true) Thread.Sleep(int.MaxValue);
        }

        private static void Initialize()
        {
            _client = new TelegramBotClient(AppSettings.GetEntry("BotToken"));
            _commands = new List<Command>()
            {
                new HelpCommand()
            };

            _client.OnMessage += Handle;

            Logger.Get().Information("Bot initialization completed!");
        }

        private static void Handle(object sender, MessageEventArgs e)
        {
            Logger.Get().Information($"{e.Message.From.FirstName}: {e.Message.Text}");

            Logger.Get().Debug($"_commands count: {_commands.Count}");
            Logger.Get().Debug($"e.Message.Text: {e.Message.Text}");

            // _commands
            //     .Where(c => c.Contains(e.Message.Text))
            //     .FirstOrDefault()
            //     .Execute(e.Message, _client);
        
            _commands
                .Where(c => c.Contains(e.Message.Text))
                .DefaultIfEmpty(new ErrorCommand())
                .First()
                .Execute(e.Message, _client);
        }
    }
}
