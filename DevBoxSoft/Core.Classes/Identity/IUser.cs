using DevBox.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Identity
{
    public interface IUser: IAuditableEntity, IDeleteableEntity
    {
        Guid UserID { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string GroupName { get; set; }
        bool IsActive { get; set; }
        //string RefreshToken { get; set; }
        //DateTime RefreshTokenExpiryTime { get; set; }
    }
}
