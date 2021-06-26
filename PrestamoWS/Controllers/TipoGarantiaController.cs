using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrestamoBLL;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class TipoGarantiaController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<TipoGarantia> Get(string JsonGet = "")
        {
            var paramss = JsonConvert.DeserializeObject<TipoGetParams>(JsonGet);
            return BLLPrestamo.Instance.TiposGarantiaGet(paramss);
            //return BllAcciones.GetData<TipoGarantia, TipoGetParams>(searchParam, "spGetTiposGarantia", GetValidation);
        }
        [HttpPost]

        public void Post(TipoGarantia insUpdParam)
        {
            //BllAcciones.InsUpdData<TipoGarantia>(insUpdParam, "spInsUpdTipoGarantia");
        }
    }
}