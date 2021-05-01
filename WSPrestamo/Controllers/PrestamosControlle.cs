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

    }
}
