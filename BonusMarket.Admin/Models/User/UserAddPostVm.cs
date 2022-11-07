using System.Collections.Generic;

namespace BonusMarket.Admin.Models.User
{
    public class UserAddPostVm : UserBaseVm
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<int> Permissions { get; set; }
        public List<int> Offices { get; set; }
        public Shared.Models.Core.User.User GetDbModel()
        {
            return new Shared.Models.Core.User.User()
            {
                
            };
        }
    }
}