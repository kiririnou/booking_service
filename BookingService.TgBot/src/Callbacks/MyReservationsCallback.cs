using System.Linq;
using System.Threading.Tasks;
using BookingService.Client;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class MyReservationsCallback : Callback
    {
        public override string Query => "myReservations";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

            Bot.UserStates[message.Chat.Id].SetState(UserState.InReservations);
            Logger.GetState(message.Chat.Username, Bot.UserStates[message.Chat.Id]);

            IReservation ir = new ApiClientWrapper(AppSettings.GetEntry("URL"));
            var reservations = (await ir.GetReservationsAsync())
                .Where(r => r.User.TgUid == message.Chat.Id.ToString())
                .ToList();

            if (reservations == null || reservations.Count == 0)
            {
                answer = "You haven't booked anything yet!";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to menu", "menu")
                        }
                    })
                );
            }
            else
            {
                answer = "Your reservations:";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer
                );

                foreach (var r in reservations)
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: 
                            $"Id: {r.Id.ToString()}\n" +
                            $"Departure: {r.Flight.Departure.ToString("dd-MM-yyyy HH:mm:ss")}\n" +
                            $"From: {r.Flight.From.Name}\n" +
                            $"To: {r.Flight.To.Name}\n" +
                            $"User: {r.User.Name}",
                        replyMarkup: new InlineKeyboardMarkup(new []
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Get info in QR code", "generateQRCode")
                            },
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Delete resrvation", "deleteResrvation")
                            }
                        })
                );

                answer = $"Choose an action:";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to menu", "menu")
                        }
                    })
                );
            }
        }
    }
}
