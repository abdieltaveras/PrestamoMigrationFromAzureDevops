using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrestamoBLL.Entidades;
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
        [HttpGet("Idcliente")]
        public IActionResult Get(int idCliente)
        {
            return Content($"Buscando cliente id {idCliente}");
        }
        [HttpGet]
        [HttpGet("nombre,apellido")]
        public IActionResult GetByParams1(string nombre,  string apellido)
        {
            return Content($"Mostrando El Nombre y apellido {nombre} {apellido}");
        }
        [HttpGet]
        public IActionResult GetByName(string nombre)
        {
            return Content($"Mostrando El Nombre y apellido {nombre}");
        }
        [HttpGet]
        public IActionResult GetByParams2(string nombre, string calle, string sector)
        {
            return Content($"Mostrando El Nombre {nombre} calle {calle}  sector {sector}");
        }

        [HttpGet]
        public IActionResult GetByParams([FromQuery] ClienteGetParams clienteParams)
        {
            return Content($"Mostrando El Nombre {clienteParams.Nombres} apellido {clienteParams.Apellidos}  idCliente {clienteParams.IdCliente}");
        }
    }
}
