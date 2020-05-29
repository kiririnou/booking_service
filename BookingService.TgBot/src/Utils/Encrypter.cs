using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.TgBot.Utils
{
    public static class Encrypter
    {
        public static async Task<string> EncryptAsync(string data)
        {
            string encrypted;
            // byte[] encrypted;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (AesManaged aes = new AesManaged())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.FeedbackSize = 128;
                aes.Padding = PaddingMode.Zeros;
                aes.Key = System.Text.Encoding.UTF8.GetBytes(AppSettings.GetEntry("AesKey"));
                aes.IV = System.Text.Encoding.UTF8.GetBytes(AppSettings.GetEntry("AesIV"));
                // aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        await cs.WriteAsync(dataBytes, 0, dataBytes.Length);
                        cs.FlushFinalBlock();

                    }
                    encrypted = Convert.ToBase64String(ms.ToArray());
                }
            }

            return encrypted;
        }
    
        public static async Task<string> DecryptAsync(string data)
        {
            string decrypted;
            byte[] dataBytes = Convert.FromBase64String(data);

            using (AesManaged aes = new AesManaged())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.FeedbackSize = 128;
                aes.Padding = PaddingMode.Zeros;
                aes.Key = System.Text.Encoding.UTF8.GetBytes(AppSettings.GetEntry("AesKey"));
                aes.IV = System.Text.Encoding.UTF8.GetBytes(AppSettings.GetEntry("AesIV"));
                // aes.GenerateIV();

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(dataBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[dataBytes.Length];
                        await cs.ReadAsync(buffer, 0, buffer.Length);
                        decrypted = Encoding.UTF8.GetString(buffer);
                    }
                }
            }

            return decrypted;    
        }
    }
}