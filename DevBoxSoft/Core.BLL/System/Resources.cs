using DevBox.Core.Classes;
using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.BLL.System
{
    public static class Resources
    {
        public static List<SystemValue> Get() => Database.DataServer.ExecReaderSelSP<SystemValue>("core.spGetResource").Select(sv => new SystemValue() { Resource = sv.Resource.Decrypt(), Value = sv.Value.Decrypt() }).ToList();
        public static string Get(string resource) => (Database.DataServer.ExecReaderSelSP<SystemValue>("core.spGetResource", SearchRec.ToSqlParams(new { Resource = resource.Encrypt() })).FirstOrDefault()?.Value ?? "").Decrypt();
        public static void Set(SystemValue resource) => Database.DataServer.ExecReaderSelSP("core.spInsUpdResource", SearchRec.ToSqlParams(new { Resource = resource.Resource.Encrypt(), Value = resource.Value.Encrypt() }));
    }
}
