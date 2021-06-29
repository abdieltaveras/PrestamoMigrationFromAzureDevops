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
    public class CatalogoController : ControllerBasePrestamoWS
    {
        [HttpPost]
        public IActionResult Post([FromBody] Catalogo catalogo)
        {
           
            try
            {
                catalogo.Usuario = this.LoginName;
                catalogo.IdNegocio = 1;
                BLLPrestamo.Instance.InsUpdCatalogo(catalogo);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado" + e.Message);

            }
        }

        
        [HttpGet]
        public IEnumerable<Catalogo> Get([FromQuery] CatalogoGetParams getParams)
        {
        
            var result = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(getParams);
            return result;

        }
    }
}
