using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Exceptions;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class GarantiasController : ControllerBasePcp
    {
        int BUSCAR_A_PARTIR_DE = 2;
        public GarantiasController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(List<ResponseMessage> ListaMensajes = null, GarantiaVM garantia = null)
        {
            GarantiaVM datos = garantia == null ? new GarantiaVM() : garantia;

            datos.ListaTipos = new SelectList( BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams { IdNegocio = pcpUserIdNegocio }), "IdTipo", "Nombre" );
            datos.ListaTiposReal =  BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams { IdNegocio = pcpUserIdNegocio });
            datos.ListaMarcas =  BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = pcpUserIdNegocio });
            //datos.ListaModelos = new SelectList( BLLPrestamo.Instance.ModelosGet(new ModeloGetParams { IdNegocio = 1 }), "IdModelo", "Nombre" );
            datos.ListaColores = new SelectList(BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = pcpUserIdNegocio }), "IdColor", "Nombre");

            datos.Garantia = new Garantia();

            datos.ListaMensajes = TempData["list"] as List<ResponseMessage>;
            //*******Imagenes Garantia****//
            var garantiaTempData = GetValueFromTempData<Garantia>("Garantia");
            var imagen1GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image1PreviewValue);
            var imagen2GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image2PreviewValue);
            var imagen3GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image3PreviewValue);
            var imagen4GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image4PreviewValue);
            garantia.Garantia.Imagen1FileName = GeneralUtils.GetNameForFile(imagen1GarantiaFileName, garantia.image1PreviewValue, garantiaTempData.Imagen1FileName);
            garantia.Garantia.Imagen2FileName = GeneralUtils.GetNameForFile(imagen2GarantiaFileName, garantia.image2PreviewValue, garantiaTempData.Imagen2FileName);
            
            garantia.Garantia.Imagen3FileName = GeneralUtils.GetNameForFile(imagen3GarantiaFileName, garantia.image3PreviewValue, garantiaTempData.Imagen3FileName);
            garantia.Garantia.Imagen4FileName = GeneralUtils.GetNameForFile(imagen4GarantiaFileName, garantia.image4PreviewValue, garantiaTempData.Imagen4FileName);
            return View(datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Garantia garantia,GarantiaVM garantiavm =null)
        {
            // Convertir detalles a JSON y crear el objeto de garantia para insertar / modificar en la tabla
            string JsonDetalesGarantia = JsonConvert.SerializeObject(garantia.DetallesJSON);
            garantia.Detalles = garantia.DetallesJSON.ToJson();
            pcpSetUsuarioAndIdNegocioTo(garantia);

            List<ResponseMessage> listaMensajes = new List<ResponseMessage>();

            //*******Imagenes Garantia****//
            var garantiaTempData = GetValueFromTempData<Garantia>("Garantia");
            var imagen1GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image1PreviewValue);
            var imagen2GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image2PreviewValue);
            var imagen3GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image3PreviewValue);
            var imagen4GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image4PreviewValue);
            garantia.Imagen1FileName = GeneralUtils.GetNameForFile(imagen1GarantiaFileName, garantiavm.image1PreviewValue, garantiaTempData.Imagen1FileName);
            garantia.Imagen2FileName = GeneralUtils.GetNameForFile(imagen2GarantiaFileName, garantiavm.image2PreviewValue, garantiaTempData.Imagen2FileName);
            garantia.Imagen3FileName = GeneralUtils.GetNameForFile(imagen3GarantiaFileName, garantiavm.image3PreviewValue, garantiaTempData.Imagen3FileName);
            garantia.Imagen4FileName = GeneralUtils.GetNameForFile(imagen4GarantiaFileName, garantiavm.image4PreviewValue, garantiaTempData.Imagen4FileName);
            //if (!ModelState.IsValid)
            //{
            //    foreach (var errors in ModelState.Values)
            //    {
            //        foreach (var error in errors.Errors)
            //        {
            //            listaMensajes.Add(new ResponseMessage()
            //            {
            //                Tipo = ResponseMessage.TYPE_ERROR,
            //                Mensaje = error.ErrorMessage
            //            });
            //        }
            //    }
            //    TempData["list"] = listaMensajes;
            //    return RedirectToAction("CreateOrEdit", new { @ListaMensajes = listaMensajes, @garantia = garantia });
            //}

            try
            {
                BLLPrestamo.Instance.InsUpdGarantia(garantia);
            }
            catch(Exception err)
            {
            }

            return RedirectToAction("CreateOrEdit", new { listaMensajes });
        }

        public string BuscarGarantias(string searchToText)
        {
            IEnumerable<Garantia> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantias(new BuscarGarantiaParams { Search = searchToText, IdNegocio = pcpUserIdNegocio });
            }
            return JsonConvert.SerializeObject(garantias);
        }
        public string BuscarGarantiasConPrestamos(string searchToText)
        {
            IEnumerable<Garantia> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = searchToText, IdNegocio = pcpUserIdNegocio });
            }
            return JsonConvert.SerializeObject(garantias);
        }

        public string BuscarLocalidadGarantias(int IdLocalidad, int IdNegocio)
        {
            List<string> localidad = null;
            localidad = BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = IdLocalidad, IdNegocio = IdNegocio }).ToList();
            return localidad[0];
        }

        //[HttpPost]
        //public ActionResult SubirImagenes(HttpPostedFileBase[] files)
        //{

        //    //Ensure model state is valid  
        //    if (ModelState.IsValid)
        //    {   //iterating through multiple file collection   
        //        foreach (HttpPostedFileBase file in files)
        //        {
        //            //Checking file is available to save.  
        //            if (file != null)
        //            {
        //                var InputFileName = Path.GetFileName(file.FileName);
        //                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
        //                //Save file to server folder  
        //                file.SaveAs(ServerSavePath);
        //                //assigning file uploaded status to ViewBag for showing message to user.  
        //                ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
        //            }

        //        }
        //    }
        //    return View();
        //}

    }
}