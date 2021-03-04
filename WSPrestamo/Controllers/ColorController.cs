using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;

namespace WSPrestamo.Controllers
{
 
    public class ColorController : BaseApiController
    {

        public IEnumerable<Color> GetAll()
        {
            ColorVM datos = new ColorVM();
            datos.ListaColores = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
            return datos.ListaColores;
            //return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public IHttpActionResult Post(Color color)
        {
            //color.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(color);
            BLLPrestamo.Instance.InsUpdColor(color);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
