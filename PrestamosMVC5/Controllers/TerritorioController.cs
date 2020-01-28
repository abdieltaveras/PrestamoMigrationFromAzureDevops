using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoEntidades;
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
    public class TerritorioController : ControllerBasePcp
    {
        // GET: Territorio
        public ActionResult Index()
        {
            TerritorioVM modelo = new TerritorioVM();

            try
            {
                modelo.ListaTerritorios = BLLPrestamo.Instance.GetDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });
            }
            catch (Exception)
            {
                throw;
            }

            return View(modelo);
        }

        public ActionResult CreateDivisionTerritorial()
        {
            var DivisionesTerritoriales = BLLPrestamo.Instance.GetDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });
            return View("CreateDivisionTerritorial", DivisionesTerritoriales);
        }

        public string BuscarComponenteDeDivision(string IdDivision)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.BuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = pcpUserIdNegocio, IdDivisionTerritorial = int.Parse(IdDivision) });
            return JsonConvert.SerializeObject(territorios);
        }

        public string BuscarTerritorios(string localidadPadre)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.BuscarTerritoriosHijos(new TerritorioSearchParams() { IdNegocio = pcpUserIdNegocio, HijoDe = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(territorios);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarTerritorio(Territorio territorio)
        {
            this.pcpSetUsuarioAndIdNegocioTo(territorio);
            var localidades = BLLPrestamo.Instance.GuardarTerritorio(territorio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult GuardarDivisionTerritorial(Territorio territorio)
        {
            pcpSetUsuarioAndIdNegocioTo(territorio);
            var localidades = BLLPrestamo.Instance.GuardarTerritorio(territorio);
            return RedirectToAction("CreateDivisionTerritorial");
        }
    }
}