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

    public class TasaInteresController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<TasaInteres> Get([FromQuery] TasaInteresGetParams getParams)
        {
            //,int idTasaInteres = -1, int idLocalidadNegocio = -1, int activo = -1, string codigo = ""
            
           // var searchParam = new TasaInteresGetParams { Activo = activo, idTasaInteres = idTasaInteres, Codigo = codigo, IdLocalidadNegocio = idLocalidadNegocio };
            var result = BLLPrestamo.Instance.GetTasasDeInteres(getParams);
            return result;
        }
        [HttpGet]
        public async Task<IEnumerable<TasaInteresPorPeriodos>> GetTasaInteresPorPeriodo(int idTasaDeInteres, int idPeriodo)
        {
            var tasaDeInteres = BLLPrestamo.Instance.GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = IdNegocio, idTasaInteres = idTasaDeInteres }).FirstOrDefault();
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { idPeriodo = idPeriodo }).FirstOrDefault();
            var tasaDeInteresPorPeriodo = BLLPrestamo.Instance.CalcularTasaInteresPorPeriodos(tasaDeInteres.InteresMensual, periodo);
            var result = new List<TasaInteresPorPeriodos> { tasaDeInteresPorPeriodo };
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TasaInteres insUpdParam)
        {
            insUpdParam.IdLocalidadNegocio = 1;
            insUpdParam.Usuario = "luis";
            insUpdParam.IdNegocio = 1;
            var id = BLLPrestamo.Instance.InsUpdTasaInteres(insUpdParam);
            return Ok(id);
        }
        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblTasaInteres",
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
        
    }
}
