using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Classes.Identity
{
    public class LoginResult
    {
        public static readonly LoginResult Unauthorized = new LoginResult();
        public bool MustChgPwd { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
