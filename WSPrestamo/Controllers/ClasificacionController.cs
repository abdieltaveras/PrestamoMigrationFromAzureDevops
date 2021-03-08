using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static PrestamoBLL.BLLPrestamo;

namespace WSPrestamo.Controllers
{
    public class ClasificacionController : BaseApiController
    {
        public IEnumerable<Clasificacion> Get(int idNegocio)
        {
            var result = PrestamoBLL.BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(idNegocio);
            var result2 = result.Where(clas => clas.RequiereGarantia);
            return result2;
        }
    }
}
