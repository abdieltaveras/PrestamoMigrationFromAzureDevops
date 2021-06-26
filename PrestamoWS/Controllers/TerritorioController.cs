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

    public class TerritorioController : ControllerBasePrestamoWS
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
            territorios = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorialPadre = int.Parse(IdDivision) });

            return territorios;
            //return JsonConvert.SerializeObject(territorios);
        }
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public string BuscarComponenteDeDivision2(string IdDivision)
        {
            
            //IdDivision = "2";
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorialPadre = int.Parse(IdDivision) });

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
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public string BuscarTerritorios(string localidadPadre)
        {
            IEnumerable<Territorio> territorios = null;
            territorios = BLLPrestamo.Instance.TerritorioBuscarTerritoriosHijos(new TerritorioSearchParams() { IdNegocio = 1, IdLocalidadPadre = int.Parse(localidadPadre) });
            return JsonConvert.SerializeObject(territorios);
        }
        [HttpPost]
        public IActionResult Post(Territorio territorio)
        {
            territorio.Usuario = this.LoginName;
            territorio.IdLocalidadNegocio = this.IdLocalidadNegocio;
            territorio.IdNegocio = 1;
            //territorio.Codigo = "";
            BLLPrestamo.Instance.TerritorioInsUpd(territorio);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }
        [HttpPost]
        public IActionResult SaveDivisionTerritorial(Territorio territorio)
        {
            territorio.Usuario = this.LoginName;
            territorio.IdLocalidadNegocio = this.IdLocalidadNegocio;
            territorio.IdNegocio = 1;
            territorio.IdLocalidadPadre = 1;
            territorio.PermiteCalle = false;
            //territorio.Codigo = "";
            BLLPrestamo.Instance.TerritorioInsUpd(territorio);
            return Ok();
        }
    }
}
