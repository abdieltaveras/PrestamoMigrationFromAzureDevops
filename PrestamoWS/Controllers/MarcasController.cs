using Microsoft.AspNetCore.Mvc;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MarcasController : Controller
    {
        [HttpGet]
        public Marca Get()
        {
            return new Marca();
        }
    }
}
