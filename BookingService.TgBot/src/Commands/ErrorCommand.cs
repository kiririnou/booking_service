using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot
{
    public class ErrorCommand : Command
    {
        public override string Name => "/default";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Command was not found!"
            );
        }
    }
}
