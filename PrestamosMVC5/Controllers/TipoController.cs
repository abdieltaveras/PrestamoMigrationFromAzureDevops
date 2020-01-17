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
    public class TipoController : Controller
    {
        // GET: Tipo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrEdit()
        {
            TipoVM datos = new TipoVM();
            datos.Tipo = new Tipo();
            datos.ListaTipos = BLLPrestamo.Instance.GetTipos(new TipoGetParams { IdNegocio = 1 });

            return View("CreateOrEdit", datos);
        }
        
        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Tipo tipo)
        {
            TipoInsUpdParams TipoAInsertar = new TipoInsUpdParams()
            {
                Nombre = tipo.Nombre,
                IdNegocio = 1,
                IdClasificacion = tipo.IdClasificacion,
                InsertadoPor = "Bryan"
            };

            BLLPrestamo.Instance.InsUpdTipo(TipoAInsertar);
            return RedirectToAction("CreateOrEdit");
        }
    }
}