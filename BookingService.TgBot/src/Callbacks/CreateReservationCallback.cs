using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookingService.Client;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class CreateReservationCallback : Callback
    {
        public override string Query => "createReservation";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

            IUser iu = new ApiClientWrapper(AppSettings.GetEntry("URL"));
            var user = (await iu.GetUsersAsync())
                .Where(u => 
                {
                    Logger.Get().Debug("u.TgUid: " + u.TgUid);
                    return u.TgUid == message.Chat.Id.ToString();
                })
                .Single();

            if (user == null)
            {
                answer = "No user with such ID has been found! Returning to main menu...";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer
                );

                await Bot.Callbacks
                    .Where(c => c.Query == "menu")
                    .SingleOrDefault()
                    .Execute(message, client, from);
                return;
            }

            Regex regex = new Regex(@"Id: (\d+)");
            string flightId = regex.Match(message.Text).Groups[1].Value;

            Logger.Get().Debug($"UserId: {user.Id}\nFlightId: {flightId}");

            IReservation ir = new ApiClientWrapper(AppSettings.GetEntry("URL"));
            await ir.CreateReservationAsync(new Client.Models.Reservation
            {
                Flight = new Client.Models.Flight { Id = int.Parse(flightId) },
                User = new Client.Models.User { Id = user.Id }
            });

            answer = "Reservation has been created successfully!";
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
