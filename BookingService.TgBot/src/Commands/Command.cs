using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookingService.TgBot
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string cmd)
        {
            return cmd.Contains(Name);
        }
    }
}
