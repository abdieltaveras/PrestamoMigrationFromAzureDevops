using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevBox.Core.Classes.Identity
{
    public class ChangeUserPwd
    {
        public Guid id { get; set; }
        public string Password { get; set; }
    }
    public class LoginCredentialsReset
    {
        public string NationalID { get; set; }
    }
    public class LoginCredentialsResetForced
    {
        public Guid id { get; set; }
    }
    public class LoginCredentials
    {
        public string CompanyCode { get; set; }
        [Required, MinLength(11)]
        public string UserName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

    }
}
