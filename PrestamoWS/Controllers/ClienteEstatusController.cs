using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using HESRAM.Utils;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Data;

namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClienteEstatusController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<ClienteEstatusGet> Get([FromQuery]ClienteEstatusGetParams search)
        {
            var result = new ClienteEstatusBLL(this.IdLocalidadNegocio, this.LoginName).Get(search);
            return result;
        }
        [HttpPost]
        public IActionResult Post([FromBody] ClienteEstatus insupdparam)
        {
            var state = ModelState.IsValid;
            try
            {
                var id = new ClienteEstatusBLL(this.IdLocalidadNegocio, this.LoginName).InsUpd(insupdparam);
                return Ok(id);
            }
            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado");

            }
        }
    }
}
