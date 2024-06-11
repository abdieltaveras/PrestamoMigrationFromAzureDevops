using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Security
{
    public interface IDeleteableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
       
    }
}
