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
        public IEnumerable<Ocupacion> Get(string JsonGet = "")
        {
            var search = JsonConvert.DeserializeObject<OcupacionGetParams>(JsonGet);
            search.Usuario = this.LoginName;
            //search.IdOcupacion = -1;
            search.IdLocalidadNegocio = -1;
            return BLLPrestamo.Instance.GetOcupaciones(search);
        }
        

        [HttpPost]
        public IActionResult PostOcupacion(Ocupacion insUpdParam)
        {
            insUpdParam.Usuario = this.LoginName;
            insUpdParam.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdOcupacion(insUpdParam);
            return Ok();
        }
    }
}
