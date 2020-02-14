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
        public ActionResult Index(int division_territorial_id = 0)
        {
            TerritorioVM modelo = new TerritorioVM();
            modelo.territorioSeleccionado = division_territorial_id;
            try
            {
                modelo.ListaTerritorios = BLLPrestamo.Instance.TerritorioDivisionesTerritorialesGet(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });
            }
            catch (Exception)
            {
                throw;
            }

            return View(modelo);
        }

        public ActionResult CreateDivisionTerritorial()
        {
            var DivisionesTerritoriales = BLLPrestamo.Instance.TerritorioDivisionesTerritorialesGet(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });
            return View("CreateDivisionTerritorial", DivisionesTerritoriales);
        }

        public string BuscarComponenteDeDivision(string IdDivision)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = pcpUserIdNegocio, IdDivisionTerritorial = int.Parse(IdDivision) });
            return JsonConvert.SerializeObject(territorios);
        }

        public string BuscarTerritorios(string localidadPadre)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarTerritoriosHijos(new TerritorioSearchParams() { IdNegocio = pcpUserIdNegocio, IdLocalidadPadre = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(territorios);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarTerritorio(Territorio territorio)
        {
            this.pcpSetUsuarioAndIdNegocioTo(territorio);
            BLLPrestamo.Instance.TerritorioInsUpd(territorio);
            return RedirectToAction("Index", new { division_territorial_id = territorio.IdDivisionTerritorial });
        }

        [HttpPost]
        public RedirectToRouteResult GuardarDivisionTerritorial(Territorio territorio)
        {
            pcpSetUsuarioAndIdNegocioTo(territorio);
            BLLPrestamo.Instance.TerritorioInsUpd(territorio);
            return RedirectToAction("CreateDivisionTerritorial");
        }
    }
}