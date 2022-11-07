
namespace BonusMarket.Shared.Models.Core.Auth
{
    public enum Modules : short
    {
        Auth = 1,
        Home = 2,
        Common = 9,
        Review = 10,
        Bank = 20,
        Pdf = 30,
        Admin = 51
    }
    
    public enum ModuleState : byte
    {
        Disabled = 0,
        Active = 1
    }


    public enum ModuleEnum : short
    {
        AuthWeb = 101,
    }
}