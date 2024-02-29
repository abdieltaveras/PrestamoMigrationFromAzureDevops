using DevBox.Core.Access;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.BLL.Identity.Interfaces
{
    public interface IUsersManager
    {
        ActionList GetDefaultActions();
        void ResetUsrPass(Guid UserPwdResetEntryID, string newPassword);
        Guid SetUsrPassReset(CoreUser user, bool forced = false);
        List<UserPwdResetEntry> GetPwdResetEntries(Guid? id = null, Guid? userID = null);
        void DeleteUser(Guid userID);
        Task<string> SaveActions(List<ActionPermission> actions);
        CoreUser GetUser(Guid UserID);
        CoreUser GetUser(string UserName);
        List<CoreUser> GetUsers(Guid? UserID, string UserName, string GroupName, string Email, bool? IsActive, int CompanyId = 1);
        List<CoreUser> GetAllUsersWithoutActions();
        CoreUser GetUserByNationalID(string NationalID);
        CoreUser CreateUser(string UserName, string FirstName, string LastName, string Email, string GroupName, string NationalID, bool IsActive, string CreatedBy, List<Func<CoreUser, bool>> conditions);
        LoginResult ValidateCredentials(LoginCredentials credentials, int durationMinutes = 10);
        LoginResult RefreshToken(RefreshTokenModel model, int durationMinutes = 10);
        ClaimsPrincipal ParseToken(string token);
        bool ExistEmailForOtherUser(CoreUser user);
        bool ExistNationalIDForOtherUser(CoreUser user);
    }
}
