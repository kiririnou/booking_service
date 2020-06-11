
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using BookingService.TgBot.Callbacks;
using BookingService.TgBot.Commands;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

using UserFSMContainer = System.Collections.Generic.Dictionary<long, BookingService.TgBot.StateMachine.UserStateMachine>;

namespace BookingService.TgBot
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command>     _commands;
        private static List<Callback>    _callbacks;

        internal static UserFSMContainer UserStates;

        public static IReadOnlyList<Command> Commands   { get => _commands.AsReadOnly();  }
        public static IReadOnlyList<Callback> Callbacks { get => _callbacks.AsReadOnly(); }

        public static void Start()
        {
            if (_client == null)
                Initialize();
            
            var username = _client.GetMeAsync().GetAwaiter().GetResult().Username;
            var id = _client.GetMeAsync().GetAwaiter().GetResult().Id;
            
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

            //* Now all commands (except ErrorCommand) will be automatically added
            //* and we don't need to worry about new ones
            //* Reflection is power!
            _commands = Assembly.GetAssembly(typeof(Command))?
                .GetTypes()
                .Where(t => typeof(Command).IsAssignableFrom(t) && 
                            !t.IsInterface && 
                            !t.IsAbstract &&
                            t.Name != "ErrorCommand")
                .Select(Activator.CreateInstance)
                .Cast<Command>()
                .ToList();

            _callbacks = Assembly.GetAssembly(typeof(Callback))
                .GetTypes()
                .Where(t => typeof(Callback).IsAssignableFrom(t) &&
                            !t.IsInterface &&
                            !t.IsAbstract &&
                            t.Name != "ErrorCallback")
                .Select(Activator.CreateInstance)
                .Cast<Callback>()
                .ToList();

            UserStates = new UserFSMContainer();

            _client.OnMessage       += OnMessage;
            _client.OnCallbackQuery += OnCallbackQuery;

            Logger.Get().Information("Bot initialization completed!");
        }

        private static async void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Logger.Get().Information($"[Message] {e.Message.From.FirstName}: {e.Message.Text}");

                if (e.Message.ReplyToMessage != null)
                    if (e.Message.ReplyToMessage.From.IsBot)
                    {
                        await _callbacks
                            .Where(c => c.Query.Contains("fromCountry"))
                            .DefaultIfEmpty(new ErrorCallback())
                            .Single()
                            .Execute(e.Message, _client, e.Message.From);
                        return;
                    }

                if (e.Message.Type == MessageType.Text)
                    _commands
                        .Where(c => c.Contains(e.Message.Text))
                        .DefaultIfEmpty(new ErrorCommand())
                        .Single()
                        .Execute(e.Message, _client);
            }
            catch (Exception ex)
            {
                Logger.Get().Error($"Error: {ex.Message}");
                await _client.SendTextMessageAsync(
                    chatId: e.Message.Chat.Id,
                    text: $"An error occurred\n{ex.Message}"
                );
            }
        }

        private static async void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            try
            {
                Logger.Get().Information($"[Callback] {e.CallbackQuery.From.FirstName}: {e. CallbackQuery.Data}");

                //TODO: make proper callback handling
                var chatId = e.CallbackQuery.Message.Chat.Id;
                if (e.CallbackQuery.Data == "fromCountry")
                    if (UserStates[chatId].CurrentState == UserState.InFlightsMenu)
                        UserStates[chatId].SetState(UserState.DepartureCountryInputStarted) ;
                if (e.CallbackQuery.Data == "toCountry")
                    if (UserStates[chatId].CurrentState == UserState.InFlightsMenu)
                        UserStates[chatId].SetState(UserState.ArrivalCountryInputStarted);
                if (e.CallbackQuery.Data == "fromTo")
                    if (UserStates[chatId].CurrentState == UserState.InFlightsMenu)
                        UserStates[chatId].SetState(UserState.DACountryInputStarted);

                await _callbacks
                    .Where(c => c.Query.Contains(e.CallbackQuery.Data))
                    .DefaultIfEmpty(new ErrorCallback())
                    .Single()
                    .Execute(e.CallbackQuery.Message, _client, e.CallbackQuery.Message. From);
            }
            catch (Exception ex)
            {
                Logger.Get().Error($"Error: {ex.Message}");
                await _client.SendTextMessageAsync(
                    chatId: e.CallbackQuery.Message.Chat.Id,
                    text: $"An error occurred\n{ex.Message}"
                );
            }
        }
    }
}
