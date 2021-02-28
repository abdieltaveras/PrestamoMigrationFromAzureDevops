using System.Web.Http;



namespace WSPrestamo.Controllers
{
    public class Test2Controller : ApiController
    {
        
        public IHttpActionResult Get(int idCliente)
        {
            return Ok($"Buscando cliente id {idCliente}");
        }

        
        public IHttpActionResult Get(string nombre, string apellido)
        {
            return Ok($"Mostrando El Nombre {nombre}");
        }
    }
    
}
