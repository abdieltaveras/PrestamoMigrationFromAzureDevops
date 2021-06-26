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

    public class TipoMorasController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<TipoMora> Get(int IdTipoMora = -1)
        {
            return BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdTipoMora = IdTipoMora });
        }
        [HttpPost]
        public IActionResult Post(TipoMora tipoMora)
        {
            tipoMora.Usuario = this.LoginName;
            tipoMora.IdLocalidadNegocio = this.IdLocalidadNegocio;
            tipoMora.IdNegocio = 1;
            var id =   BLLPrestamo.Instance.InsUpdTipoMora(tipoMora);
            return Ok(id);
        }
        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblTipoMora",
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

        //public IActionResult TipoMoraCancel(TipoMoraDelParams delParam)
        //{
        //    //BllAcciones.CancelData<TipoMoraDelParams>(delParam, "spAnularTipoMora");
        //    BLLPrestamo.Instance.TipoMoraCancel(delParam);
        //    return Ok();
        //    //PrestamosDB.ExecSelSP("spAnularTipoMora", SearchRec.ToSqlParams(delParam));
        //}

        //public IActionResult TipoMoraDelete(TipoMoraDelParams delParam)
        //{
        //    BLLPrestamo.Instance.TipoMoraDelete(delParam);
        //    return Ok();
        //}
    }
}
