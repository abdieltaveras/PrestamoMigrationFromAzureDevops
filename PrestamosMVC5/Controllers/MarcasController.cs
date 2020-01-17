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
    public class MarcasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            MarcaVM datos = new MarcaVM();
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1});

            return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(MarcaInsUpdParams marca)
        {
            marca.IdNegocio = 1;
            marca.InsertadoPor = "Bryan";
            BLLPrestamo.Instance.InsUpdMarca(marca);
            return RedirectToAction("CreateOrEdit");
        }
    }
}