using System.Linq;
using System.Threading.Tasks;
using BookingService.Client;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class FlightsMenuCallback : Callback
    {
        public override string Query => "flightsMenu|fromCountry|toCountry|fromTo";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";
            IFlight ifl = new ApiClientWrapper(AppSettings.GetEntry("URL"));


            if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.InMainMenu)
                Bot.UserStates[message.Chat.Id].SetState(UserState.InFlightsMenu);
            Logger.GetState(message.Chat.Username, Bot.UserStates[message.Chat.Id]);

            if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.InFlightsMenu)
            {
                answer = "Find the country you're going...";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("From", "fromCountry")    // TODO: change name
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("To", "toCountry")        // TODO: change name
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("From... To...", "fromTo")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to menu", "menu")
                        }
                    })
                );
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.DepartureCountryInputStarted)
            {
                answer = "Please, reply on this message with name country of departure:";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new ForceReplyMarkup()
                );

                Bot.UserStates[message.Chat.Id].SetState(StateMachine.UserState.DepartureCountryInputEnded);
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.DepartureCountryInputEnded)
            {
                // Command logic
                var flights = (await ifl.GetFlightsAsync())
                    .Where(f => f.From.Name == message.Text)
                    .ToList();

                // Show all found flights
                if (flights == null)
                {
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "No flights from this country yet. Sorry for inconvenience.",
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
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Found {flights.Count} flights"
                    );
                    foreach (var flight in flights)
                        await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: 
                            $"Id: {flight.Id}\n" +
                            $"From: {flight.From.Name}\n" + 
                            $"To: {flight.To.Name}" +
                            $"Date: {flight.Departure.ToString("dd-MM-yyyy HH:mm:ss")}",
                        replyMarkup: new InlineKeyboardMarkup(new []
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Create reservation", "createReservation")
                            }
                        }) 
                    );
                }

                // Print message with button to return to main menu
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose an action:",
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to menu", "menu")
                        }
                    })
                );
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.ArrivalCountryInputStarted)
            {
                answer = "Please, reply on this message with name country of arrival:";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new ForceReplyMarkup()
                );

                Bot.UserStates[message.Chat.Id].SetState(StateMachine.UserState.ArrivalCountryInputEnded);
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.ArrivalCountryInputEnded)
            {
                // Command logic
                var flights = (await ifl.GetFlightsAsync())
                    .Where(f => f.To.Name == message.Text)
                    .ToList();

                // Show all found flights
                if (flights == null)
                {
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "No flights to this country yet. Sorry for inconvenience.",
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
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Found {flights.Count} flights"
                    );
                    foreach (var flight in flights)
                        await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: 
                            $"Id: {flight.Id}\n" +
                            $"From: {flight.From.Name}\n" + 
                            $"To: {flight.To.Name}" +
                            $"Date: {flight.Departure.ToString("dd-MM-yyyy HH:mm:ss")}",
                        replyMarkup: new InlineKeyboardMarkup(new []
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Create reservation", "createReservation")
                            }
                        }) 
                    );
                }

                // Print message with button to return to main menu
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose an action:",
                    replyMarkup: new InlineKeyboardMarkup(new []
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to menu", "menu")
                        }
                    })
                );
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.DACountryInputStarted)
            {
                answer = "Please, reply on this message with name country of departure and arrival(for example, Ukraine USA):";
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new ForceReplyMarkup()
                );

                Bot.UserStates[message.Chat.Id].SetState(StateMachine.UserState.DACountryInputEnded);
            }
            else if (Bot.UserStates[message.Chat.Id].CurrentState == UserState.DACountryInputEnded)
            {
                string[] countries = message.Text.Split(" ").ToArray();
                // Command logic
                var flights = (await ifl.GetFlightsAsync())
                    .Where(f => f.From.Name == countries[0] && f.To.Name == countries[1])
                    .ToList();

                // Show all found flights
                if (flights == null)
                {
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "No flights with such parameters yet. Sorry for inconvenience.",
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
                    await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Found {flights.Count} flights"
                    );
                    foreach (var flight in flights)
                        await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: 
                            $"Id: {flight.Id}\n" +
                            $"From: {flight.From.Name}\n" + 
                            $"To: {flight.To.Name}" +
                            $"Date: {flight.Departure.ToString("dd-MM-yyyy HH:mm:ss")}",
                        replyMarkup: new InlineKeyboardMarkup(new []
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Create reservation", "createReservation")
                            }
                        }) 
                    );
                }

                // Print message with button to return to main menu
                await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose an action:",
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
