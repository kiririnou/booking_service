using System.Linq;
using BookingService.TgBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot
{
    public sealed class HelpCommand : Command
    {
        public override string Name => "help";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            string answer = string.Format(
                "Available commands:\n{0}", 
                string.Join("\n", string.Join("\n", Bot.Commands.Select(c => "/" + c.Name).ToArray()))
            );

            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: answer
            );
        }
    }
}
