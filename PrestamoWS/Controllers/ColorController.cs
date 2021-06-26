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
using Microsoft.AspNetCore.Http;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ColorController : ControllerBasePrestamoWS
    {
        
        [HttpGet]
        public IEnumerable<Color> Get(string JsonGet = "")
        {
            var JsonResult = JsonConvert.DeserializeObject<ColorGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetColores(JsonResult);
            return result;
            //return View("CreateOrEdit", datos);
        }
      

        [HttpPost]
        public IActionResult Post(Color color)
        {
            color.Usuario = this.LoginName;
            color.IdLocalidadNegocio = this.IdLocalidadNegocio;
            try
            {
                BLLPrestamo.Instance.InsUpdColor(color);
                return Ok();
            }
            catch (Exception e)
            {

                return InternalServerError(e.Message);
            }
            
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
