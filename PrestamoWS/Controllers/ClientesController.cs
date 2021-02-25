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
    public class ClientesController : Controller
    {
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams());
            return data;
        }

        [HttpGet("{id:int}")]
        public IEnumerable<Cliente> Get(int id)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams {IdCliente=id });
            return data;
        }
        [HttpGet("{noIdentificacion}/{nombre}/{apellidos}")]
        public IEnumerable<Cliente> Get(string noIdentificacion, string nombre, string apellidos)
        {
            // todo: el GetClientes hay que adecuarlo para buscar por nombres y apellidos
            //var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { NoIdentificacion = noIdentificacion, Nombre = nombre, Apellidos= apellidos });
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { NoIdentificacion = noIdentificacion });
            return data;
        }

        [HttpGet]
        public IEnumerable<Cliente> GetByParams(ClienteGetParams param)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { });
            return data;
        }

        [HttpGet("{searchToText}/{CargarImagenesClientes:bool}")]
        public IEnumerable<Cliente> Get(string searchText, bool CargarImagenesClientes)
        {
            var clientes = searchCliente(searchText, CargarImagenesClientes);
            return clientes;
        }
        /// <summary>
        /// esto es para insertar o actualizar un cliente
        /// </summary>
        /// <param name="cliente"></param>
        [HttpPost]
        public void Post(Cliente cliente)
        {
            BLLPrestamo.Instance.InsUpdCliente(cliente);
        }
        /// <summary>
        /// Esto es para Borrar, anular un cliente
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Del(int id)
        {
            BLLPrestamo.Instance.AnularClientes(new ClienteDelParams { Id = id, Usuario = "pendiente" });
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
