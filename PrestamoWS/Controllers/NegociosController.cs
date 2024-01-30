using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

using PrestamoWS.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrestamoWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NegociosController : ControllerBasePrestamoWS
    {
        // GET: api/<LocalidadNegocioController>
        [HttpGet]
        public IActionResult Get([FromQuery] NegociosGetParams param)
        {
            try
            {
                var result = new NegociosBLL(this.LoginName).GetNegocios(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    
        }

        [HttpGet]
        public ActionResult<bool> CodigoYaExiste(string codigo)
        {
            try
            {
                var result = new NegociosBLL(this.LoginName).CodigoYaRegistrado(codigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // POST api/<LocalidadNegocioController>
        [HttpPost]
        public IActionResult Post([FromBody] Negocio param)
        {
            try
            {
                param.Usuario = this.LoginName;
                var result = new NegociosBLL(this.LoginName).InsUpd(param);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
