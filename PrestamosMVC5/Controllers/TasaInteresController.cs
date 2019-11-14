using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoEntidades;

namespace PrestamosMVC5.Controllers
{
    public class TasaInteresController : Controller
    {
        // GET: TasaInteres
        public ActionResult Index()
        {
            List<TasaInteres> interes = new List<TasaInteres>{ 
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 10, Activo = false, RequiereAutorizacion = false},
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 20, Activo = true, RequiereAutorizacion = true},
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 30, Activo = true, RequiereAutorizacion = true},
            };

            return View(interes);
        }
    }
}