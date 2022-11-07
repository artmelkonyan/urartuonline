using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BonusMarket.Shared.Services
{
    public class EncryptionService
    {
        public EncryptionService()
        {
            
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string CryptPasswordWithSalt(string password, string HashedSalt)
        {
            string hashedPassWithSalt = GetMd5Hash(HashedSalt + GetMd5Hash(password));
            return hashedPassWithSalt;
        }
        public string HashedSaltGenerator()
        {
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltStringHashed = GetMd5Hash(Encoding.UTF8.GetString(salt));
            return saltStringHashed;
        }

        private string GetMd5Hash(string password)
        {
            StringBuilder sBuilder;
            using (MD5 alg = MD5.Create())
            {
                byte[] data = alg.ComputeHash(Encoding.UTF8.GetBytes(password));

                sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X"));
                }
            }
            return sBuilder.ToString();
        }
        public string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }
    }
}