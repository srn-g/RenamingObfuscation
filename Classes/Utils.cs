using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RenamingObfuscation.Classes
{
    public static class Utils
    {
        private static List<string> used_names = new List<string>();

        public static string GenerateRandomString()
        {
            string randomString_md5;

            do
            {
                Random rnd = new Random();

                string randomString = GenerateRandomString(rnd.Next(2, 24));

                randomString_md5 = MD5Hash(randomString);

                if (char.IsDigit(randomString_md5[0]))
                {
                    char randomLetter = GetLetter();

                    randomString_md5 = randomString_md5.Replace(randomString_md5[0], randomLetter);
                }
            } while (CheckStringExists(randomString_md5));

            used_names.Add(randomString_md5);

            return randomString_md5;
        }

        private static string GenerateRandomString(int size)
        {
            var charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&()";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private static char GetLetter()
        {
            Random rnd = new Random();

            int num = rnd.Next(0, 26); // Zero to 25

            return (char)('a' + num);
        }

        private static bool CheckStringExists(string stringToCheck)
        {
            if (used_names.Contains(stringToCheck))
                return true;

            return false;
        }
    }
}