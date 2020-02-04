﻿using emtSoft.DAL;
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
        public IEnumerable<Garantia> GarantiaSearch(BuscarGarantiaParams searchParam)
        {
            return BllAcciones.GetData<Garantia, BuscarGarantiaParams>(searchParam, "spBuscarGarantias", GetValidation);
        }

        public void GarantiaInsUpd(Garantia insUpdParam)
        {
            BllAcciones.InsUpdData<Garantia>(insUpdParam, "spInsUpdGarantias");
        }

    }
}
