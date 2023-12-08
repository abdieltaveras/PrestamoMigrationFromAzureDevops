using System.Configuration;

namespace DevBox.Core.Classes.Identity
{
    public static class TokenAuthConfig
    {
        public static string Audience => ConfigurationManager.AppSettings["Jwt:Audience"];
        public static string Issuer => ConfigurationManager.AppSettings["Jwt:Issuer"];
        public static string JwtKey => ConfigurationManager.AppSettings["Jwt:Key"];
    }
}