using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

using PrestamoWS.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClasificacionController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Clasificacion> Get(string JsonGet = "")
        {
            var searchParam = JsonConvert.DeserializeObject<ClasificacionesGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetClasificaciones(searchParam);
            return result;
        }

        [HttpGet]
        public IEnumerable<int> GetClasificacionesQueLlevanGarantia()
        {
            var data = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(-1).Select(item => item.IdClasificacion);
            return data;
        }
        [HttpPost]
        public IActionResult Post(Clasificacion clasificacion)
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
