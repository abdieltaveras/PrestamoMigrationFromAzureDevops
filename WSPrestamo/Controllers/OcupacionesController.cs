using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrestamoBLL;
namespace WSPrestamo.Controllers
{
    public class OcupacionesController : BaseApiController
    {
        public IEnumerable<Ocupacion> Get()
        {
            var search = new OcupacionGetParams();
            search.Usuario = this.LoginName;
            search.IdOcupacion = -1;
            search.IdLocalidadNegocio = -1;
            return BLLPrestamo.Instance.GetOcupaciones(search);
        }
        //public IEnumerable<Ocupacion> GetById(int idOcupacion = -1)
        //{
        //    var search = new OcupacionGetParams();
        //    search.Usuario = this.LoginName;
        //    search.IdOcupacion = idOcupacion;
        //    search.IdLocalidadNegocio = this.IdLocalidadNegocio;
        //    return BLLPrestamo.Instance.GetOcupaciones(search);
        //}
        [HttpPost]
        public IHttpActionResult InsUpdOcupacion(Ocupacion insUpdParam)
        {
            insUpdParam.Usuario = this.LoginName;
            insUpdParam.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdOcupacion(insUpdParam);
            return Ok();
        }
    }
}
