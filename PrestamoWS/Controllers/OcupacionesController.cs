using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using PrestamoBLL;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]


    public class OcupacionesController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Ocupacion> Get([FromQuery] OcupacionGetParams getParams)
        {
          return BLLPrestamo.Instance.GetOcupaciones(getParams);
        }
        

        [HttpPost]
        public IActionResult PostOcupacion([FromBody]Ocupacion insUpdParam)
        {
            insUpdParam.Usuario = this.LoginName;
            insUpdParam.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdOcupacion(insUpdParam);
            return Ok();
        }
    }
}
