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

        public ActionResult CreateOrEdit(string tipoCatalogo)
        {
            //Hacer peticion al catalogo elegido

            CatalogoVM data = new CatalogoVM();

            //data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblOcupaciones" }, tipoCatalogo);
            //data.TipoCatalogo = tipoCatalogo;


            switch (tipoCatalogo)
            {
                case "ocupacion":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblOcupaciones", IdTabla = "IdOcupacion" }, tipoCatalogo);
                    break;
                case "verificadordireccion":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblVerificadorDirecciones", IdTabla = "IdVerificadorDireccion" }, tipoCatalogo);
                    break;
                case "tipotelefono":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTipoTelefonos", IdTabla = "IdTipoTelefono" }, tipoCatalogo);
                    break;
                case "tiposexo":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTipoSexos", IdTabla = "IdTipoSexo" }, tipoCatalogo);
                    break;
                case "tasador":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblTasadores", IdTabla = "IdTasador" }, tipoCatalogo);
                    break;
                case "localizador":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblLocalizadores", IdTabla = "IdLocalizador" }, tipoCatalogo);
                    break;
                case "estadocivil":
                    data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblEstadosCiviles", IdTabla = "IdEstadoCivil" }, tipoCatalogo);
                    break;
                default:
                    break;
            }
            data.TipoCatalogo = tipoCatalogo;
            return View("Catalogos", data);
        }


        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Marca marca)
        {
            this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.MarcaInsUpd(marca);
            return RedirectToAction("CreateOrEdit");
        }
    }
}