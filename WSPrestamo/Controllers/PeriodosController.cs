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
    public class PeriodosController : BaseApiController
    {
        public IEnumerable<Periodo> Get(int idPeriodo=-1, int idLocalidadNegocio=-1, int activo=-1, string codigo="")
        {
            var searchParam = new PeriodoGetParams { Activo = activo, idPeriodo = idPeriodo, Codigo = codigo, IdLocalidadNegocio = idLocalidadNegocio };
            var result = BLLPrestamo.Instance.GetPeriodos(searchParam);
            return result;
        }
        [HttpPost]
        public IHttpActionResult PeriodoInsUpd(Periodo insUpdParam)
        {
            BLLPrestamo.Instance.InsUpdPeriodo(insUpdParam);
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
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
