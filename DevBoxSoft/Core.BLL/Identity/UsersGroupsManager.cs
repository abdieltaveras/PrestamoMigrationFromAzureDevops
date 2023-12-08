using DevBox.Core.Access;
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
    public static class UsersGroupsManager
    {
        static Dictionary<string, Func<CoreUserGroup, object>> ConvertMap => new Dictionary<string, Func<CoreUserGroup, object>>
        {
            { "Actions", g => new ActionList(XElement.Parse(g.ActionsSrt.Decrypt()))}
        };
        public static CoreUserGroup GetUserGroup(string groupName)
        {
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUserGroups", ConvertMap, SearchRec.ToSqlParams(new { groupName })).FirstOrDefault();
            return result;
        }
        public static List<CoreUserGroup> GetUserGroups()
        {
            var result = Database.DataServer.ExecReaderSelSP("core.spGetUserGroups", ConvertMap, SearchRec.ToSqlParams(new { }));
            return result;
        }
        public static CoreUserGroup CreateUserGroup(string GroupName, string Description, string CreatedBy)
        {
            var group = new { GroupName, Description, CreatedBy };
            var result = Database.DataServer.ExecReaderSelSP<CoreUserGroup>("core.spCreateUser", SearchRec.ToSqlParams(group)).FirstOrDefault();
            return result;
        }
    }
}
