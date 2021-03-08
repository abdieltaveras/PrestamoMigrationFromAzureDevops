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
    /// Para registrar los pagos realizados por los clientes a los prestamos
    /// </summary>
    public class ModeloGuiaController : BaseApiController
    {
        /// <summary>
        /// este metodo es valido usarlo en los catalogos, cuyas tablas no 
        /// tendran tanta informacion, pero no en tablas con mas de 100 registros.
        /// nadie suele estar viendo todas las operaciones registrada, normalmente se filtran
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Color> GetAll()
        {
            
            var getParam = new ColorGetParams();
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

        [HttpPost]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblColor",
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
