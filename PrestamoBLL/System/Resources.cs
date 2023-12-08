using DevBox.Core.Classes.Configuration;
using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoBLL.Core.System
{
    public static class Resources
    {
        public static List<SystemValue> Get() => Database.DataServer.ExecReaderSelSP<SystemValue>("core.spGetResource").Select(sv => new SystemValue() { Resource = sv.Resource.Decrypt(), Value = sv.Value.Decrypt() }).ToList();
        public static string Get(string resource) => (Database.DataServer.ExecReaderSelSP<SystemValue>("core.spGetResource", SearchRec.ToSqlParams(new { Resource = resource.Encrypt() })).FirstOrDefault()?.Value ?? "").Decrypt();
        public static void Set(SystemValue resource) => Database.DataServer.ExecReaderSelSP("core.spInsUpdResource", SearchRec.ToSqlParams(new { Resource = resource.Resource.Encrypt(), Value = resource.Value.Encrypt() }));
    }
}
