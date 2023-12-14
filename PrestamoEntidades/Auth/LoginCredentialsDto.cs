using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Auth
{
    public class LoginCredentialsDto
    {
        public string CompanyCode { get; set; }
        [Required, MinLength(11)]
        public string UserName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
