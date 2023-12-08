using DevBox.Core.Access;
using DevBox.Core.BLL.System;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using DevBox.Core.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevBox.Core.BLL.Identity
{
    public static class UsersManager
    {
        static ActionList _defaultActions = null;
        static Dictionary<string, ActionList> _groupsActions = new Dictionary<string, ActionList>();
        static ActionList getDefaultActions()
        {
            if (_defaultActions == null)
            {
                var defActionsSrt = Resources.Get("DefaultActions");
                _defaultActions = defActionsSrt.IsEmpty() ? ActionList.Empty
                                                         : new ActionList(XElement.Parse(defActionsSrt));
            }
            return _defaultActions;
        }
        private static ActionList getGroupActions(string groupName)
        {
            if (!_groupsActions.ContainsKey(groupName))
            {
                _groupsActions[groupName] = UsersGroupsManager.GetUserGroup(groupName)?.Actions ?? ActionList.Empty;
            }
            return _groupsActions[groupName];
        }
        static Dictionary<string, Func<CoreUser, object>> ConvertMap => new Dictionary<string, Func<CoreUser, object>>
        {
            { "Actions", u => getUserActions(u) }
        };
        static object getUserActions(CoreUser u)
        {
            var defActions = getDefaultActions();
            var userActions = new ActionList(XElement.Parse(u.ActionsSrt.Decrypt()));
            var grpActions = getGroupActions(u.GroupName);
            var result = ActionManager.MergeActions(defActions, userActions, grpActions);
            return result;
        }
        public static List<CoreUser> GetUsers(string UserName, string GroupName, string Email, bool? IsActive)
        {
            var parameters = new { UserName, Email, GroupName, IsActive };
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUsers", ConvertMap, SearchRec.ToSqlParams(parameters));
            return result;
        }
        public static CoreUser CreateUser(string UserName, string FirstName, string LastName, string Email, string GroupName, string CreatedBy)
        {
            var user = new { UserName, FirstName, LastName, Email, GroupName, CreatedBy };
            var result = Database.DataServer.ExecReaderSelSP<CoreUser>("core.spCreateUser", ConvertMap, SearchRec.ToSqlParams(user)).FirstOrDefault();
            return result;
        }
        public static LoginResult ValidateCredentials(LoginCredentials credentials)
        {
            var user = Database.DataServer.ExecReaderSelSP("core.spAuthUser", ConvertMap, SearchRec.ToSqlParams(credentials)).FirstOrDefault();
            return user != null ? new LoginResult { Token = TokenManager.GenerateToken(user) } : LoginResult.Unauthorized;
        }
        public static ClaimsPrincipal ParseToken(string token)
        {
            var result = TokenManager.GetPrincipal(token);
            return result;
            //var claims = TokenManager.ParseClaimsFromJwt(token);
            //var user=claims.ToList().Find(c=>c.Type == "exp").Value;
            //var claimsIdentity = new ClaimsIdentity(claims);
            //var result = new ClaimsPrincipal(claimsIdentity);
            //return result;
        }
    }
}
