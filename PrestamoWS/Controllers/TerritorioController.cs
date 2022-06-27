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

    public class DivisionTerritorialController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<DivisionTerritorial> Get()
        {
            var result = BLLPrestamo.Instance.GetDivisionesTerritoriales2(new DivisionTerritorialGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
            return result;
            //return View("CreateOrEdit", datos);
        }
        [HttpGet]
        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales()
        {
            var result = BLLPrestamo.Instance.GetDivisionesTerritoriales(new DivisionTerritorialGetParams() { IdNegocio = 1 });
            return result;
            //return View("CreateOrEdit", datos);
        }
       
        [HttpGet]
        public IEnumerable<DivisionTerritorial> BuscarComponenteDeDivision()
        {
            string IdDivision = "";
            IdDivision = "2";
            IEnumerable<DivisionTerritorial> DivisionTerritorials = null;
            DivisionTerritorials = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorialPadre = int.Parse(IdDivision) });

            return DivisionTerritorials;
            //return JsonConvert.SerializeObject(DivisionTerritorials);
        }
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<DivisionTerritorial> BuscarComponenteDeDivision2(int IdDivision)
        {
            
            //IdDivision = "2";
            IEnumerable<DivisionTerritorial> DivisionTerritorials = null;
            DivisionTerritorials = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(new DivisionSearchParams() { IdNegocio = 1, IdDivisionTerritorialPadre = IdDivision });

            //return DivisionTerritorials;
            return DivisionTerritorials;
        }
        //public IEnumerable<DivisionTerritorial> GetByParam(int idColor, int idLocalidadNegocio)
        //{
        //    var search = new ColorGetParams { IdColor = idColor, IdLocalidadNegocio = idLocalidadNegocio };
        //    var result = BLLPrestamo.Instance.GetColores(new ColorGetParams { IdNegocio = 1/*this.pcpUserIdNegocio*/ });
        //    return result;
        //    //return View("CreateOrEdit", datos);
        //}
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<DivisionTerritorial> BuscarDivisionTerritorials(string localidadPadre)
        {
            IEnumerable<DivisionTerritorial> DivisionTerritorials = null;
            DivisionTerritorials = BLLPrestamo.Instance.GetDivisionesTerritoriales(new DivisionTerritorialGetParams() { IdNegocio = 1, IdLocalidadPadre = int.Parse(localidadPadre) });
            return DivisionTerritorials;
        }
        [HttpPost]
        public IActionResult Post([FromBody] DivisionTerritorial DivisionTerritorial)
        {
            DivisionTerritorial.Usuario = this.LoginName;
            DivisionTerritorial.IdLocalidadNegocio = this.IdLocalidadNegocio;
            DivisionTerritorial.IdNegocio = 1;
            //DivisionTerritorial.Codigo = "";
            BLLPrestamo.Instance.InsUpdDivisionTerritorial(DivisionTerritorial);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }

        //[HttpPost]
        //public IActionResult SaveDivisionTerritorial([FromBody] DivisionTerritorial DivisionTerritorial)
        //{
        //    DivisionTerritorial.Usuario = this.LoginName;
        //    DivisionTerritorial.IdLocalidadNegocio = this.IdLocalidadNegocio;
        //    DivisionTerritorial.IdNegocio = 1;
        //    //DivisionTerritorial.IdLocalidadPadre = 1;
        //    DivisionTerritorial.PermiteCalle = false;
        //    //DivisionTerritorial.Codigo = "";
        //    BLLPrestamo.Instance.DivisionTerritorialInsUpd(DivisionTerritorial);
        //    return Ok();
        //}
    }
}
