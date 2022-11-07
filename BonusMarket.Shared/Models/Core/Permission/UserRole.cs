namespace BonusMarket.Shared.Models.Core.Permission
{
    public class UserRole
    {
        public int ID { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public bool Status { get; set; } = true;
        
        public virtual Role Role { get; set; }
        public virtual User.User User { get; set; }
    }
}