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
        public IEnumerable<Periodo> Get([FromQuery] PeriodoGetParams getParams) => _Get(getParams);

        [HttpPost]
        public IActionResult Post([FromBody] Periodo insUpdParam) => _Post(insUpdParam);
        //[HttpGet]
        private IEnumerable<Periodo> _Get([FromQuery]PeriodoGetParams getParams)
        {

            var result = new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName).GetPeriodos(getParams);
            return result;
        }
        //[HttpPost]
        private IActionResult _Post([FromBody] Periodo insUpdParam)
        {
            new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName).InsUpdPeriodo(insUpdParam);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            throw new NotImplementedException();
            var elimParam = new AnularCatalogo
            {
                //NombreTabla = "tblPeriodo",
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
