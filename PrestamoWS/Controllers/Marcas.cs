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
    public class MarcasControllerOld : CatalogoController<Marca>
    {
        public MarcasControllerOld(): base(CatalogoName.Marcas)  { }
        [HttpGet]
        public override IEnumerable<Marca> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        [HttpPost]
        public override IActionResult Post([FromBody] Marca insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override IActionResult Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
    }
}
