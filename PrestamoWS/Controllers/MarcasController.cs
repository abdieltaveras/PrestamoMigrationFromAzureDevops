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

    public class MarcasController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Marca> Get(string JsonGet = "")
        {
            //Hay que agregar el controller
            var JsonResult = JsonConvert.DeserializeObject<MarcaGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetMarcas(JsonResult);
            return result;
            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public IActionResult Post(Marca marca)
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
        public IActionResult Anular(int idRegistro)
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
