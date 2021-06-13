using PrestamoBLL.Entidades;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace WSPrestamo.Controllers
{
    
    public class TestController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Get(int idCliente)
        {
            return Ok($"Buscando cliente id {idCliente}");
        }
        [HttpGet]
        public IHttpActionResult Get(string nombre,  string apellido)
        {
            return Ok($"Mostrando El Nombre y apellido {nombre} {apellido}");
        }
        [HttpGet]
        public IHttpActionResult Get(string nombre)
        {
            return Ok($"Mostrando El Nombre y apellido {nombre}");
        }
        [HttpGet]
        public IHttpActionResult GetByParams2(string nombre, string calle, string sector)
        {
            return Ok($"Mostrando El Nombre {nombre} calle {calle}  sector {sector}");
        }

        [HttpGet]
        public IHttpActionResult GetByParams(ClienteGetParams clienteParams)
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

    }
}
