using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot.Callbacks
{
    public sealed class ErrorCallback : Callback
    {
        public override string Query => null;

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

            // Bot.UserStates[message.Chat.Id].SetState(UserState.InMainMenu);
            Logger.GetState(message.Chat.Username, Bot.UserStates[message.Chat.Id]);

            answer = $"Incorrect callback query. Returning to main menu...";
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: answer
            );

            await Bot.Callbacks
                .Where(c => c.Query == "menu")
                .SingleOrDefault()
                .Execute(message, client, from);
        }
    }
}
