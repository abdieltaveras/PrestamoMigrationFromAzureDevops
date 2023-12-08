using DevBox.Core.Classes.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.BLL.System
{
    public static partial class SysConfiguration
    {
        const string SYSPOL_ELEMENT_NAME = "sysPolicies";
        public static List<SysPolicyCategory> GetSystemPolicies()
        {
            var result = new List<SysPolicyCategory>() { new SysPolicyCategory(Resources.Get(SYSPOL_ELEMENT_NAME)) };
            return result;
        }
        public static void SetSystemPolicies(SysPolicyCategory _system) =>
          Resources.Set(new SystemValue() { Resource = SYSPOL_ELEMENT_NAME, Value = _system.ToXml().ToString() });

    }
}
