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
        public IEnumerable<Cliente> GetAll()
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams());
            return data;
        }

        [HttpGet]
        public IEnumerable<Cliente> GetById(int id)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams {IdCliente=id });
            return data;
        }
        [HttpGet]
        public IEnumerable<Cliente> GetByParams(string noIdentificacion, string nombre, string apellido)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams {  });
            return data;
        }

        [HttpGet]
        public IEnumerable<Cliente> GetByParams2(ClienteGetParams param)
        {
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { });
            return data;
        }

        [HttpGet]
        public IEnumerable<Cliente> BuscarClientes(string searchToText, bool CargarImagenesClientes)
        {
            var clientes = searchCliente(searchToText, CargarImagenesClientes);
            return clientes;
        }
        private IEnumerable<Cliente> searchCliente(string searchToText, bool CargarImagenesClientes)
        {
            IEnumerable<Cliente> clientes = null;

            clientes = BLLPrestamo.Instance.SearchCliente(new BuscarClienteParams { TextToSearch = searchToText, IdNegocio = 1 });

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
