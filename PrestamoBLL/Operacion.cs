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
        public IEnumerable<Operacion> OperacionesGet(OperacionGetParams searchParam)
        {
            return BllAcciones.GetData<Operacion, OperacionGetParams>(searchParam, "spGetOperaciones", GetValidation);
        }

        //public void OcupacionInsUpd(Ocupacion insUpdParam)
        //{
        //    BllAcciones.InsUpdData<Ocupacion>(insUpdParam, "spInsUpdOcupacion");
        //}
    }
}
