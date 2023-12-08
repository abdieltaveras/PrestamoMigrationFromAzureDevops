using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.Identity
{
    public class UserPwdResetEntry
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime IssuedDT { get; set; }
        public DateTime ValidUntilDT { get; set; }
        public DateTime ResetDT { get; set; }
    }
}
