using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{

    public class LocalidadesController : Controller
    {
        const int BUSCAR_A_PARTIR_DE = 2;
        // GET: Localidades
        public ActionResult Index()
        {
            //var localidades = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams { IdLocalidad = 8 });

            //string test = "La Rom";
            //var localidadess = BLLPrestamo.Instance.BuscarLocalidad(new BuscarLocalidadParams { Search = test });

            ////ViewBag.localidades = localidades;
            //ViewBag.localidadess = localidadess;

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult GuardarLocalidad(Localidad localidad)
        {
            localidad.IdNegocio = 1;
            var localidades = BLLPrestamo.Instance.GuardarLocalidad(localidad);
            
            return RedirectToAction("Index");
        }

        public string BuscarLocalidad(string searchToText)
        {
            IEnumerable<Localidad> localidades = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                localidades = BLLPrestamo.Instance.BuscarLocalidad(new BuscarLocalidadParams { Search = searchToText });
            }
             return JsonConvert.SerializeObject(localidades);
        }
    }
}