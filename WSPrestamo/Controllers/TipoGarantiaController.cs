using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using Newtonsoft.Json;
namespace WSPrestamo.Controllers
{
    public class TipoGarantiaController : BaseApiController
    {
        public IEnumerable<TipoGarantia> Get(string JsonGet = "")
        {
            var paramss = JsonConvert.DeserializeObject<TipoGetParams>(JsonGet);
            return BLLPrestamo.Instance.TiposGarantiaGet(paramss);
            //return BllAcciones.GetData<TipoGarantia, TipoGetParams>(searchParam, "spGetTiposGarantia", GetValidation);
        }

        public void TipoGarantiaInsUpd(TipoGarantia insUpdParam)
        {
            //BllAcciones.InsUpdData<TipoGarantia>(insUpdParam, "spInsUpdTipoGarantia");
        }
    }
}