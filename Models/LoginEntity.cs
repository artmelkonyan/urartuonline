using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LoginEntity
    {
        public string UserName { get; set; } = null;
        public string PassWord { get; set; } = null;
        public UserEntity User {get;set; } = null;
        public bool? LoggedIn { get; set; } = null;
        public string ErrorMessage { get; set; } = "Սխալ ծածկանուն և գաղտնաբառ";
    }
}
