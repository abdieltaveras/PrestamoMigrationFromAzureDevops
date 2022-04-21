
using PrestamoEntidades;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Colorv2Controller : CatalogoController<Color>
    {
        public Colorv2Controller() : base(CatalogoName.Color) { }
        [HttpGet]
        public override IEnumerable<Color> Get([FromQuery] BaseCatalogoGetParams getParams) => base.GetBase(getParams);
        [HttpPost]
        public override IActionResult Post([FromBody] Color insUpdParam) => base.PostBase(insUpdParam);
        [HttpPost]
        public override IActionResult Delete([FromBody] BaseCatalogoDeleteParams catalogoDelParams) => base.DeleteBase(catalogoDelParams);
    }
    
}
