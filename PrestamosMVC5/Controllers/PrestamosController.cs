using emtSoft.DAL;
using Microsoft.Ajax.Utilities;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class PrestamosController : ControllerBasePcp
    {
        public PrestamosController()
        {
            UpdViewBag_LoadCssAndJsGrp2(true);
            UpdViewBag_ShowSummaryErrorsTime(10);
        }
        // GET: Prestamos
        public ActionResult Index()
        {

            UpdViewBag_LoadCssAndJsForDatatable(true);
            var pres = BLLPrestamo.Instance.GetPrestamos(new PrestamosGetParams { idPrestamo = -1 });
            var prestamosList = pres.Select(pr => new PrestamosListVm { Fecha = pr.FechaEmisionReal.ToShortDateString(), idPrestamo = pr.IdPrestamo, PrestamoNumero = pr.PrestamoNumero, MontoPrestado = pr.MontoPrestado, NombreCliente = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { IdCliente = pr.IdCliente, IdNegocio = this.pcpUserIdNegocio }).FirstOrDefault().NombreCompleto });
            return View(prestamosList);
        }

        public ActionResult CreateOrEdit(int id = -1, string mensaje = "")
        {
            var cl = new Cliente() { Activo = false };
            var model = new PrestamoVm();
            model.Prestamo.IdPrestamo = id;
            setInitialValue(mensaje, model);
            if (id != -1)
            {
                // Buscar el cliente
                var searchResult = getPrestamoForUi(id);
                if (!searchResult.IsNull())
                {
                    TempData["Prestamo"] = searchResult;
                    model = searchResult;
                }
                else
                {
                    model.MensajeError = "Lo siento no encontramos datos para su peticion";
                }
            }
            pcpSetUsuarioAndIdNegocioTo(model.Prestamo);
            return View(model);
        }

        private static void setInitialValue(string mensaje, PrestamoVm model)
        {
            if (model.Prestamo.IdPrestamo <= 0) {
                model.Prestamo = Constant.prestamoDefault;
            }
            model.IncluirRenovacion = false;
            model.MensajeError = mensaje;
            model.MontoAPrestar = model.Prestamo.MontoPrestado.ToString(); ;
            model.LlevaGastoDeCierre = model.Prestamo.LlevaGastoDeCierre;
        }
        private static void setInitialValueForTesting(string mensaje, PrestamoVm model)
        {
            
            model.IncluirRenovacion = model.Prestamo.HabilitarRenovacion;
            model.MensajeError = mensaje;
            model.Prestamo.IdTasaInteres = 5;
            model.Prestamo.IdPeriodo = 6;
            model.Prestamo.CantidadDePeriodos = 5;
            model.Prestamo.CantidadDePeriodos = 5;
            model.MontoAPrestar = "10000.00";
            model.LlevaGastoDeCierre = model.Prestamo.LlevaGastoDeCierre;
        }

        [HttpPost]
        public ActionResult CreateOrEdit(PrestamoVm model)
        {
            ActionResult _actResult = null;
            //var prestamo = model.Prestamo;
            try
            {
                //var pr = new PrestamoBuilder(model.Prestamo);
                BLLPrestamo.Instance.InsUpdPrestamo(model.Prestamo);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            ModelState.AddModelError("", "Sus datos fueron guardados exitosamente, continue trabajando");
            _actResult = RedirectToAction("CreateOrEdit", new { id = -1 });
            return _actResult;
        }

        [HttpPost]
        public JsonResult GuardarPrestamo(Prestamo prestamo)
        {
            var message = string.Empty;
            Thread.Sleep(5000);
            try
            {
                BLLPrestamo.Instance.InsUpdPrestamo(prestamo);
                Response.StatusCode = 205;
                //Response.StatusCode = 500;
                message = "datos procesados exitosamente";
            }
            catch (Exception e)
            {
                message = "sus datos no fueron guardados ocurrio estos errores " + e.Message;
                Response.StatusCode = 500;
            }
            var data = new {  Prestamo = prestamo, Mensaje =  message };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, int idPeriodo)
        {
            var searchPeriodo = new PeriodoGetParams { idPeriodo = idPeriodo };
            var periodo = BLLPrestamo.Instance.GetPeriodos(searchPeriodo).FirstOrDefault();
            if (periodo == null)
            {
                Response.StatusCode = 400;
                var mensaje = "no se encontraron periodos para los parametros especificados";
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            };
            var data = BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(tasaInteresMensual, periodo);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClasificacionesQueLlevanGarantia()
        {
            var data = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(this.pcpUserIdNegocio).Select(item => item.IdClasificacion);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GenerarCuotas(infoGeneradorDeCuotas info, int idPeriodo, int idTipoAmortizacion)
        //infoGeneradorDeCuotas info)
        {
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
            info.TipoAmortizacion = (TiposAmortizacion)idTipoAmortizacion;
            info.Periodo = periodo;
            var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return Json(cuotas, JsonRequestBehavior.AllowGet);
        }
        private PrestamoVm getPrestamoForUi(int id)
        {
            PrestamoVm model = null;
            var searchResult = BLLPrestamo.Instance.GetPrestamoConDetalleForUIPrestamo(id);
            if (searchResult != null)
            {
                model = new PrestamoVm();
                model.Prestamo = searchResult.infoPrestamo;
                model.infoCliente = searchResult.infoCliente.InfoDelCliente;
                model.infoGarantia = searchResult.infoGarantias.FirstOrDefault().InfoVehiculo;
                model.LlevaGastoDeCierre = (model.Prestamo.InteresGastoDeCierre > 0);
            }
            return model;
        }
        public JsonResult getPrestamo(int idPrestamo)
        {
            JsonResult returnValue;
            if (idPrestamo > 0)
            {
                var search = new PrestamosGetParams { idPrestamo = idPrestamo, };
                pcpSetIdNegocioTo(search);
                var prestamo = BLLPrestamo.Instance.GetPrestamos(search).FirstOrDefault();
                if (prestamo != null)
                {
                    Response.StatusCode = 200;
                    returnValue = Json(prestamo, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.StatusCode = 400;
                    returnValue = Json(new { mensaje = "no se encontro prestamo para su solicitud" }, JsonRequestBehavior.AllowGet );
                }
            }
            else
            {
                Response.StatusCode = 400;
                returnValue = Json(new { mensaje = "parametro de busqueda incorrecto" }, JsonRequestBehavior.AllowGet);
            }
            return returnValue;
        }
    }
}