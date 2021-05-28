using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using WSPrestamo.Utilidades;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace WSPrestamo.Controllers
{
    public class CatalogoController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody] Catalogo catalogo)
        {
           
            try
            {
                catalogo.Usuario = this.LoginName;
                BLLPrestamo.Instance.InsUpdCatalogo(catalogo);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado" + e.Message);

            }
        }

        //[HttpGet]
        //public IEnumerable<Catalogo> Get(string JsonGet = "")
        //{
        //    var JsonResult = JsonConvert.DeserializeObject<CatalogoGetParams>(JsonGet);
        //    var result = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(JsonResult);
        //    return result;
        //    //return View("CreateOrEdit", datos);
        //}
        [HttpGet]
        public IEnumerable< string> Get(string JsonGet = "")
        {
            List<string> ls = new List<string>();
            var JsonResult = JsonConvert.DeserializeObject<CatalogoGetParams>(JsonGet);
            var result = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(JsonResult);
            var d =  JsonConvert.SerializeObject( result);
            ls.Add(d);
            return ls;
            //return View("CreateOrEdit", datos);
        }
    }
}
