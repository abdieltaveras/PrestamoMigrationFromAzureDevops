using DevBox.Core.Access;
using DevBox.Core.BLL.Identity.Interfaces;
using DevBox.Core.BLL.System;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using DevBox.Core.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevBox.Core.BLL.Identity
{
    /// <summary>
    /// Recuerde por defecto usamos Dataserver para conecciones y tambien la cadena GrantAdminFullAccess en el archivo de configuracion
    /// </summary>
    public class UsersManager:IUsersManager
    {
        ActionList _defaultActions = null;
        Dictionary<string, ActionList> _groupsActions = new Dictionary<string, ActionList>();
        public ActionList GetDefaultActions()
        {
            if (_defaultActions == null)
            {
                var defActionsSrt = Resources.Get("DefaultActions");
                _defaultActions = defActionsSrt.IsEmpty() ? ActionList.Empty
                                                         : new ActionList(XElement.Parse(defActionsSrt));
            }
            return _defaultActions;
        }
        public void ResetUsrPass(Guid UserPwdResetEntryID, string newPassword)
        {
            var parameters = new { ID = UserPwdResetEntryID, newPassword };
            Database.DataServer.ExecNonQuerySP("core.spResetPw", SearchRec.ToSqlParams(parameters));
        }
        public Guid SetUsrPassReset(CoreUser user, bool forced = false)
        {
            var parameters = new { userID = user.UserID, forced = forced ? 1 : 0 };
            var result = Database.DataServer.ExecReaderSelSP<UserPwdResetEntry>("[core].[spSetPwReset]", SearchRec.ToSqlParams(parameters)).FirstOrDefault();
            return result?.ID ?? Guid.Empty;
        }
        public List<UserPwdResetEntry> GetPwdResetEntries(Guid? id = null, Guid? userID = null)
        {
            var parameters = new { id, userID };
            var result = Database.DataServer.ExecReaderSelSP<UserPwdResetEntry>("core.spGetPwdResetEntries", SearchRec.ToSqlParams(parameters));
            return result;
        }
        public void DeleteUser(Guid userID)
        {
            var parameters = new { userID };
            Database.DataServer.ExecNonQuerySP("core.spDelUser", SearchRec.ToSqlParams(parameters));
        }

        private ActionList getGroupActions(string groupName)
        {
            var gm = new UsersGroupsManager();
            if (!_groupsActions.ContainsKey(groupName))
            {
                _groupsActions[groupName] = gm.GetUserGroup(groupName)?.Actions ?? ActionList.Empty;
            }
            return _groupsActions[groupName];
        }

        Dictionary<string, Func<CoreUser, object>> UserConvertMapNothing => null;
        Dictionary<string, Func<CoreUser, object>> UserConvertMap => new Dictionary<string, Func<CoreUser, object>>
        {
            { "Actions", u => getUserActions(u) }
        };
        public async Task<string> SaveActions(List<ActionPermission> actions)
        {
            var tasks = new List<Task>();
            var gm = new UsersGroupsManager();
            foreach (var action in actions)
            {
                tasks.Add(Task.Run(() =>
                {
                    ActionList actionList = null;
                    var user = GetUser(action.EntityID);
                    if (user != null)
                    {
                        actionList = user.Actions;
                    }
                    else
                    {
                        var group = gm.GetUserGroup(action.EntityID);
                        if (group != null)
                        {
                            actionList = group.Actions;
                        }
                    }
                    if (actionList != null)
                    {
                        actionList[action.ActionID].PermissionLevel = action.ActionPermissionLevel;
                        var subActions = action.SubActionsValues.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subAction in subActions)
                        {
                            var parts = subAction.Split(':');
                            var saVal = parts[0];
                            var saPer = (ActionPermissionLevel)Enum.Parse(typeof(ActionPermissionLevel), parts[1]);
                            actionList[action.ActionID].SubActions[saVal].PermissionLevel = saPer;
                        }
                        var parameters = new { ID = action.EntityID, ActionsStr = actionList.ToXml().ToString().Encrypt() };
                        Database.DataServer.ExecReaderSelSP("core.spSetActions", SearchRec.ToSqlParams(parameters));
                    }
                }));
            }
            await Task.WhenAll(tasks);
            return "";
        }
        object getUserActions(CoreUser u)
        {
            var defActions = GetDefaultActions();
            var grantResult = ConfigurationManager.AppSettings["GrantAdminsFullAccess"];
            var grantAdminsFullAccess = grantResult == null ? false : bool.Parse(grantResult);

            if (u.GroupName == "Admin" && grantAdminsFullAccess)
            {
                defActions.ForEach(a => a.PermissionLevel = ActionPermissionLevel.Allow);
                return defActions;
            }

            var grpActions = getGroupActions(u.GroupName);
            var userActions = new ActionList(XElement.Parse(u.ActionsSrt.Decrypt()));
            var result = ActionManager.MergeActions(defActions, grpActions, userActions);
            return result;

        }
        public CoreUser GetUser(Guid UserID) => Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, SearchRec.ToSqlParams(new { UserID })).FirstOrDefault();
        public CoreUser GetUser(string UserName) => Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, SearchRec.ToSqlParams(new { UserName })).FirstOrDefault();

        public List<CoreUser> GetUsers(Guid? UserID, string UserName, string GroupName, string Email, bool? IsActive)
        {
            
            var parameters = new { UserID, UserName, Email, GroupName, IsActive };
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, SearchRec.ToSqlParams(parameters));
            return result;
        }

        public List<CoreUser> GetAllUsersWithoutActions()
        {
            var parameters = new { };
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMapNothing, SearchRec.ToSqlParams(parameters));
            return result;
        }
        public CoreUser GetUserByNationalID(string NationalID)
        {
            var parameters = SearchRec.ToSqlParams(new { NationalID });
            var user = Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, parameters).FirstOrDefault();
            return user;
        }
        public CoreUser CreateUser(string UserName, string FirstName, string LastName, string Email, string GroupName, string NationalID, bool IsActive, string CreatedBy, List<Func<CoreUser,bool>> conditions)
        {
            var user = new { UserName, FirstName, LastName, Email, NationalID, GroupName, IsActive, CreatedBy };
            var coreUser = new CoreUser { UserName = UserName, Email = Email, NationalID = NationalID, FirstName = FirstName, LastName = LastName };

            if (conditions != null)
            {
                var conditionResults = new List<bool>();
                foreach (var item in conditions)
                {
                    conditionResults.Add(item.Invoke(coreUser));
                }
                if (conditionResults.Contains(false)) return null;
            }
            var result = Database.DataServer.ExecReaderSelSP<CoreUser>("core.spCreateUser", UserConvertMap, SearchRec.ToSqlParams(user)).FirstOrDefault();
            return result;
        }
        public LoginResult ValidateCredentials(LoginCredentials credentials, int durationMinutes=10)
        {
            var user = Database.DataServer.ExecReaderSelSP("core.spAuthUser", UserConvertMap, SearchRec.ToSqlParams(credentials)).FirstOrDefault();
            if (user != null)
            {
                string refToken = TokenManager.GenerateRefreshToken();
                UpdateRefreshToken(new RefreshTokenModel { UserName = user.UserName, RefreshToken = refToken });
                return new LoginResult
                {
                    MustChgPwd = user.MustChangePassword,
                    Token = TokenManager.GenerateToken(user, durationMinutes),
                    RefreshToken = refToken
                };
            }
            else
            {
                return LoginResult.Unauthorized;
            }
        }
        public LoginResult RefreshToken(RefreshTokenModel model, int durationMinutes = 10)
        {
            var user = GetUser(model.UserName);
            if(user == null) return LoginResult.Unauthorized;
            if (model.RefreshToken == user.RefreshToken) {
                string refToken = TokenManager.GenerateRefreshToken();
                UpdateRefreshToken(new RefreshTokenModel { UserName = model.UserName, RefreshToken = refToken });
                return new LoginResult
                {
                    MustChgPwd = user.MustChangePassword,
                    Token = TokenManager.GenerateToken(user, durationMinutes),
                    RefreshToken = refToken
                };
            }
            else
            {
                return LoginResult.Unauthorized;
            }
        }
        public bool UpdateRefreshToken(RefreshTokenModel model)
        {
            var par = SearchRec.ToSqlParams(model);
            var result = Database.DataServer.ExecNonQuerySP("core.spUpdateRefreshToken", par);
            return (result>0);
        }
        public ClaimsPrincipal ParseToken(string token)
        {
            var result = TokenManager.GetPrincipal(token);
            return result;
            //var claims = TokenManager.ParseClaimsFromJwt(token);
            //var user=claims.ToList().Find(c=>c.Type == "exp").Value;
            //var claimsIdentity = new ClaimsIdentity(claims);
            //var result = new ClaimsPrincipal(claimsIdentity);
            //return result;
        }

        public bool ExistEmailForOtherUser(CoreUser user)
        {
            
            var parameters = SearchRec.ToSqlParams(new { Email = user.Email });
            //var parameters = SearchRec.ToSqlParams(new { NationalID })
            var resultUser = Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, parameters).FirstOrDefault();
            return (resultUser != null && resultUser.UserName != user.UserName);
        }

        public  bool ExistNationalIDForOtherUser(CoreUser user)
        {

            var parameters = SearchRec.ToSqlParams(new { NationalId =   user.NationalID });
            var userResult = Database.DataServer.ExecReaderSelSP("core.spGetUsers", UserConvertMap, parameters).FirstOrDefault();
            var result = (userResult != null &&  (userResult.NationalID ==null || userResult.UserName != user.UserName));
            return result;
        }
    }

}
