
using PrestamoEntidades;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TiposCargosDebitoController : CatalogoController<TipoCargoDebito>
    {
        public TiposCargosDebitoController() : base(CatalogoName.TipoCargoDebito) { }
        [HttpGet]
        public override IEnumerable<TipoCargoDebito> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        [HttpPost]
        public override IActionResult Post([FromBody] TipoCargoDebito insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override IActionResult Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
    }
    
}
