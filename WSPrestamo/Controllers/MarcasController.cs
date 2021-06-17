using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using Newtonsoft.Json;

namespace WSPrestamo.Controllers
{
    public class MarcasController : BaseApiController
    {
        public IEnumerable<Marca> Get(string JsonGet = "")
        {
            //Hay que agregar el controller
            var JsonResult = JsonConvert.DeserializeObject<MarcaGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetMarcas(JsonResult);
            return result;
            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public IHttpActionResult Post(Marca marca)
        {
            try
            {
                marca.Usuario = this.LoginName;
                marca.IdLocalidadNegocio = this.IdLocalidadNegocio;
                BLLPrestamo.Instance.InsUpdMarca(marca);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

            //return RedirectToAction("CreateOrEdit");
        }
        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblMarcas",
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
