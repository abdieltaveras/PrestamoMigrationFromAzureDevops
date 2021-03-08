using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;

namespace WSPrestamo.Controllers
{
    public class MarcasController : BaseApiController
    {
        public IEnumerable<Marca> GetAll()
        {
            MarcaVM datos = new MarcaVM();
            //Hay que agregar el controller
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });
            return datos.ListaMarcas;
            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public IHttpActionResult Post(Marca marca)
        {
            //marca.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.InsUpdMarca(marca);
            return Ok();
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
