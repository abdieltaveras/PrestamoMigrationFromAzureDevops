using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading;


namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class TestController : ControllerBasePrestamoWS
    {
        [HttpGet("{nombre}")]
        public IActionResult Get(string nombre)
        {
            return Ok($"Mostrando El Nombre y apellido {nombre}");
        }
        [HttpGet("{nombre}/{calle}/{sector}")]
        public IActionResult GetByParams2(string nombre, string calle, string sector)
        {
            return Ok($"Mostrando El Nombre {nombre} calle {calle}  sector {sector}");
        }

        [HttpGet("{ClienteGetParams}")]
        public IActionResult GetByParams(ClienteGetParams clienteParams)
        {
            return Ok($"Mostrando El Nombre {clienteParams.Nombres} apellido {clienteParams.Apellidos}  idCliente {clienteParams.IdCliente}");
        }

        [HttpGet]
        public IEnumerable<int> Test01(int seconds)
        {
            Thread.Sleep(seconds * 1000);
            var list = new int[] {1,2 };
            return list;
        }

        [HttpGet]
        public IEnumerable<LocalidadNegocio> GetLocalidadesNegocioTest()
        {
            var result = BLLPrestamo.Instance.GetLocalidadesNegocio(null);
            return result;
        }
    }
}
