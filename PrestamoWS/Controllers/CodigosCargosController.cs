
using PrestamoEntidades;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using System;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CodigosCargosController : ControllerBasePrestamoWS
    {

        [HttpGet]
        public IEnumerable<CodigosCargosDebitos> Get([FromQuery] CodigosCargosGetParams search)
        {
            
            var result = new CodigosCargosDebitosBLL(this.IdLocalidadNegocio, this.LoginName).Get(search);
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodigosCargosDebitos param)
        {
            param.Usuario = this.LoginName;
            param.IdLocalidadNegocio = this.IdLocalidadNegocio;
            try
            {
                var id = new CodigosCargosDebitosBLL(this.IdLocalidadNegocio, this.LoginName).InsUpd(param);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"El codigo no pudo ser creado || {e.Message}");
            }
        }

    }
    
}
