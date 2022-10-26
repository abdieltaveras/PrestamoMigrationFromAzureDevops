using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrestamoBLL;
using PrestamoWS.Models;


using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocalizadoresController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Localizador> Get([FromQuery] LocalizadorGetParams getParams)
        {
            var result = new LocalizadorBLL(this.IdLocalidadNegocio, this.LoginName).Get(getParams);
            return result;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Localizador insUpdParam)
        {
            //insUpdParam.IdLocalidadNegocio = 1;
            var id = new LocalizadorBLL(this.IdLocalidadNegocio, this.LoginName).InsUpd(insUpdParam);
            return Ok(id);
        }
    }
}