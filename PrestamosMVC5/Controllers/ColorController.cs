using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            ColorVM datos = new ColorVM();
            datos.ListaColores = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1 });

            return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Color color)
        {
            color.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            BLLPrestamo.Instance.InsUpdColor(color);
            return RedirectToAction("CreateOrEdit");
        }
    }
}