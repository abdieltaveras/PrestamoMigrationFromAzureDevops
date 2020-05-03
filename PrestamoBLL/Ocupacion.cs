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
        public IEnumerable<Ocupacion> OcupacionesGet(OcupacionGetParams searchParam)
        {
            return BllAcciones.GetData<Ocupacion, OcupacionGetParams>(searchParam, "spGetOcupaciones", GetValidation);
        }

        public void OcupacionInsUpd(Ocupacion insUpdParam)
        {
            BllAcciones.InsUpdData<Ocupacion>(insUpdParam, "spInsUpdOcupacion");
        }
    }
}
