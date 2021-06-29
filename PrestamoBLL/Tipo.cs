﻿using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<TipoGarantia> TiposGarantiaGet(TipoGarantiaGetParams searchParam)
        {
            return BllAcciones.GetData<TipoGarantia, TipoGarantiaGetParams>(searchParam, "spGetTiposGarantia", GetValidation);
        }

        public void TipoGarantiaInsUpd(TipoGarantia insUpdParam)
        {
            BllAcciones.InsUpdData<TipoGarantia>(insUpdParam, "spInsUpdTipoGarantia");
        }
    }
}
