using BookingService.TgBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot
{
    public sealed class StartCommand : Command
    {
        public override string Name => "start";
        
        public override async void Execute(Message message, TelegramBotClient client)
        {
            string answer;
            if (!Bot.UserStates.ContainsKey(message.From.Id))
            {
                answer = "Simple BookingBot helps you to book flight tickets\nPlease, signup to continue";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Signup", "signup")
                        }
                    })
                );
            }
            else 
            {
                answer = $"Process to menu, {message.From.Username}";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Go to menu", "menu")
                        }
                    })
                ); 
            }
        }
    }
}