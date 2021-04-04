using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;

namespace WSPrestamo.Controllers
{
    public class TipoGarantiaController : BaseApiController
    {
        public IEnumerable<TipoGarantia> Get()
        {
            return BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams());
            //return BllAcciones.GetData<TipoGarantia, TipoGetParams>(searchParam, "spGetTiposGarantia", GetValidation);
        }

        public void TipoGarantiaInsUpd(TipoGarantia insUpdParam)
        {
            //BllAcciones.InsUpdData<TipoGarantia>(insUpdParam, "spInsUpdTipoGarantia");
        }
    }
}