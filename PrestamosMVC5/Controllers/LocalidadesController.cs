using Newtonsoft.Json;
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

    public class LocalidadesController : Controller
    {
        const int BUSCAR_A_PARTIR_DE = 2;
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buscador()
        {
            TipoBusqueda tipo = new TipoBusqueda();
            return View("Buscador", tipo);
        }

        public ActionResult Editar()
        {
            return View("Edit");
        }
        
        //Buscar ruta de una localidad
        public string Buscar(string IDLocalidad)
        {
            var localidades = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams { IdLocalidad = int.Parse(IDLocalidad) });
            
            return JsonConvert.SerializeObject(localidades);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarLocalidad(LocalidadInsUptParams localidad)
        {
            localidad.IdNegocio = 1;
            BLLPrestamo.Instance.GuardarLocalidad(localidad);

            dynamic message = new { type = "", message = "" };

            //if (localidad.IdLocalidad == 0)
            //{
            //    message = new { type = "edit", message = "Edicion realizada correctamente" };
            //}
            //else
            //{
            //    message = new { type = "edit", message = "Edicion realizada correctamente" };
            //}
            return RedirectToAction("Index");
        }

        public string BuscarLocalidad(string searchToText)
        {
            IEnumerable<Localidad> localidades = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                localidades = BLLPrestamo.Instance.BuscarLocalidad(new BuscarLocalidadParams { Search = searchToText, IdNegocio = 1 });
            }
             return JsonConvert.SerializeObject(localidades);
        }

        public ActionResult CreatePaisDivisionTerritorial()
        {
            var Paises = BLLPrestamo.Instance.GetPaisesDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = 1 });

            return View("CreatePais", Paises);
        }

    }    
}