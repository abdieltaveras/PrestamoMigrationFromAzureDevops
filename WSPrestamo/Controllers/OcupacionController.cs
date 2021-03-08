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
    public class OcupacionController : BaseApiController
    {
        public IEnumerable<Ocupacion> Get(OcupacionGetParams searchParam)
        {
            return BLLPrestamo.Instance.GetOcupaciones(searchParam);
        }

        [HttpPost]
        public IHttpActionResult InsUpdOcupacion(Ocupacion insUpdParam)
        {
            BLLPrestamo.Instance.InsUpdOcupacion(insUpdParam);
            return Ok();
        }
    }
}
