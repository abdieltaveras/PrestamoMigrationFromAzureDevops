using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class Test2Controller : Controller
    {
        [HttpGet("{idCliente}")]
        public IActionResult Get(int idCliente)
        {
            return Content($"Buscando cliente id {idCliente}");
        }

        [HttpGet("{nombre}/{apellido}")]
        public IActionResult Get(string nombre, string apellido)
        {
            return Content($"Mostrando El Nombre {nombre}");
        }
    }
    
}
