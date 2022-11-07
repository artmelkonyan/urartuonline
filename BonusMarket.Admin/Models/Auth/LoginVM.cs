using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusMarket.Admin.Models.Auth
{
    public class AuthLoginErrMsg
    {
        public static string EmailErrMsg { get; set; } = "No valid Email is provided!"; 
        public static string PasswordErrMsg { get; set; } = "Pasasword is not safe! (5-25) symbols";
        public static string PasswordReqErrMsg { get; set; } = "Please enter the password for your Biditex account.";
        public static string ModelStateNotValidErrMsg { get; set; } = "Wrong email or password!";
        public static string EmailReqErrMsg { get; set; } = "Enter a valid email address.";
        public static string AccessDenied { get; set; } = "Your access is denied";
    }

    public class LoginVM
    {
        [RegularExpression(@"^[A-Za-z0-9._-]+@[A-Za-z0-9.]+\.[A-Za-z]{2,4}$", ErrorMessageResourceName = "EmailErrMsg", ErrorMessageResourceType = typeof(AuthLoginErrMsg))]
        [Required(ErrorMessageResourceName = "EmailReqErrMsg", ErrorMessageResourceType = typeof(AuthLoginErrMsg))]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "PasswordReqErrMsg", ErrorMessageResourceType = typeof(AuthLoginErrMsg))]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }

        public string ModelStateNotValid { get; set; }
    }
}
