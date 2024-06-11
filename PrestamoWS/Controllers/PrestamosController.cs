using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using static PrestamoBLL.BLLPrestamo;
using Newtonsoft.Json;
using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    /// <summary>
    /// Para registrar los pagos realizados por los Prestamos a los prestamos
    /// </summary>
    public class PrestamosController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Prestamo> GetById(int idPrestamo = -1) => _GetById(idPrestamo);

        [HttpGet]
        public IEnumerable<PrestamoConDetallesParaUIPrestamo> GetConDetallesForUi(int idPrestamo = -1) => _GetConDetallesForUi(idPrestamo);

        [HttpGet]
        public IEnumerable<Prestamo> Get([FromQuery] PrestamosGetParams getParams) => _Get(getParams);

        [HttpPost]
        public IActionResult Post([FromBody] Prestamo Prestamo) => _Post(Prestamo);


        private IEnumerable<Prestamo> _GetById(int idPrestamo=-1)
        {
            var getParams = new PrestamosGetParams { idPrestamo = idPrestamo };
            return _Get(getParams);
            //var data = new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).GetPrestamos(getParams); //BLLPrestamo.Instance.GetPrestamos(getParams);
            //return data;
        }
  
        private IEnumerable<PrestamoConDetallesParaUIPrestamo> _GetConDetallesForUi(int idPrestamo = -1)
        //public PrestamoConDetallesParaUIPrestamo GetConDetallesForUi(int idPrestamo = -1)
        {
            var prestamo = new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).GetPrestamoConDetalleForUIPrestamo(idPrestamo,true);
            //var prestamos = new List<PrestamoConDetallesParaUIPrestamo>();
            //prestamos.Add(prestamo);
            //var data = prestamo.ToJson<PrestamoConDetallesParaUIPrestamo>();
            //var data2 = data.ToType<PrestamoConDetallesParaUIPrestamo>();
            var result = new List<PrestamoConDetallesParaUIPrestamo> { prestamo };
            return result;
        }

        
        private IEnumerable<Prestamo> _Get([FromQuery] PrestamosGetParams getParams)
        {
            var data = new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).GetPrestamos(getParams);
            return data;
        }

        [HttpGet]
        public IActionResult GetPrestamoClienteUI([FromQuery] PrestamoClienteUIGetParam getParams)
        {
            try
            {

                var data = new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).GetPrestamoCliente(getParams);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private IActionResult _Post([FromBody] Prestamo prestamo)
        {
            prestamo.Usuario = this.LoginName;
            prestamo.IdNegocio = this.IdNegocio;
            prestamo.IdLocalidadNegocio = this.IdLocalidadNegocio;
            var validstate = ModelState.IsValid;
            try
            {
                var id = new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).InsUpdPrestamo(prestamo);
                //var id = BLLPrestamo.Instance.InsUpdPrestamo(prestamo);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Esto es para Borrar, anular un prestamo
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IActionResult Delete(int idPrestamo)
        {
            try
            {
                new PrestamoBLLC(this.IdLocalidadNegocio, this.LoginName).AnularPrestamo(idPrestamo);
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        [HttpGet]
        public TasasInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, int idPeriodo)
        {
            PeriodoBLL periodoBLL = new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName);

            var searchPeriodo = new PeriodoGetParams { idPeriodo = idPeriodo };
            var periodo = periodoBLL.GetPeriodos(searchPeriodo).FirstOrDefault();
            if (periodo == null)
            {
                var mensaje = "no se encontraron periodos para los parametros especificados";
                throw new Exception("datos no encontrados");
            };
            var data = new TasaInteresBLL(-1, "Luis Prueba").CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);

            return data;
        }


        [HttpGet]
        public IEnumerable<DebitoPrestamoConDetallesViewModel> GenerarCuotas2(string jsonInfoGenCuotas)
        {
            var infoGenCuotas = jsonInfoGenCuotas.ToType<InfoGeneradorDeCuotas>();
            infoGenCuotas.Usuario = this.LoginName;
            infoGenCuotas.IdLocalidadNegocio = this.IdLocalidadNegocio;
            infoGenCuotas.IdNegocio = this.IdNegocio;
            var cuotas = Generar_Cuotas(infoGenCuotas);
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return cuotas;
        }

        [HttpPost]
        public IEnumerable<DebitoPrestamoConDetallesViewModel> GenerarCuotas([FromBody] InfoGeneradorDeCuotas infoGenCuotas)
        {
            infoGenCuotas.Usuario = this.LoginName;
            infoGenCuotas.IdLocalidadNegocio = this.IdLocalidadNegocio;
            infoGenCuotas.IdNegocio = this.IdNegocio;
            var cuotas = Generar_Cuotas(infoGenCuotas);
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return cuotas;
        }
        private IEnumerable<DebitoPrestamoConDetallesViewModel> Generar_Cuotas(InfoGeneradorDeCuotas infoGenCuotas)
        {
            var montoGC = infoGenCuotas.MontoGastoDeCierre;
            // todo revisar 20240415
            var result =  MaestroDetalleDebitosBLL.Instance.ProyectarCuotasPrestamos(25, infoGenCuotas);
            return result;
        }

        [HttpGet]
        public IEnumerable<DebitoPrestamoConDetallesViewModel> GenerarCuotas(string jsonInfoGenCuotas, int idPeriodo, int idTipoAmortizacion)
        //infoGeneradorDeCuotas info)
        {
            //todo actualizarlo a la nueva forma con el nuevo objeto
            
            PeriodoBLL periodoBLL = new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName);
            var infoGenCuotas = jsonInfoGenCuotas.ToType<InfoGeneradorDeCuotas>();
            var periodo = periodoBLL.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
            infoGenCuotas.TipoAmortizacion = (TiposAmortizacion)idTipoAmortizacion;
            infoGenCuotas.IdPeriodo = idPeriodo;
            var cuotas = Generar_Cuotas(infoGenCuotas);
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return cuotas;
        }

        [HttpGet]
        public async Task<PrestamoConCalculos> Calcular(Prestamo prestamo)
        //infoGeneradorDeCuotas info)
        {
            PeriodoBLL periodoBLL = new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName);

            PrestamoConCalculos prconcalc = new PrestamoConCalculos();
            var clasificaciones = BLLPrestamo.Instance.GetClasificaciones(new ClasificacionesGetParams { IdNegocio = IdNegocio });
            var tiposMora = new TipoMoraBLL(this.IdLocalidadNegocio, this.LoginName).GetTiposMoras(new TipoMoraGetParams { IdNegocio = IdNegocio });

            //var tiposMora = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams
            //{
            //    IdNegocio = IdNegocio
            //});

            var tasasDeInteres = new TasaInteresBLL(-1, "Luis Prueba").GetTasasDeInteres(new TasaInteresGetParams
            {
                IdNegocio = IdNegocio
            });

            var periodos = periodoBLL.GetPeriodos(new PeriodoGetParams
            {
                IdNegocio = IdNegocio
            });
            prconcalc.SetServices(null, clasificaciones, tiposMora, tasasDeInteres, periodos);

            prconcalc.ActivateCalculos();
            var result = await prconcalc.CalcularPrestamo();
            return result;
        }

        
    }
}
