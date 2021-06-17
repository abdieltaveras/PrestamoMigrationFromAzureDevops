using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using WSPrestamo.Utilidades;
using PcpUtilidades;

namespace WSPrestamo.Controllers
{

    /// <summary>
    /// Para registrar los pagos realizados por los clientes a los prestamos
    /// </summary>

    public class ClientesController : BaseApiController
    {
        public IEnumerable<Cliente> Get(int idCliente, string codigo = "", string nombres = "", string apellidos = "", int Activo = -1,
            int idLocalidad = -1, int idTipoIdentificacion = -1, string noIdentificacion = "", int anulado = -1, DateTime? insertadoDesde = null, DateTime? insertadoHasta = null, int seleccionarCantidadRegistros = -1, int idRegistroInicioSeleccion = -1,
            bool convertJsonToObj = false
            )
        {

            var getParams = new ClienteGetParams
            {
                Codigo = codigo,
                Nombres = nombres,
                Apellidos = apellidos,
                IdCliente = idCliente,
                IdLocalidadNegocio = idLocalidad,
                IdNegocio = -1,
                Activo = Activo,
                Anulado = anulado,
                IdTipoIdentificacion = idTipoIdentificacion,
                NoIdentificacion = noIdentificacion,
                InsertadoDesde = insertadoDesde,
                InsertadoHasta = insertadoHasta,
                CantidadRegistrosASeleccionar = seleccionarCantidadRegistros,
                SeleccionarLuegoDelIdCliente = idRegistroInicioSeleccion,
                Usuario = this.LoginName,
                ConvertJsonToObj = convertJsonToObj
            };
            var data = BLLPrestamo.Instance.GetClientes(getParams, DirectorioDeImagenes.ParaClientes);
            return data;
        }


        public IEnumerable<Cliente> Get(string jsonGet)
        {
            var getParams = jsonGet.ToType<ClienteGetParams>();
            var data = BLLPrestamo.Instance.GetClientes(getParams, DirectorioDeImagenes.ParaClientes);
            return data;
        }

        [HttpGet]
        public IEnumerable<Cliente> SearchClientes(string textoABuscar, bool cargarImagenesClientes=false)
        {
            var clientes = searchCliente(textoABuscar, cargarImagenesClientes);
            return clientes;
        }
        /// <summary>
        /// esto es para insertar o actualizar un cliente
        /// </summary>
        /// <param name="cliente"></param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Cliente cliente)
        {
            var img = cliente.ImagenesObj;
            cliente.Usuario = this.LoginName;
            cliente.IdLocalidadNegocio = this.IdLocalidadNegocio;
            var state = ModelState.IsValid;
            try
            {
                ManejoImagenes.ProcesarImagenes(cliente.ImagenesObj, DirectorioDeImagenes.ParaClientes, string.Empty);
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
