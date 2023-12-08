using DevBox.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Identity
{
    public interface IUser: IAuditableEntity, IDeleteableEntity
    {
        string CompaniesAccess { get; set; }
        int CompanyId { get; set; }
        Guid UserID { get; set; }
        string NationalID { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string GroupName { get; set; }
        bool IsActive { get; set; }
        bool MustChangePassword { get; set; }        
        //string RefreshToken { get; set; }
        //DateTime RefreshTokenExpiryTime { get; set; }
    }
}
