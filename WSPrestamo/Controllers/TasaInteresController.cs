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
    public class TasaInteresController : BaseApiController
    {
        public IEnumerable<TasaInteres> Get(int idTasaInteres, int idLocalidadNegocio, int activo, string codigo)
        {
            var searchParam = new TasaInteresGetParams { Activo = activo, idTasaInteres = idTasaInteres, Codigo = codigo, IdLocalidadNegocio = idLocalidadNegocio };
            var result = BLLPrestamo.Instance.GetTasasDeInteres(searchParam);
            return result;
        }

        [HttpPost]
        public IHttpActionResult TasaInteresInsUpd(TasaInteres insUpdParam)
        {
            var id = BLLPrestamo.Instance.InsUpdTasaInteres(insUpdParam);
            return Ok(id);
        }
        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
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
        //public void TasaInteresDelete(TasaInteresDelParams delParam)
        //{
        //    DBPrestamo.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        //}
    }
}
