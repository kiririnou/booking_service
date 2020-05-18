using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using BookingService.TgBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

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
            var username = _client.GetMeAsync().GetAwaiter().GetResult().Username;
            var id = _client.GetMeAsync().GetAwaiter().GetResult().Id;
            
            // Logger.Get().Information($"{AppSettings.GetEntry("BotName")} started...");
            Logger.Get().Information($"Bot: {username}\tId: {id.ToString()}");
            Logger.Get().Information($"Bot started...");
            _client.StartReceiving();
            // System.Console.ReadLine();
            // Logger.Get().Information($"{AppSettings.GetEntry("BotName")} stopped");
        
            while (true) Thread.Sleep(int.MaxValue);
        }

        private static void Initialize()
        {
            _client = new TelegramBotClient(AppSettings.GetEntry("BotToken"));

            // Now all commands (except ErrorCommand) will be automatically added
            // and we don't need to worry about new ones
            // Reflection is power!
            _commands = Assembly.GetAssembly(typeof(Command))?
                .GetTypes()
                .Where(t => typeof(Command).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract &&
                            t.Name != "ErrorCommand")
                .Select(Activator.CreateInstance)
                .Cast<Command>()
                .ToList();

            _client.OnMessage += Handle;

            Logger.Get().Information("Bot initialization completed!");
        }

        private static void Handle(object sender, MessageEventArgs e)
        {
            Logger.Get().Information($"{e.Message.From.FirstName}: {e.Message.Text}");

            Logger.Get().Debug($"_commands count: {_commands.Count.ToString()}");
            Logger.Get().Debug($"e.Message.Text: {e.Message.Text}");

            if (e.Message.Type == MessageType.Text)
                _commands
                .Where(c => c.Contains(e.Message.Text))
                .DefaultIfEmpty(new ErrorCommand())
                .First()
                .Execute(e.Message, _client);
        }
    }
}
