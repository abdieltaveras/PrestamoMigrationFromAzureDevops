using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Exceptions;
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
    public class GarantiasController : ControllerBasePcp
    {
        const int BUSCAR_A_PARTIR_DE = 2;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(List<ResponseMessage> ListaMensajes = null, GarantiaVM garantia = null)
        {
            GarantiaVM datos = garantia == null ? new GarantiaVM() : garantia;

            datos.ListaTipos = new SelectList( BLLPrestamo.Instance.TiposGet(new TipoGetParams { IdNegocio = 1 }), "IdTipo", "Nombre" );
            datos.ListaTiposReal =  BLLPrestamo.Instance.TiposGet(new TipoGetParams { IdNegocio = 1 });
            //datos.ListaMarcasReal = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });
            datos.ListaMarcas = new SelectList( BLLPrestamo.Instance.MarcasGet(new MarcaGetParams { IdNegocio = 1 }), "IdMarca", "Nombre" );
            datos.ListaModelos = new SelectList( BLLPrestamo.Instance.ModelosGet(new ModeloGetParams { IdNegocio = 1 }), "IdModelo", "Nombre" );
            datos.ListaColores = new SelectList(BLLPrestamo.Instance.ColoresGet(new ColorGetParams { IdNegocio = 1 }), "IdColor", "Nombre");

            datos.Garantia = new Garantia();

            datos.ListaMensajes = TempData["list"] as List<ResponseMessage>;

            return View(datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Garantia garantia)
        {
            // Convertir detalles a JSON y crear el objeto de garantia para insertar / modificar en la tabla
            string JsonDetalesGarantia = JsonConvert.SerializeObject(garantia.DetallesJSON);
            garantia.Detalles = garantia.DetallesJSON.ToJson();
            pcpSetUsuarioAndIdNegocioTo(garantia);

            List<ResponseMessage> listaMensajes = new List<ResponseMessage>();
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
                BLLPrestamo.Instance.GarantiaInsUpd(garantia);
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
                garantias = BLLPrestamo.Instance.GarantiaSearch(new BuscarGarantiaParams { Search = searchToText, IdNegocio = pcpUserIdNegocio });                
                
            }
            return JsonConvert.SerializeObject(garantias);
        }

        public string BuscarLocalidadGarantias(int IdLocalidad, int IdNegocio)
        {
            List<string> localidad = null;
            localidad = BLLPrestamo.Instance.LocalidadSearchName(new BuscarNombreLocalidadParams { IdLocalidad = IdLocalidad, IdNegocio = IdNegocio });
            return localidad[0];
        }

    }
}