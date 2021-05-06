using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using Newtonsoft.Json;
namespace WSPrestamo.Controllers
{
 
    public class ColorController : BaseApiController
    {
        //[HttpGet]
        //public IEnumerable<Color> GetByParam(int idColor, int idLocalidadNegocio)
        //{
        //    var search = new ColorGetParams { IdColor = idColor, IdLocalidadNegocio = idLocalidadNegocio };
        //    var result = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
        //    return result;
        //    //return View("CreateOrEdit", datos);
        //}
        [HttpGet]
        public IEnumerable<Color> Get(string JsonGet = "")
        {
            
            var JsonResult = JsonConvert.DeserializeObject<ColorGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetColores(JsonResult);
            return result;
            //return View("CreateOrEdit", datos);
        }
      

        [HttpPost]
        public IHttpActionResult Post(Color color)
        {
            color.Usuario = this.LoginName;
            color.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdColor(color);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
