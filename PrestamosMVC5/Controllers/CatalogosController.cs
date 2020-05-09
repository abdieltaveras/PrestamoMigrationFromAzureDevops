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
    public class CatalogosController : ControllerBasePcp
    {
        public CatalogosController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
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
 
        public string searchInCatalogo1(string textToSearch, string tableName)
        {
            switch (tableName)
            {
                case "tblOcupaciones":
                    return JsonConvert.SerializeObject( BLLPrestamo.Instance.catalogoSearch<Ocupacion>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblVerificadorDirecciones":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<VerificadorDireccion>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblTipoTelefonos":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<TipoTelefono>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblTipoSexos":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<TipoSexo>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblTasadores":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<TipoTelefono>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblLocalizadores":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<Localizador>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                case "tblEstadosCiviles":
                    return JsonConvert.SerializeObject(BLLPrestamo.Instance.catalogoSearch<EstadoCivil>(new SearchCatalogoParams { TextToSearch = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio }));
                default:
                    return null;// throw new Exception($"La tabla {searchParam.NombreTabla} , no se encontro en ninguna eleccion para ejecutar una consulta de datos");
            }
        }


        //public List<T> searchInCatalogo<T>(string textToSearch, string tableName) where T : class
        //{
        //    List<T> catalogo;
        //    //if (textToSearch.Length >= BUSCAR_A_PARTIR_DE)
        //    //{
        //    catalogo = BLLPrestamo.Instance.catalogoSearch<T>(new SearchCatalogoParams { TextToSearch = textToSearch, IdNegocio = pcpUserIdNegocio, TableName = tableName });

        //    switch (tableName)
        //    {
        //        case "tblOcupaciones":
        //            //return BLLPrestamo.Instance.catalogoSearch<T>(new SearchCatalogoParams { Text = textToSearch, TableName = tableName, IdNegocio = pcpUserIdNegocio });
        //        //case "tblVerificadorDirecciones":
        //        //    return BllAcciones.GetData<VerificadorDireccion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        //case "tblTipoTelefonos":
        //        //    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        //case "tblTipoSexos":
        //        //    return BllAcciones.GetData<TipoSexo, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        //case "tblTasadores":
        //        //    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        //case "tblLocalizadores":
        //        //    return BllAcciones.GetData<Localizador, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        //case "tblEstadosCiviles":
        //        //    return BllAcciones.GetData<EstadoCivil, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
        //        default:
        //            return null;// throw new Exception($"La tabla {searchParam.NombreTabla} , no se encontro en ninguna eleccion para ejecutar una consulta de datos");
        //    }


        //    //}
        //    return JsonConvert.SerializeObject<T>(catalogo, Formatting.Indented);
        //}
    }
}