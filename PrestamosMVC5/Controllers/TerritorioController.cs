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
    public class TerritorioController : Controller
    {
        // GET: Territorio
        public ActionResult Index()
        {            
            var territorios = BLLPrestamo.Instance.GetTerritorios(new TerritorioGetParams() { IdNegocio = 1 });
            ViewBag.Territorios = territorios;
            return View();
        }

        public string BuscarTerritorios(string localidadPadre)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.BuscarTerritoriosHijos(new TerritorioSearchParams() { IdNegocio = 1, PadreDe = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(territorios);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarTerritorio(Territorio territorio)
        {
            territorio.IdNegocio = 1;
            var localidades = BLLPrestamo.Instance.GuardarTerritorio(territorio);

            return RedirectToAction("Index");
        }
    }
}