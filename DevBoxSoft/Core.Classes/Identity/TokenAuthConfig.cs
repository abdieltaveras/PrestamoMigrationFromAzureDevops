using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.Identity
{
    public static class TokenAuthConfig
    {
        public static string JwtKey => ConfigurationManager.AppSettings["Jwt:Key"];
        public static string Issuer => ConfigurationManager.AppSettings["Jwt:Issuer"];
        public static string Audience => ConfigurationManager.AppSettings["Jwt:Audience"];
    }
}
