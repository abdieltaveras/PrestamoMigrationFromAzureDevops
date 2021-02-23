using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get(int idCliente)
        {
            return Content($"Buscando cliente id {idCliente}");
        }
        [HttpGet]
        public IActionResult Nombre(string nombre)
        {
            return Content($"Mostrando El Nombre {nombre}");
        }
    }
}
