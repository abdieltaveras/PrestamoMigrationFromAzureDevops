using DevBox.Core.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Identity
{
    public class CoreUserGroup : IUserGroup
    {
        public Guid GroupID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }        
        public ActionList Actions { get; set; }
        public string ActionsSrt { get; set; }
        public override string ToString() => GroupName;
    }
}
