using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookingService.Client;
using BookingService.TgBot.Utils;
using Newtonsoft.Json;
using QRCoder;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookingService.TgBot.Callbacks
{
    public sealed class GenerateQRCodeCallback : Callback
    {
        public override string Query => "generateQRCode";

        public override async Task Execute(Message message, TelegramBotClient client, User from)
        {
            string answer = "";

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
            
            // TODO: add compressing to string
            //*For example: huffman coding
            string rawData = JsonConvert.SerializeObject(reservation);
            string data = await Encrypter.EncryptAsync(rawData);

            using (QRCodeGenerator qrcodeGenerator = new QRCodeGenerator())
            using (QRCodeData qrcodeData = qrcodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrcode = new QRCode(qrcodeData))
            using (Bitmap bitmap = qrcode.GetGraphic(50))
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                TimeSpan timeSpan = DateTime.Now - new DateTime(1970, 1, 1);
                string filename = $"{timeSpan.TotalMilliseconds.ToString()}.png";

                await client.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputOnlineFile(ms, filename),
                    caption: "QrCode generated!",
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
