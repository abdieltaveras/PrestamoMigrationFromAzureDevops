using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamoWS.Models
{
    public class NewUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalID { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        
    }
}