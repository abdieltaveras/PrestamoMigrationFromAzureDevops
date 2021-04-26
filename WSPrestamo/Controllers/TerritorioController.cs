using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace WSPrestamo.Controllers
{
 
    public class TerritorioController : BaseApiController
    {
        [HttpGet]
        public IEnumerable<Territorio> Get()
        {
            var result = BLLPrestamo.Instance.TerritoriosGet(new TerritorioGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
            return result;
            //return View("CreateOrEdit", datos);
        }
        [HttpGet]
        public IEnumerable<Territorio> GetDivisionesTerritoriales()
        {
            var result = BLLPrestamo.Instance.TerritorioDivisionesTerritorialesGet(new TerritorioGetParams() { IdNegocio = 1 });
            return result;
            //return View("CreateOrEdit", datos);
        }
       
        [HttpGet]
        public IEnumerable<Territorio> BuscarComponenteDeDivision()
        {
            string IdDivision = "";
            IdDivision = "2";
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorial = int.Parse(IdDivision) });

            return territorios;
            //return JsonConvert.SerializeObject(territorios);
        }
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string BuscarComponenteDeDivision2(string IdDivision)
        {
            
            //IdDivision = "2";
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorial = int.Parse(IdDivision) });

            //return territorios;
            return JsonConvert.SerializeObject(territorios);
        }
        //public IEnumerable<Territorio> GetByParam(int idColor, int idLocalidadNegocio)
        //{
        //    var search = new ColorGetParams { IdColor = idColor, IdLocalidadNegocio = idLocalidadNegocio };
        //    var result = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
        //    return result;
        //    //return View("CreateOrEdit", datos);
        //}

        [HttpPost]
        public IHttpActionResult Post(Territorio territorio)
        {
            territorio.Usuario = this.LoginName;
            territorio.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.TerritorioInsUpd(territorio);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
