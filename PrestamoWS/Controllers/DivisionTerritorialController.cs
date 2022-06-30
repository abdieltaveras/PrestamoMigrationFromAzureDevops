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
        #region actions

        [HttpGet]
        public IEnumerable<DivisionTerritorial> Get([FromQuery] DivisionTerritorialGetParams search)=> _GetDivisiones(search);
        [HttpGet]
        public IEnumerable<DivisionTerritorial> GetDivisionTerritorialComponents([FromQuery]  DivisionTerritorialComponentsGetParams search)=>
            _GetDivisionTerritorialComponent(search);
        [HttpGet]
        public IEnumerable<DivisionTerritorial> GetTiposDivisionTerritorial()=>  _GetTiposDivisionTerritorial();
        [HttpPost]
        public IActionResult Post([FromBody] DivisionTerritorial DivisionTerritorial)=>_Save(DivisionTerritorial);

        #endregion

        #region private functions
        private IActionResult _Save(DivisionTerritorial DivisionTerritorial)
        {
            DivisionTerritorial.Usuario = this.LoginName;
            DivisionTerritorial.IdLocalidadNegocio = this.IdLocalidadNegocio;
            DivisionTerritorial.IdNegocio = 1;
            BLLPrestamo.Instance.InsUpdDivisionTerritorial(DivisionTerritorial);
            return Ok();
        }
        private static IEnumerable<DivisionTerritorial> _GetTiposDivisionTerritorial()
        {
            var result = BLLPrestamo.Instance.GetTiposDivisonTerritorial("test");
            return result;
        }
        private static IEnumerable<DivisionTerritorial> _GetDivisionTerritorialComponent(DivisionTerritorialComponentsGetParams search)
        {
            var result = BLLPrestamo.Instance.GetDivisionTerritorialComponents(search);
            return result;
        }
        private IEnumerable<DivisionTerritorial> _GetDivisiones(DivisionTerritorialGetParams search)
        {
            var result = BLLPrestamo.Instance.GetDivisionesTerritoriales(search);
            return result;
        }
        #endregion 
    }
}
