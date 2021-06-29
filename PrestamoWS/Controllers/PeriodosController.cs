using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using PrestamoBLL;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class PeriodosController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Periodo> Get([FromQuery]PeriodoGetParams getParams)
        {
            
            var result = BLLPrestamo.Instance.GetPeriodos(getParams);
            return result;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Periodo insUpdParam)
        {
            BLLPrestamo.Instance.InsUpdPeriodo(insUpdParam);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblPeriodo",
                IdRegistro = idRegistro.ToString()
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
        //public void PeriodoDelete(PeriodoDelParams delParam)
        //{
        //    DBPrestamo.ExecSelSP("spDelPeriodo", SearchRec.ToSqlParams(delParam));
        //}
    }
}
