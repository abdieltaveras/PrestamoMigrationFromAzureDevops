using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using WSPrestamo.Utilidades;
using System.Web.Http.Results;
using static PrestamoBLL.BLLPrestamo;

namespace WSPrestamo.Controllers
{
    /// <summary>
    /// Para registrar los pagos realizados por los Prestamos a los prestamos
    /// </summary>
    public class PrestamosController : BaseApiController
    {
        public IEnumerable<Prestamo> Get(int idPrestamo=-1)
        {
            var getParams = new PrestamosGetParams
            {
                idPrestamo = idPrestamo,
            };
            var data = BLLPrestamo.Instance.GetPrestamos(getParams);
            return data;
        }
        public IEnumerable<Prestamo> Get(DateTime fechaEmisionRealDesde,
            DateTime fechaEmisionRealHasta, int idPrestamo = -1, int idCliente = -1, int idGarantia = -1, int idLocalidadNegocio = -1, int idNegocio = -1)
        {
            var getParams = new PrestamosGetParams
            {
                fechaEmisionRealDesde = fechaEmisionRealDesde,
                fechaEmisionRealHasta = fechaEmisionRealHasta,
                idCliente = idCliente,
                idPrestamo = idPrestamo,
                idGarantia = idGarantia,
                idLocalidadNegocio = idLocalidadNegocio,
                IdNegocio = idNegocio
            };
            var data = BLLPrestamo.Instance.GetPrestamos(getParams);
            return data;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Prestamo Prestamo)
        {
            Prestamo.Usuario = this.LoginName;
            Prestamo.IdNegocio = this.IdNegocio;
            Prestamo.IdLocalidadNegocio = this.IdLocalidadNegocio;
            var validstate = ModelState.IsValid;
            try
            {
                var id = BLLPrestamo.Instance.InsUpdPrestamo(Prestamo);
                return Ok(id);
            }
            catch (Exception e)
            {
                throw new Exception("El Prestamo no pudo ser creado");
            }
        }
        /// <summary>
        /// Esto es para Borrar, anular un Prestamo
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IHttpActionResult Delete(int idPrestamo)
        {
            try
            {
                BLLPrestamo.Instance.AnularPrestamo(idPrestamo);
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        

        
        
        

        
        public TasaInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, int idPeriodo)
        {
            var searchPeriodo = new PeriodoGetParams { idPeriodo = idPeriodo };
            var periodo = BLLPrestamo.Instance.GetPeriodos(searchPeriodo).FirstOrDefault();
            if (periodo == null)
            {
                var mensaje = "no se encontraron periodos para los parametros especificados";
                throw new Exception("datos no encontrados");
            };
            var data = BLLPrestamo.Instance.CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);

            return data;
        }

        public IEnumerable<int> GetClasificacionesQueLlevanGarantia()
        {
            var data = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(-1).Select(item => item.IdClasificacion);
            return data;
        }

        [HttpGet]
        public IEnumerable<Cuota> GenerarCuotas(infoGeneradorDeCuotas info, int idPeriodo, int idTipoAmortizacion)
        //infoGeneradorDeCuotas info)
        {
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
            info.TipoAmortizacion = (TiposAmortizacion)idTipoAmortizacion;
            info.Periodo = periodo;
            var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            //var data = new { infoCuotas = info, IdPeriodo = idPeriodo, idTipoAmortizacion= idTipoAmortizacion };
            return cuotas;
        }
        
        
    }


}
