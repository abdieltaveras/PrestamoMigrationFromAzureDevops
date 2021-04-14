using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrestamoBLL;
namespace WSPrestamo.Controllers
{
    public class TipoMorasController : BaseApiController
    {
        public IEnumerable<TipoMora> Get(int IdTipoMora = -1)
        {
            return BLLPrestamo.Instance.TiposMorasGet(new TipoMoraGetParams { IdTipoMora = IdTipoMora });
        }
        [HttpPost]
        public IHttpActionResult Post(TipoMora insUpdParam)
        {
           var id =   BLLPrestamo.Instance.TipoMoraInsUpd(insUpdParam);
            return Ok(id);
        }
        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
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

        //public IHttpActionResult TipoMoraCancel(TipoMoraDelParams delParam)
        //{
        //    //BllAcciones.CancelData<TipoMoraDelParams>(delParam, "spAnularTipoMora");
        //    BLLPrestamo.Instance.TipoMoraCancel(delParam);
        //    return Ok();
        //    //PrestamosDB.ExecSelSP("spAnularTipoMora", SearchRec.ToSqlParams(delParam));
        //}

        //public IHttpActionResult TipoMoraDelete(TipoMoraDelParams delParam)
        //{
        //    BLLPrestamo.Instance.TipoMoraDelete(delParam);
        //    return Ok();
        //}
    }
}
