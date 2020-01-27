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
    public class LocalidadesController : ControllerBasePcp
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
            IEnumerable<Localidad> localidades = new List<Localidad>();
            if (IDLocalidad != "")
            {
                localidades = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams { IdLocalidad = int.Parse(IDLocalidad) });
            }
            
            return JsonConvert.SerializeObject(localidades);
        }

        [HttpPost]
        public RedirectToRouteResult GuardarLocalidad(Localidad localidad)
        {
            //localidad.IdNegocio = 1;
            //localidad.Usuario = "Usuario de prueba";
            pcpSetUsuarioAndIdNegocioTo(localidad);
            try
            {
                BLLPrestamo.Instance.GuardarLocalidad(localidad);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        public string BuscarLocalidad(string searchToText)
        {
            IEnumerable<Localidad> localidades = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                localidades = BLLPrestamo.Instance.BuscarLocalidad(new BuscarLocalidadParams { Search = searchToText, IdNegocio = pcpUserIdNegocio });
            }
             return JsonConvert.SerializeObject(localidades);
        }

        public ActionResult CreatePaisDivisionTerritorial()
        {
            TerritorioVM modelo = new TerritorioVM();
            modelo.ListaTerritorios = BLLPrestamo.Instance.GetPaisesDivisionesTerritoriales(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });

            return View("CreatePais", modelo);
        }

    }    
}