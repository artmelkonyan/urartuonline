namespace BonusMarket.Admin.Models.Auth
{
    public class BonusMarketAdminAuthenticationDefaults
    {
        public static readonly string CookiePrefix = ".BonusMarketAdmin.";
        public static readonly string ClaimsIssuer = "BonusMarketAdmin";
        public static readonly string ReturnUrlParameter = "";
        public const string AuthenticationScheme = "Cookies";
        public const string ExternalAuthenticationScheme = "ExternalAuthentication";
    }
}