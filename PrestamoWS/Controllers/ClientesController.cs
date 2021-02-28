using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {

        protected InfoAccion InfoAutenticacionDeLaSesion()
        {
            // esto lo obtendra mas real por ahora es para desarrollo
            return new InfoAccion
            {
                IdAplicacion = 1,
                IdDispositivo = 1,
                IdLocalidad = 1,
                IdUsuario = 1,
                Usuario = "UsrDevelopement"
            };
        }

    }


    public class ClientesController : BaseController
    {
        [HttpGet]
        public IEnumerable<Cliente> GetAll()
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams());
            return data;
        }


        [HttpGet("{idCliente:int}")]
        public IEnumerable<Cliente> Get([FromQuery]int idCliente)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { IdCliente = idCliente });
            return data;
        }
        [HttpGet("{nombre} {apellidos} {activo:int} {idCliente:int} {idLocalidad:int} {idTipoIdentificacion:int} {noIdentificacion} {anulado:int}")]
        public IEnumerable<Cliente> Get(string nombre = "", string apellidos = "", int Activo = -1, int idCliente = -1, 
            int idLocalidad = -1, int idTipoIdentificacion = -1, string noIdentificacion = "", int anulado = -1)
        {
            var getP = new ClienteGetParams { Nombres = nombre, Apellidos = apellidos, IdCliente = idCliente, IdLocalidad = idLocalidad, IdNegocio = -1, Activo = Activo, Anulado = anulado, IdTipoIdentificacion = idTipoIdentificacion, NoIdentificacion = noIdentificacion };
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { NoIdentificacion = noIdentificacion });
            return data;
        }


        [HttpGet]
        public IEnumerable<Cliente> GetByParams(ClienteGetParams param)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { });
            return data;
        }

        [HttpGet("{textoABuscar}/{CargarImagenesClientes:bool}")]
        public IEnumerable<Cliente> Get(string textoABuscar, bool CargarImagenesClientes)
        {
            var clientes = searchCliente(textoABuscar, CargarImagenesClientes);
            return clientes;
        }
        /// <summary>
        /// esto es para insertar o actualizar un cliente
        /// </summary>
        /// <param name="cliente"></param>
        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            try
            {
                var id = BLLPrestamo.Instance.InsUpdCliente(cliente);
                return Ok(id);
            }

            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado");
                
            }
        }
        /// <summary>
        /// Esto es para Borrar, anular un cliente
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{idCliente:int}")]
        public ActionResult Delete(int idCliente)
        {
            try
            {
                BLLPrestamo.Instance.AnularClientes(new ClienteDelParams { Id = idCliente, Usuario = "pendiente" });
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        private IEnumerable<Cliente> searchCliente(string searchText, bool CargarImagenesClientes)
        {
            IEnumerable<Cliente> clientes = null;
            clientes = BLLPrestamo.Instance.SearchCliente(new BuscarClienteParams { TextToSearch = searchText, IdNegocio = 1 });
            if (CargarImagenesClientes)
            {
                foreach (var cliente in clientes)
                {
                    //cliente.Imagen1FileName = Url.Content(SiteDirectory.ImagesForClientes + "/" + cliente.Imagen1FileName);
                }
            }
            return clientes;
        }
    }
}
