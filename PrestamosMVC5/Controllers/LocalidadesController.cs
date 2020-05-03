using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
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
                localidades = BLLPrestamo.Instance.LocalidadesGet(new LocalidadGetParams { IdLocalidad = int.Parse(IDLocalidad) });
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
                BLLPrestamo.Instance.LocalidadInsUpd(localidad);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        public string BuscarLocalidad(string textToSearch)
        {
            IEnumerable<Localidad> localidades = null;
            if (textToSearch.Length >= BUSCAR_A_PARTIR_DE)
            {
                localidades = BLLPrestamo.Instance.LocalidadSearch(new BuscarLocalidadParams { Search = textToSearch, IdNegocio = pcpUserIdNegocio });
            }
             return JsonConvert.SerializeObject(localidades);
        }

        public ActionResult CreatePaisDivisionTerritorial()
        {
            TerritorioVM modelo = new TerritorioVM();
            modelo.ListaTerritorios = BLLPrestamo.Instance.TerritorioDivisionesTerritorialesPaisesGet(new TerritorioGetParams() { IdNegocio = pcpUserIdNegocio });
            modelo.ListaPaises = BLLPrestamo.Instance.PaisesGet(new LocalidadPaisesGetParams() { IdNegocio = pcpUserIdNegocio });
            return View("CreatePais", modelo);
        }

        public string BuscarLocalidadesHijas(string localidadPadre)
        {
            IEnumerable<LocalidadesHijas> localidades = null;
            localidades = BLLPrestamo.Instance.LocalidadesHijasGet(new LocalidadGetParams() { IdNegocio = pcpUserIdNegocio, IdLocalidad = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(localidades);
        }
    }
}