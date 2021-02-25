using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamoWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GarantiasController : Controller
    {
        int BUSCAR_A_PARTIR_DE = 2;
        [HttpGet]
        public IEnumerable<GarantiaConMarcaYModelo> GetIndex()
        {
            //UpdViewBag_LoadCssAndJsForDatatable(true);
            var garantias = GetGarantias();
            return garantias;
            //ActionResult _actResult = View(garantias);
            //return _actResult;
        }
        [HttpGet]
        public void SaveGet( GarantiaVM garantia)
        {
            //int id = -1, List<ResponseMessage> ListaMensajes = null, GarantiaVM garantia = null
            //Faltan todos los ID Negocios
            GarantiaVM datos = garantia == null ? new GarantiaVM() : garantia;

            datos.ListaTipos = new SelectList(BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams { IdNegocio = 1 }), "IdTipoGarantia", "Nombre");
            datos.ListaTiposReal = BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams { IdNegocio = 1 });
            datos.ListaMarcas = new SelectList(BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 }), "IdMarca", "Nombre");
            datos.ListaModelos = new SelectList(BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdNegocio = 1 }), "IdModelo", "Nombre");
            datos.ListaColores = new SelectList(BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1 }), "IdColor", "Nombre");
            datos.Garantia = new Garantia();

            //datos.ListaMensajes = TempData["list"] as List<ResponseMessage>;
            //Comentado porque no permite mas de dos parametros
            //if (id != -1)
            //{
                var datosGarantia = GetGarantiaByIdGarantia(1).DataList.FirstOrDefault();
                datos.Garantia = datosGarantia;
                datos.Garantia.DetallesJSON = datosGarantia.Detalles.ToType<DetalleGarantia>();
                TempData["Garantia"] = datosGarantia;
            //}


            ////*******Imagenes Garantia****//
            //var garantiaTempData = GetValueFromTempData<Garantia>("Garantia");
            //var imagen1GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image1PreviewValue);
            //var imagen2GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image2PreviewValue);
            //var imagen3GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image3PreviewValue);
            //var imagen4GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantia.image4PreviewValue);
            //garantia.Garantia.Imagen1FileName = GeneralUtils.GetNameForFile(imagen1GarantiaFileName, garantia.image1PreviewValue, garantiaTempData.Imagen1FileName);
            //garantia.Garantia.Imagen2FileName = GeneralUtils.GetNameForFile(imagen2GarantiaFileName, garantia.image2PreviewValue, garantiaTempData.Imagen2FileName);

            //garantia.Garantia.Imagen3FileName = GeneralUtils.GetNameForFile(imagen3GarantiaFileName, garantia.image3PreviewValue, garantiaTempData.Imagen3FileName);
            //garantia.Garantia.Imagen4FileName = GeneralUtils.GetNameForFile(imagen4GarantiaFileName, garantia.image4PreviewValue, garantiaTempData.Imagen4FileName);
            //return View(datos);
        }

        //[HttpPost]
        //public void SavePost(Garantia garantia, GarantiaVM garantiavm = null)
        //{
        //    //string errol = string.Empty;
        //    //// Convertir detalles a JSON y crear el objeto de garantia para insertar / modificar en la tabla
        //    //string JsonDetalesGarantia = JsonConvert.SerializeObject(garantia.DetallesJSON);
        //    //garantia.Detalles = garantia.DetallesJSON.ToJson();
        //    //pcpSetUsuarioAndIdNegocioTo(garantia);

        //    //List<ResponseMessage> listaMensajes = new List<ResponseMessage>();


        //    //var garantiaTempData = GetValueFromTempData<Garantia>("Garantia");
        //    //var imagen1GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image1PreviewValue);
        //    //var imagen2GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image2PreviewValue);
        //    //var imagen3GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image3PreviewValue);
        //    //var imagen4GarantiaFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForGarantia), garantiavm.image4PreviewValue);
        //    //garantia.Imagen1FileName = GeneralUtils.GetNameForFile(imagen1GarantiaFileName, garantiavm.image1PreviewValue, garantiaTempData.Imagen1FileName);
        //    //garantia.Imagen2FileName = GeneralUtils.GetNameForFile(imagen2GarantiaFileName, garantiavm.image2PreviewValue, garantiaTempData.Imagen2FileName);
        //    //garantia.Imagen3FileName = GeneralUtils.GetNameForFile(imagen3GarantiaFileName, garantiavm.image3PreviewValue, garantiaTempData.Imagen3FileName);
        //    //garantia.Imagen4FileName = GeneralUtils.GetNameForFile(imagen4GarantiaFileName, garantiavm.image4PreviewValue, garantiaTempData.Imagen4FileName);
        //    ////if (!ModelState.IsValid)
        //    ////{
        //    ////    foreach (var errors in ModelState.Values)
        //    ////    {
        //    ////        foreach (var error in errors.Errors)
        //    ////        {
        //    ////            listaMensajes.Add(new ResponseMessage()
        //    ////            {
        //    ////                Tipo = ResponseMessage.TYPE_ERROR,
        //    ////                Mensaje = error.ErrorMessage
        //    ////            });
        //    ////        }
        //    ////    }
        //    ////    TempData["list"] = listaMensajes;
        //    ////    return RedirectToAction("CreateOrEdit", new { @ListaMensajes = listaMensajes, @garantia = garantia });
        //    ////}

        //    //try
        //    //{
        //    //    BLLPrestamo.Instance.InsUpdGarantia(garantia);
        //    //}
        //    //catch (Exception err)
        //    //{
        //    //    errol = err.Message;
        //    //}

        //    //return RedirectToAction("Index", new { listaMensajes });
        //}
        [HttpGet]
        public IEnumerable<Garantia> GetGarantiasByName(string searchToText)
        {
            IEnumerable<Garantia> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantias(new BuscarGarantiaParams { Search = searchToText, IdNegocio = 1 });
            }
            return garantias;
        }
        [HttpGet]
        public IEnumerable<Garantia> GetGarantiasWithPrestamos(string searchToText)
        {
            IEnumerable<Garantia> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = searchToText, IdNegocio = 1 });
            }
            return garantias;
        }
        [HttpGet]
        public string GetLocalidadGarantias(int IdLocalidad, int IdNegocio)
        {
            List<string> localidad = null;
            localidad = BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = IdLocalidad, IdNegocio = IdNegocio }).ToList();
            return localidad[0];
        }
        [HttpGet]
        private IEnumerable<GarantiaConMarcaYModelo> GetGarantias()
        {
            var getGarantiasParams = new GarantiaGetParams();
            //this.pcpSetUsuarioAndIdNegocioTo(getGarantiasParams);
            var garantias = BLLPrestamo.Instance.GetGarantias(getGarantiasParams);
            foreach (var item in garantias)
            {
                item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>();
            }
            return garantias;
        }
        //private SeachResult<GarantiaConMarcaYModelo> GetGarantias()
        //{
        //    var getGarantiasParams = new GarantiaGetParams();
        //    this.pcpSetUsuarioAndIdNegocioTo(getGarantiasParams);
        //    var garantias = BLLPrestamo.Instance.GetGarantias(getGarantiasParams);
        //    var result = new SeachResult<GarantiaConMarcaYModelo>(garantias);
        //    return result;
        //}
        [HttpGet]
        private SeachResult<Garantia> GetGarantiaByIdGarantia(int id)
        {
            var searchGarantia = new GarantiaGetParams { IdGarantia = id };
            //pcpSetUsuarioAndIdNegocioTo(searchGarantia);
            var garantias = BLLPrestamo.Instance.GetGarantias(searchGarantia);
            var result = new SeachResult<Garantia>(garantias);
            return result;
        }


        public class SeachResult<T>
        {
            public bool DatosEncontrados { get; private set; } = false;
            public IEnumerable<T> DataList
            {
                get;
                private set;
            }

            public SeachResult(IEnumerable<T> data)
            {
                this.DatosEncontrados = (data != null & data.Count() > 0);
                if (DatosEncontrados)
                    DataList = data;
                else
                    DataList = new List<T>();
            }
        }
    }
}
