using System;
using System.Xml.Linq;
using DataLayer;
using System.Collections.Generic;
using Models;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    public class LoginLayer
    {
        public LoginEntity SignIn(LoginEntity loginModel)
        {
            LoginEntity loginEntity = new LoginEntity();
            loginModel.PassWord = CalculateMD5Hash(loginModel.PassWord);
            loginEntity.User = new LoginDbProxy().SignIn(loginModel);
            if (loginEntity.User != null)
            {
                loginEntity.UserName = loginModel.UserName;
                loginEntity.LoggedIn = true;
            }else
            {
                loginEntity.LoggedIn = false;
            }
            return loginEntity;
        }
    
        public int? Register(UserEntity user)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            user.PasswordHash = CalculateMD5Hash(user.Password);
            return new LoginDbProxy().Register(user);
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
