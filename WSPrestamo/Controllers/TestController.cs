using PrestamoBLL.Entidades;
using System.Web.Http;

namespace WSPrestamo.Controllers
{
    
    public class TestController : ApiController
    {

        
        public IHttpActionResult Get(int idCliente)
        {
            return Ok($"Buscando cliente id {idCliente}");
        }
        
        public IHttpActionResult Get(string nombre,  string apellido)
        {
            return Ok($"Mostrando El Nombre y apellido {nombre} {apellido}");
        }
        
        public IHttpActionResult Get(string nombre)
        {
            return Ok($"Mostrando El Nombre y apellido {nombre}");
        }
        
        public IHttpActionResult GetByParams2(string nombre, string calle, string sector)
        {
            return Ok($"Mostrando El Nombre {nombre} calle {calle}  sector {sector}");
        }

        
        public IHttpActionResult GetByParams(ClienteGetParams clienteParams)
        {
            return Ok($"Mostrando El Nombre {clienteParams.Nombres} apellido {clienteParams.Apellidos}  idCliente {clienteParams.IdCliente}");
        }
    }
}
