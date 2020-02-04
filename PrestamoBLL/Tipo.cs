using emtSoft.DAL;
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
        public IEnumerable<Tipo> TiposGet(TipoGetParams searchParam)
        {

            return BllAcciones.GetData<Tipo, TipoGetParams>(searchParam, "spGetTipos", GetValidation);
        }

        public void TipoInsUpd(Tipo insUpdParam)
        {
            BllAcciones.InsUpdData<Tipo>(insUpdParam, "spInsUpdTipo");
        }
    }
}
