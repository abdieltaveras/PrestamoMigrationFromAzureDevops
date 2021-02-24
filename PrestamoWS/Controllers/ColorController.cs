using Microsoft.AspNetCore.Mvc;
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
    public class ColorController : Controller
    {
        [HttpGet]
        public void SaveGet()
        {
            ColorVM datos = new ColorVM();
            datos.ListaColores = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });

            //return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public void SavePost(Color color)
        {
            //color.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(color);
            BLLPrestamo.Instance.InsUpdColor(color);
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
