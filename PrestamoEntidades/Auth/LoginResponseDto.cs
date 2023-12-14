using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Auth
{
    public class LoginResponseDto
    {
        public bool MustChgPwd { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }    
    }
}
