using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot.Callbacks
{
    public abstract class Callback
    {
        public abstract string Query { get; }

        public abstract Task Execute(Message message, TelegramBotClient client, User from);

        public bool Contains(string query)
        {
            return query.Contains(Query);
        }
    }
}
