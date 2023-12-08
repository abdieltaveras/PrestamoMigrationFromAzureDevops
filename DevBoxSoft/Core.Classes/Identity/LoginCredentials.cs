using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevBox.Core.Classes.Identity
{
    public class LoginCredentials
    {
        [Required, MinLength(11)]
        public string UserName { get; set; }

        [Required, MinLength(5)]
        public string Password { get; set; }

    }
}
