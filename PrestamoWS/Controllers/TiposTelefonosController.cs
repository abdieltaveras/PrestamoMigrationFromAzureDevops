
using PrestamoEntidades;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TiposTelefonosController : CatalogoController<TipoTelefono>
    {
        public TiposTelefonosController() : base(CatalogoName.TipoTelefono) { }
        [HttpGet]
        public override IEnumerable<TipoTelefono> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        
        [HttpPost]
        public override IActionResult Post([FromBody] TipoTelefono insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override IActionResult Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
    }
    
}
