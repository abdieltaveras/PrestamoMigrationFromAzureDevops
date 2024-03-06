using DevBox.Core.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Identity
{
    public class CoreUser : IUser
    {
        public int CompanyId { get; set; }
        public string CompaniesAccess { get; set; }
        public string CurrentCompanyLocationId { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string LastName { get; set; }
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public bool MustChangePassword { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiryTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ActionList Actions { get; set; }
        public string ActionsSrt { get; set; }        
        public override string ToString() => FullName;
    }
}
