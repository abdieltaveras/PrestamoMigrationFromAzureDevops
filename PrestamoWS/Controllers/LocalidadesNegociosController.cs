﻿using PrestamoBLL;
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
    public class LocalidadesNegociosController : ControllerBasePrestamoWS
    {
        // GET: api/<LocalidadNegocioController>
        [HttpGet]
        public IEnumerable<LocalidadNegocio> Get([FromQuery] LocalidadNegociosGetParams param)
        {
            var result = BLLPrestamo.Instance.GetLocalidadesNegocio(param);
            return result;
        }

        // POST api/<LocalidadNegocioController>
        [HttpPost]
        public IActionResult Post([FromBody] LocalidadNegocio param)
        {
            try
            {
                param.IdNegocio = 1;
                param.Usuario = this.LoginName;
                param.IdLocalidadNegocio = this.IdLocalidadNegocio;
                BLLPrestamo.Instance.InsUpdLocalidadNegocio(param);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
