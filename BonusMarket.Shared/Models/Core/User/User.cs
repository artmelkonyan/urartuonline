using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BonusMarket.Shared.Models.Core.Permission;

namespace BonusMarket.Shared.Models.Core.User
{
    public enum UserRoleEnum
    {
        ROLE_ADMIN = 0,
        ROLE_USER = 1,
        ROLE_GUEST = 2
    }
    public class User
    {
        
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PasswordHash { get; set; }
        
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public bool? Status { get; set; } = true;
        public UserRoleEnum Role { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }
}