using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common
{
    public class PasswordHelper
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        public static string CreateMd5Hash(string value)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(value);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            return BitConverter.ToString(hash);
        }

        public static string CreateShaHash(string value)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(value);

            return Convert.ToBase64String(SHA256.Create().ComputeHash(encodedPassword));
        }

        public static int GetRandomNumber(int min, int max)
        {
            lock (SyncLock)
            { // synchronize
                return Random.Next(min, max);
            }
        }

        public static string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            if (length > 0)
            {
                StringBuilder sb = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                    sb.Append(characters[GetRandomNumber(0, characters.Length)]);

                return sb.ToString();
            }
            return string.Empty;
        }
    }
}