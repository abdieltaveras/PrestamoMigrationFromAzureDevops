using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Security
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
