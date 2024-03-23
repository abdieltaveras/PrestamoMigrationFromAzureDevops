
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
        public IEnumerable<CodigosCargosDebitosReservados> Get([FromQuery] CodigosCargosGetParams search)
        {
            var result = new CodigosCargosDebitosReservadosBLL(this.IdLocalidadNegocio, this.LoginName).Get(search);
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodigosCargosDebitosReservados param)
        {
            param.Usuario = this.LoginName;
            param.IdLocalidadNegocio = this.IdLocalidadNegocio;
            try
            {
                var id = new CodigosCargosDebitosReservadosBLL(this.IdLocalidadNegocio, this.LoginName).InsUpd(param);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"El codigo no pudo ser creado || {e.Message}");
            }
        }

    }
    
}
