using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CatalogoController : BaseApiController
    {
        [HttpPost]
        public  ActionResult Post([FromBody] Catalogo catalogo)
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
        public IEnumerable<string> Get(string JsonGet = "")
        {

            List<string> ls = new List<string>();
            var JsonResult = JsonConvert.DeserializeObject<CatalogoGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(JsonResult);
            var d = JsonConvert.SerializeObject(result);
            ls.Add(d);
            return ls;
            //return View("CreateOrEdit", datos);
        }
        [HttpGet]
        public IEnumerable<Catalogo> Get2(string JsonGet = "")
        {
            var JsonResult = JsonConvert.DeserializeObject<CatalogoGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(JsonResult);
            return result;
        }
    }
    
}
