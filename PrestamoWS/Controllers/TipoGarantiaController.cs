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
        public IEnumerable<TipoGarantia> Get([FromQuery] TipoGarantiaGetParams getParams)
        {
            
            return BLLPrestamo.Instance.TiposGarantiaGet(getParams);
            //return BllAcciones.GetData<TipoGarantia, TipoGetParams>(searchParam, "spGetTiposGarantia", GetValidation);
        }
        [HttpPost]

        public void Post([FromBody] TipoGarantia tipoGarantia)
        {
            //BllAcciones.InsUpdData<TipoGarantia>(insUpdParam, "spInsUpdTipoGarantia");
        }
    }
}