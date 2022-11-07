using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BonusMarket.Admin.Models.User
{
    public class UserEditPostVm : UserBaseVm
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<int> Permissions { get; set; }
        public Shared.Models.Core.User.User GetDbModel()
        {
            return new Shared.Models.Core.User.User()
            {
                Id = Id,
                Email = Email,
                PasswordHash = Password,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Phone = Phone,
            };
        }
    }
}