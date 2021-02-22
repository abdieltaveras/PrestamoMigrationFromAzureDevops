using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class PeriodosController : ControllerBasePcp
    {
        // GET: Periodos
        public PeriodosController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        public ActionResult Index()
        {
            var model = new Periodo();
            return View(model);
        }
        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Periodo periodo)
        {
            var model = periodo;
            return RedirectToAction("Index");
        }
    }
}