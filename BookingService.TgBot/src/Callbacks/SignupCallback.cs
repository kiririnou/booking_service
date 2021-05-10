using System.Linq;
using System.Threading.Tasks;
using BookingService.Client;
using BookingService.TgBot.StateMachine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class SignupCallback : Callback
    {
        public override string Query => "signup";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

            if (!Bot.UserStates.ContainsKey(message.Chat.Id))
            {
                Bot.UserStates.Add(
                    message.Chat.Id, 
                    new UserStateMachine
                    {
                        CurrentState  = UserState.None,
                        PreviousState = UserState.None
                    }
                );
            }

            IUser iuser = new ApiClientWrapper(AppSettings.GetEntry("URL"));
            var ausers = (await iuser.GetUsersAsync());
            var users = ausers
                .Where(u => u.TgUid == message.Chat.Id.ToString())
                .ToList();

            if (users == null || users.Count == 0)
            {
                await iuser.CreateUserAsync(new Client.Models.User 
                {
                    Name = message.Chat.Username,
                    TgUid = message.Chat.Id.ToString()
                });

                answer = "User has been created successfully!";
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
            else
            {
                answer = $"You are already signed up.\nProcess to menu, please";
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
}
