using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookingService.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class DeteleReservationCallback : Callback
    {
        public override string Query => "deleteResrvation";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

            // Bot.UserStates[message.Chat.Id].SetState(UserState.InMainMenu);
            Logger.GetState(message.Chat.Username, Bot.UserStates[message.Chat.Id]);

            Regex regex = new Regex(@"Id: (\d+)");
            string flightId = regex.Match(message.Text).Groups[1].Value;

            IReservation ir = new ApiClientWrapper(AppSettings.GetEntry("URL"));
            var reservation = await ir.GetReservationByIdAsync(int.Parse(flightId));

            if (reservation == null)
            {
                answer = "Reservation has not been found!";
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
                return;
            }

            if (await ir.DeleteReservationAsync(reservation.Id))
            {
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Reservation has been successfully deleted!",
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Go to menu", "menu")
                        }
                    })
                );
            }
            else
            {
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Something went wrong. Return to main menu.",
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
