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
 
    /// <summary>
    /// para tomarlo como modelo y copiarlo para hacer los demas
    /// </summary>
    public class ModeloGuiaController : BaseApiController
    {
        public IEnumerable<Color> GetAll()
        {
            //
            var data = BLLPrestamo.Instance.GetColores(new ColorGetParams());
            return data;
        }

        public IEnumerable<Color> Get(int idColor)
        {
            var data = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdColor = idColor });
            return data;
        }

        [HttpPost]
        public IHttpActionResult Post(Color color)
        {
            try
            {
                BLLPrestamo.Instance.InsUpdColor(color);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser guardado");
            }
            //return RedirectToAction("CreateOrEdit");
        }

        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new DelCatalogo {
                NombreTabla="tblColor",
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
