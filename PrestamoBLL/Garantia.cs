using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<GarantiaConMarcaYModelo> GarantiaSearch(BuscarGarantiaParams searchParam)
        {
            return BllAcciones.GetData<GarantiaConMarcaYModelo, BuscarGarantiaParams>(searchParam, "spBuscarGarantias", GetValidation);
        }

        public void GarantiaInsUpd(Garantia insUpdParam)
        {
            BllAcciones.InsUpdData<Garantia>(insUpdParam, "spInsUpdGarantias");
        }

    }
}
