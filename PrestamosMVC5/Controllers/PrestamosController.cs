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
            var prestamosList = pres.Select(pr => new PrestamosListVm { Fecha = pr.FechaEmisionReal.ToShortDateString(), idPrestamo = pr.IdPrestamo, PrestamoNumero = pr.PrestamoNumero, MontoPrestado = pr.MontoPrestado, NombreCliente = BLLPrestamo.Instance.ClientesGet(new ClienteGetParams { IdCliente = pr.IdCliente, IdNegocio = this.pcpUserIdNegocio }).FirstOrDefault().NombreCompleto });
            return View(prestamosList);
        }

        public ActionResult CreateOrEdit(int id = -1, string mensaje = "")
        {
            var cl = new Cliente() { Activo = false };
            var model = new PrestamoVm();
            model.IncluirRenovacion = false;
            model.MensajeError = mensaje;
            model.Prestamo.IdTasaInteres = 5;
            model.Prestamo.IdPeriodo = 6;
            model.Prestamo.CantidadDePeriodos = 5;
            if (id != -1)
            {
                // Buscar el cliente
                var searchResult = getPrestamo(id);
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

        public JsonResult SavePrestamo(PrestamoVm prestamovm)
        {
            Response.StatusCode = 200;
            var data = prestamovm.Prestamo.ToJson();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SavePrestamo2(Prestamo prestamo)
        {
            Response.StatusCode = 200;
            var data = prestamo.ToJson();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularTasaInteresPorPeriodo(decimal tasaInteresMensual, int idPeriodo)
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

        public JsonResult ClasificacionesQueLlevanGarantia()
        {
            var data = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(1).Select(item => item.IdClasificacion);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GenerarCuotas(infoGeneradorDeCuotas info, int idPeriodo, int idTipoAmortizacion)
        //infoGeneradorDeCuotas info)
        {
            var info2 = new infoGeneradorDeCuotas(TiposAmortizacion.Amortizable_cuotas_fijas);
            //info.Periodo
            //info.CantidadDePeriodos
            //info.TasaDeInteresPorPeriodo
            //info.TipoAmortizacion
            //info.MontoCapital
            //info.MontoGastoDeCierre
            //info.CargarInteresAlGastoDeCierre
            //info.FechaEmisionReal
            //info.FechaInicioPrimeraCuota
            //info.AcomodarFechaALasCuotas
            //var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            //var data = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(1).Select(item => item.IdClasificacion);
            
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
            info.TipoAmortizacion = (TiposAmortizacion)idTipoAmortizacion;
            info.Periodo = periodo;
            var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return Json(cuotas, JsonRequestBehavior.AllowGet);
        }
        private PrestamoVm getPrestamo(int id)
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
    }
}