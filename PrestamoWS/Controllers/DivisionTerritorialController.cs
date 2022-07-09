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
        public async Task Post([FromBody] DivisionTerritorial DivisionTerritorial)=> await _Save(DivisionTerritorial);

        #endregion

        #region private functions
        private async Task _Save(DivisionTerritorial DivisionTerritorial)
        {
            new DivisionTerritorialBLL(this.IdLocalidadNegocio, this.LoginName).SaveDivisionTerritorial(DivisionTerritorial);
        }
        private IEnumerable<DivisionTerritorial> _GetTiposDivisionTerritorial()
        {
            var result = new DivisionTerritorialBLL(this.IdLocalidadNegocio, this.LoginName).GetTiposDivisionTerritorial();
            return result;
        }
        private  IEnumerable<DivisionTerritorial> _GetDivisionTerritorialComponent(DivisionTerritorialComponentsGetParams search)
        {
            var result = new DivisionTerritorialBLL(this.IdLocalidadNegocio, this.LoginName).GetDivisionTerritorialComponents(search);
            return result;
        }
        private IEnumerable<DivisionTerritorial> _GetDivisiones(DivisionTerritorialGetParams search)
        {
            var result = new DivisionTerritorialBLL(this.IdLocalidadNegocio, this.LoginName).GetDivisionesTerritoriales(search);
            return result;
        }
        #endregion 
    }
}
