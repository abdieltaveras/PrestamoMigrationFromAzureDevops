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
        public IEnumerable<Ocupacion> Get(int idOcupacion = -1, int idLocalidadNegocio = -1)
        {
            var search = new OcupacionGetParams();
            search.Usuario = this.LoginName;
            search.IdOcupacion = idOcupacion;
            search.IdLocalidadNegocio = idLocalidadNegocio;
            return BLLPrestamo.Instance.GetOcupaciones(search);
        }

        [HttpPost]
        public IHttpActionResult InsUpdOcupacion(Ocupacion insUpdParam)
        {
            BLLPrestamo.Instance.InsUpdOcupacion(insUpdParam);
            return Ok();
        }
    }
}
