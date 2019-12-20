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
            var DivisionesTerritoriales = BLLPrestamo.Instance.GetDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = 1 });

            //IEnumerable<TerritoriosConHijo> territorios = BLLPrestamo.Instance.GetTerritorios(new TerritorioGetParams() { IdNegocio = 1 });
            ViewBag.DivisionesTerritoriales = DivisionesTerritoriales;
            return View();
        }

        public ActionResult CreateDivisionTerritorial()
        {
            var DivisionesTerritoriales = BLLPrestamo.Instance.GetDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = 1 });
            return View("CreateDivisionTerritorial", DivisionesTerritoriales);
        }        

        public string BuscarComponenteDeDivision(string IdDivision)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.BuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorial = int.Parse(IdDivision) });
            return JsonConvert.SerializeObject(territorios);
        }

        public string BuscarTerritorios(string localidadPadre)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.BuscarTerritoriosHijos(new TerritorioSearchParams() { IdNegocio = 1, HijoDe = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(territorios);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarTerritorio(Territorio territorio)
        {
            territorio.IdNegocio = 1;
            var localidades = BLLPrestamo.Instance.GuardarTerritorio(territorio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult GuardarDivisionTerritorial(Territorio territorio)
        {
            territorio.IdNegocio = 1;
            var localidades = BLLPrestamo.Instance.GuardarTerritorio(territorio);
            return RedirectToAction("CreateDivisionTerritorial");
        }
    }
}