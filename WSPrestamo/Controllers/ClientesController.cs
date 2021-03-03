using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;

namespace WSPrestamo.Controllers
{
    
    public abstract class BaseApiController : ApiController
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


    public class ClientesController : BaseApiController
    {
        
        public IEnumerable<Cliente> GetAll()
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams ());
            return data;
            
        }
        
        public IEnumerable<Cliente> Get(int idCliente)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { IdCliente = idCliente });
            return data;
        }
        
        public IEnumerable<Cliente> Get(string nombre = "", string apellidos = "", int Activo = -1, int idCliente = -1, 
            int idLocalidad = -1, int idTipoIdentificacion = -1, string noIdentificacion = "", int anulado = -1)
        {
            var getP = new ClienteGetParams { Nombres = nombre, Apellidos = apellidos, IdCliente = idCliente, IdLocalidad = idLocalidad, IdNegocio = -1, Activo = Activo, Anulado = anulado, IdTipoIdentificacion = idTipoIdentificacion, NoIdentificacion = noIdentificacion };
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { NoIdentificacion = noIdentificacion });
            return data;
        }


        
        public IEnumerable<Cliente> Get(ClienteGetParams param)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { });
            return data;
        }

        
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
        public IHttpActionResult Post(Cliente cliente)
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
        [HttpDelete]
        public IHttpActionResult Delete(int idCliente)
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
