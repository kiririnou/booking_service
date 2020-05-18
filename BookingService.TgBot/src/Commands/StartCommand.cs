using BookingService.TgBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot
{
    public class StartCommand : Command
    {
        public override string Name => "start";
        
        public override async void Execute(Message message, TelegramBotClient client)
        {
            string answer = "Simple BookingBot helps you to book flight tickets\nFor more information send /help";

            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: answer
            );
        }
    }
}