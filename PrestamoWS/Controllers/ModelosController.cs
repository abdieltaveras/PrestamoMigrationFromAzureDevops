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
    public class ModelosController : ControllerBasePrestamoWS
    {
        //[HttpGet]
        public IEnumerable<ModeloWithMarca> Get(string JsonGet = "")
        {
            var jsonResult = JsonConvert.DeserializeObject<ModeloGetParams>(JsonGet);
            return BLLPrestamo.Instance.GetModelos(jsonResult);
           
        }
        
        //public IEnumerable<Modelo> Get(int idMarca)
        //{
        //    IEnumerable<Modelo> modelos = null;
        //    modelos = BLLPrestamo.Instance.GetModelosByMarca(new ModeloGetParams { IdMarca = idMarca, IdNegocio = 1 });
        //    return modelos;
        //}

        [HttpPost]
        public IActionResult Post(Modelo modelo)
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
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblModelos",
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
