using DevBox.Core.BLL.System;
using DevBox.Core.Classes.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DevBox.Core.BllService.Controllers.System
{
    public class SystemController : BaseApiController
    {      
        [Route("api/diasferiados"), HttpGet, Authorize(Roles = "DíasFeriados")]
        public List<DiaFeriado> GetDiaFeriados(int year = -1) => SysConfiguration.GetDiasFeriados(year);
        
        [Route("api/diasferiados"), HttpPost, Authorize(Roles = "DíasFeriados")]
        public void PostDiaFeriados([FromBody] DiaFeriado diaFeriado) => SysConfiguration.SaveDiaFeriado(diaFeriado);
        
        [Route("api/diasferiados"), HttpDelete, Authorize(Roles = "DíasFeriados")]
        public void DelDiaFeriados(DiaFeriado diaFeriado) => SysConfiguration.DelDiaFeriado(diaFeriado);

        [Route("api/system/policies"), HttpGet, Authorize(Roles = "SysPolicies")]
        public List<SysPolicyCategory> GetSysPolicies()=> SysConfiguration.GetSystemPolicies();

        [Route("api/system/policies"), HttpPost, Authorize(Roles = "SysPolicies")]
        public void PostSysPolicies([FromBody] SysPolicyCategory sysPolicy)=>SysConfiguration.SetSystemPolicies(sysPolicy);        
    }
}
