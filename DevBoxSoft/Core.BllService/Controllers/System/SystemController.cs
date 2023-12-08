using DevBox.Core.BLL.System;
using DevBox.Core.Classes.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevBox.Core.BllService.Controllers.System
{
    public class SystemController : ApiController
    {
        [Route("api/diasferiados"), HttpGet, AuthFilter]
        public List<DiaFeriado> GetDiaFeriados(int year = -1) => SysConfiguration.GetDiasFeriados(year);
        [Route("api/diasferiados"), HttpPost, AuthFilter]
        public void PostDiaFeriados(DiaFeriado dia) => SysConfiguration.SaveDiaFeriado(dia);
        [Route("api/diasferiados"), HttpDelete, AuthFilter]
        public void DelDiaFeriados(DiaFeriado dia) => SysConfiguration.DelDiaFeriado(dia);
    }
}
