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
            var result = ConfigurationManager.AppSettings["emtsoft.ECS"];
            var data = BLLPrestamo.Instance.GetClientes(new ClienteGetParams());
            return data;
        }
        
    }
}
