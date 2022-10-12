using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using PrestamoBLL;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class EstatusController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Estatus> Get([FromQuery] EstatusGetParams getParams)
        {
            var result = new EstatusBLL(this.IdLocalidadNegocio,this.LoginName).Get(getParams);
            return result;
        }
        //[HttpGet]
        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    var result = new EstatusBLL(this.IdLocalidadNegocio, this.LoginName).ListEstatus();
        //    return result;
        //}
        //[HttpGet]
        //public async Task<IEnumerable<TasaInteresPorPeriodos>> GetTasaInteresPorPeriodo(int idTasaDeInteres, int idPeriodo)
        //{
        //    PeriodoBLL periodoBLL = new PeriodoBLL(this.IdLocalidadNegocio, this.LoginName);

        //    var tasaDeInteres = new TasaInteresBLL(this.IdLocalidadNegocio, this.LoginName).GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = IdNegocio, idTasaInteres = idTasaDeInteres }).FirstOrDefault();
        //    var periodo = periodoBLL.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
        //    var tasaDeInteresPorPeriodo = new TasaInteresBLL(this.IdLocalidadNegocio, this.LoginName).CalcularTasaInteresPorPeriodos(tasaDeInteres.InteresMensual, periodo);
        //    var result = new List<TasaInteresPorPeriodos> { tasaDeInteresPorPeriodo };
        //    return result;
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Estatus insUpdParam)
        {
            //insUpdParam.IdLocalidadNegocio = 1;
            var id = new EstatusBLL(this.IdLocalidadNegocio, this.LoginName).InsUpd(insUpdParam);
            return Ok(id);
        }
        //[HttpDelete]
        //public IActionResult Anular(int idRegistro)
        //{
        //    throw new NotImplementedException();
        //    // llenar el parametro de borrado si lo requier el metodo
        //    var elimParam = new AnularCatalogo
        //    {
        //        //NombreTabla = "tblTasaInteres",
        //        IdRegistro = idRegistro.ToString()
        //    };
        //    try
        //    {
        //        BLLPrestamo.Instance.AnularCatalogo(elimParam);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Registro no pudo ser anulado");
        //    }

        //    //return RedirectToAction("CreateOrEdit");
        //}
        
    }
}
