using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

using PrestamoWS.Models;
using Microsoft.AspNetCore.Mvc;
using PcpUtilidades;
namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    /// <summary>
    /// para tomarlo como modelo y copiarlo para hacer los demas
    /// </summary>
    public class IngresosController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Ingreso> Get([FromQuery] IngresoGetParams getParams)
        {
            //
            //var getParam = new IngresoGetParams { IdIngreso= idIngreso,FechaDesde= FechaDesde, FechaHasta= fechaHasta,
            //                IdLocalidadNegocio= idLocalidadNegocio, NumeroTransaccion= numeroTransaccion, Users = this.LoginName} ;
            var data = BLLPrestamo.Instance.GetIngresos(getParams);

            return data;
        }

        [HttpGet]
        public IEnumerable<Ingreso> GetById(int id)
        {
            //
            var getParam = new IngresoGetParams
            {
                IdIngreso = id
            };
            var data = BLLPrestamo.Instance.GetIngresos(getParam);

            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ingreso ingreso)
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

        [HttpDelete]
        public IActionResult Anular(int idRegistro)
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
