using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamoWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ColorController : BaseApiController
    {

        [HttpGet]
        public IEnumerable<Color> Get(string JsonGet = "")
        {
            var JsonResult = JsonGet.ToType<ColorGetParams>();
            var result = BLLPrestamo.Instance.GetColores(JsonResult);
            return result;
            //return View("CreateOrEdit", datos);
        }


        [HttpPost]
        public ActionResult Post(Color color)
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
                return BadRequest(e);
            }

            //return RedirectToAction("CreateOrEdit");
        }
    }
    
}
