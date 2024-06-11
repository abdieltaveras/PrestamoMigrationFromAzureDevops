using DevBox.Core.Access;
using DevBox.Core.BLL.System;
using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using DevBox.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevBox.Core.BLL.Identity
{
    public class UsersGroupsManager
    {
        Dictionary<string, Func<CoreUserGroup, object>> ConvertMap => new Dictionary<string, Func<CoreUserGroup, object>>
        {
            { "Actions", g => getGroupActions(g)}
        };
        ActionList _defaultActions = null;
        ActionList GetDefaultActions()
        {
            if (_defaultActions == null)
            {
                var defActionsSrt = Resources.Get("DefaultActions");
                _defaultActions = defActionsSrt.IsEmpty() ? ActionList.Empty
                                                          : new ActionList(XElement.Parse(defActionsSrt));
            }
            return _defaultActions;
        }
        private object getGroupActions(CoreUserGroup g)
        {
            var defActions = GetDefaultActions();
            var actions = new ActionList(XElement.Parse(g.ActionsSrt.Decrypt()));
            var result = ActionManager.MergeActions(defActions, actions);
            return result;
        }
        public CoreUserGroup GetUserGroup(Guid GroupID) => Database.DataServer.ExecReaderSelSP("core.spGetUserGroups", ConvertMap, SearchRec.ToSqlParams(new { GroupID })).FirstOrDefault();
        public CoreUserGroup GetUserGroup(string groupName)
        {
            var result = GetUserGroups(groupName).FirstOrDefault();
            return result;
        }
        public List<CoreUserGroup> GetUserGroups(string GroupName)
        {
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUserGroups", ConvertMap, SearchRec.ToSqlParams(new { GroupName }));
            return result;
        }
        public CoreUserGroup CreateUserGroup(string GroupName, string Description, string CreatedBy, int CompanyId)
        {
            var group = new { GroupName, Description, Actions = "", CreatedBy, CompanyId };
            var result = Database.DataServer.ExecReaderSelSP<CoreUserGroup>("[core].[spInsUpdUserGroup]", SearchRec.ToSqlParams(group)).FirstOrDefault();
            return result;
        }
    }
}
