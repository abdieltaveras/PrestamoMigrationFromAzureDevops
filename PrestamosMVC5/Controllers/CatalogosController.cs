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
    public class CatalogosController : ControllerBasePcp
    {
        // GET: Catalogos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(string tabla)
        {
            //Hacer peticion al catalogo elegido

            CatalogoVM data = new CatalogoVM();

            switch (tabla)
            {
                case "tblOcupaciones":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblOcupaciones", IdTabla = "IdOcupacion" });
                    data.TipoCatalogo = "Ocupaciones";
                    data.IdTabla = "IdOcupacion";
                    break;
                case "tblVerificadorDirecciones":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblVerificadorDirecciones", IdTabla = "IdVerificadorDireccion" });
                    data.TipoCatalogo = "verificadores de direccion";
                    data.IdTabla = "IdVerificadorDireccion";
                    break;
                case "tblTipoTelefonos":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTipoTelefonos", IdTabla = "IdTipoTelefono" });
                    data.TipoCatalogo = "Tipos de telefonos";
                    data.IdTabla = "IdTipoTelefono";
                    break;
                case "tblTipoSexos":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTipoSexos", IdTabla = "IdTipoSexo" });
                    data.TipoCatalogo = "Sexos";
                    data.IdTabla = "IdTipoSexo";
                    break;
                case "tblTasadores":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTasadores", IdTabla = "IdTasador" });
                    data.TipoCatalogo = "Tasadores";
                    data.IdTabla = "IdTasador";
                    break;
                case "tblLocalizadores":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblLocalizadores", IdTabla = "IdLocalizador" });
                    data.TipoCatalogo = "Localizadores";
                    data.IdTabla = "IdLocalizador";
                    break;
                case "tblEstadosCiviles":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblEstadosCiviles", IdTabla = "IdEstadoCivil" });
                    data.TipoCatalogo = "Estados civiles";
                    data.IdTabla = "IdEstadoCivil";
                    break;
                default:
                    break;
            }
            data.NombreTabla = tabla;
            return View("Catalogos", data);
        }


        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Catalogo catalogo)
        {
            pcpSetUsuarioAndIdNegocioTo(catalogo);
            BLLPrestamo.Instance.CatalogoInsUpd(catalogo);
            return RedirectToAction("CreateOrEdit", new { tabla = catalogo.NombreTabla});
        }
        
        public RedirectToRouteResult ActivarDesactivar(ToggleStatusCatalogo catalogo)
        {
            pcpSetUsuarioTo(catalogo);
            BLLPrestamo.Instance.CatalogoToggleStatus(catalogo);
            return RedirectToAction("CreateOrEdit", new { tabla = catalogo.NombreTabla });
        }
        public RedirectToRouteResult Delete(DelCatalogo catalogo)
        {
            pcpSetUsuarioTo(catalogo);
            BLLPrestamo.Instance.CatalogoDel(catalogo);
            return RedirectToAction("CreateOrEdit", new { tabla = catalogo.NombreTabla });
        }
    }
}