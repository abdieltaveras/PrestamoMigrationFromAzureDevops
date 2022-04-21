
using PrestamoEntidades;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TipoSexoController : CatalogoController<TipoSexo>
    {
        public TipoSexoController() : base(CatalogoName.TipoSexo) { }
        [HttpGet]
        public override IEnumerable<TipoSexo> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        [HttpPost]
        public override IActionResult Post([FromBody] TipoSexo insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override IActionResult Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
        
    }
    
}
