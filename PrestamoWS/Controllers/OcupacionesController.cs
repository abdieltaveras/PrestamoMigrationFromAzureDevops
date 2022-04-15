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
    public class OcupacionesController : CatalogoController<Ocupacion>
    {
        public OcupacionesController(): base(CatalogoName.Ocupacion)  { }
        [HttpGet]
        public override IEnumerable<Ocupacion> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        [HttpPost]
        public override IActionResult Post([FromBody] Ocupacion insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override void Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
    }
}
