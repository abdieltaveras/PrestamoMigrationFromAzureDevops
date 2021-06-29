using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;


namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    /// <summary>
    /// Para registrar los pagos realizados por los clientes a los prestamos
    /// </summary>

    public class ClientesController : ControllerBasePrestamoWS
    {
        
        [HttpGet]
        public IEnumerable<Cliente> Get([FromQuery] ClienteGetParams getParams, bool convertToObj)
        {
            var imgPath = ImagePathForClientes;    
            var data = BLLPrestamo.Instance.GetClientes(getParams, convertToObj,  ImagePathForClientes);
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
        public IActionResult Post([FromBody] Cliente cliente)
        {
            var img = cliente.ImagenesObj;
            cliente.Usuario = this.LoginName;
            cliente.IdLocalidadNegocio = this.IdLocalidadNegocio;
            var state = ModelState.IsValid;
            try
            {
                ManejoImagenes.ProcesarImagenes(cliente.ImagenesObj, ImagePathForClientes , string.Empty);
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
        public IActionResult Delete(int idCliente)
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
