using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoBLL.Entidades;
using emtSoft.DAL;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public void InsUpdIngreso(Ingreso insUpdParam)
        {
            BllAcciones.InsUpdData<Ingreso>(insUpdParam, "spInsUpdIngresos");
        }

        public IEnumerable<Ingreso> GetDatosPIngreso(IngresoPGetParams searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            return BllAcciones.GetData<Ingreso, IngresoPGetParams>(searchParam, "spGetDatosPIngreso", GetValidation);
        }
    }
}
