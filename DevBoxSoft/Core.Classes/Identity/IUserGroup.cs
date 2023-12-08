using DevBox.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Identity
{
    public interface IUserGroup:IAuditableEntity, IDeleteableEntity
    {
        Guid GroupID { get; set; }
        string GroupName { get; set; }
        string Description{ get; set; }        
    }
}
