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
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ModelosController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<ModeloWithMarca> Get([FromQuery] ModeloGetParams getParams)
        {
            return BLLPrestamo.Instance.GetModelos(getParams);  
        }
        
        

        [HttpPost]
        public IActionResult Post([FromBody] Modelo modelo)
        {
            modelo.Usuario = this.LoginName;
            modelo.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdModelo(modelo);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            throw new NotImplementedException();
            var elimParam = new AnularCatalogo
            {
                //NombreTabla = "tblModelos",
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
        }
    }
}
