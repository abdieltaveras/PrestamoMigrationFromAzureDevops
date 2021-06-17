using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;

namespace WSPrestamo.Controllers
{
 
    /// <summary>
    /// para tomarlo como modelo y copiarlo para hacer los demas
    /// </summary>
    public class IngresosController : BaseApiController
    {
        
        public IEnumerable<Ingreso> Get(int idIngreso,int idLocalidadNegocio, DateTime FechaDesde, DateTime fechaHasta, string numeroTransaccion )
        {
            //
            var getParam = new IngresoGetParams { IdIngreso= idIngreso,FechaDesde= FechaDesde, FechaHasta= fechaHasta,
                            IdLocalidadNegocio= idLocalidadNegocio, NumeroTransaccion= numeroTransaccion, Usuario = this.LoginName} ;
            var data = BLLPrestamo.Instance.GetIngresos(getParam);

            return data;
        }

        public IEnumerable<Ingreso> Get(int id)
        {
            //
            var getParam = new IngresoGetParams
            {
                IdIngreso = id
            };
            var data = BLLPrestamo.Instance.GetIngresos(getParam);

            return data;
        }

        public IHttpActionResult Post([FromBody] Ingreso ingreso)
        {
            try
            {
                BLLPrestamo.Instance.InsUpdIngreso(ingreso);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser guardado");
            }
            //return RedirectToAction("CreateOrEdit");
        }

        
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
            
            };
            try
            {
                BLLPrestamo.Instance.AnularCatalogo(elimParam);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser anulado");
            }

            //return RedirectToAction("CreateOrEdit");
        }

        
    }
}
