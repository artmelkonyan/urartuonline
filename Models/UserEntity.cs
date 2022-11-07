using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum UserRole
    {
        ROLE_ADMIN = 0,
        ROLE_USER = 1,
        ROLE_GUEST = 2
    }
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}