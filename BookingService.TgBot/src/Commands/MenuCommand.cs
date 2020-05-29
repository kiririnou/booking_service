using BookingService.TgBot.Commands;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot
{
    public sealed class MenuCommand : Command
    {
        public override string Name => "menu";
        
        public override async void Execute(Message message, TelegramBotClient client)
        {
            string answer = "";

            Bot.UserStates[message.Chat.Id].SetState(UserState.InMainMenu);
            Logger.GetState(message.Chat.Username, Bot.UserStates[message.Chat.Id]);

            answer = $"Welcome back, {message.Chat.Username}\n\nHow can I help you?";
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: answer,
                replyMarkup: new InlineKeyboardMarkup(new []
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("My reservations", "myReservations")
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Flights", "flightsMenu")
                    }
                })
            );
        }
    }
}