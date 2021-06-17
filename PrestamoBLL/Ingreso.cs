using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;
using emtSoft.DAL;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        

        public void InsUpdIngreso(Ingreso insUpdParam)
        {
            BllAcciones.InsUpdData<Ingreso>(insUpdParam, "spInsUpdIngresos");
        }

        public IEnumerable<Ingreso> GetIngresos(IngresoGetParams searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            return BllAcciones.GetData<Ingreso, IngresoGetParams>(searchParam, "spGetDatosPIngreso", GetValidation);
        }

        public void AnularIngreso(int idIngreso, string usuario)
        {
            PendienteDeImplementacionException();
        }
    }
}
