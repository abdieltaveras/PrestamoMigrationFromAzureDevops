using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using PrestamoEntidades;

namespace PrestamosMVC5.Controllers
{
    public class TasaInteresController : Controller
    {
    
        // GET: TasaInteres
        public ActionResult Index()
        {
            var intereses = BLLPrestamo.Instance.GetTasaInteres(new TasaInteresGetParams { InteresMensualMenorOIgualA=20} );
            return View(intereses);
        }
    }
}