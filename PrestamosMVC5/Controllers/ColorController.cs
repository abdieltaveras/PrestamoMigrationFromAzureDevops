using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class ColorController : ControllerBasePcp
    {
        public ColorController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            ColorVM datos = new ColorVM();
            datos.ListaColores = BLLPrestamo.Instance.ColoresGet(new ColorGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Color color)
        {
            //color.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            this.pcpSetUsuarioAndIdNegocioTo(color);
            BLLPrestamo.Instance.ColorInsUpd(color);
            return RedirectToAction("CreateOrEdit");
        }
    }
}