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
    public class ClasificacionController : BaseApiController
    {
        [HttpGet]
        public IEnumerable<Clasificacion> Get(string JsonGet = "")
        {
            var searchParam = JsonConvert.DeserializeObject<ClasificacionesGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetClasificaciones(searchParam);
            return result;
        }
        [HttpPost]
        public IHttpActionResult Post(Clasificacion clasificacion)
        {
            try
            {
                clasificacion.IdNegocio = 1;
                clasificacion.Usuario = this.LoginName;
                clasificacion.IdLocalidadNegocio = this.IdLocalidadNegocio;
                BLLPrestamo.Instance.InsUpdClasificacion(clasificacion);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

            //return RedirectToAction("CreateOrEdit");
        }
    }

}
