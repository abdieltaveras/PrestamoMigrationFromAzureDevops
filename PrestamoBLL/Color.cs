using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public void InsUpdColor(Color insUpdParam)
        {
            BllAcciones.InsUpdData<Color>(insUpdParam, "spInsUpdColor");
        }

        public IEnumerable<Color> GetColores(ColorGetParams searchParam)
        {
            return BllAcciones.GetData<Color, ColorGetParams>(searchParam, "spGetColores", GetValidation);
        }

        public void AnularColor(int idColor)
        {
            // verificar si el color ha sido usado en algun registro de alguna tabla relacionada


            // segundo si no ha sido usado proceder a anularlo
            BLLPrestamo.DBPrestamo.ExecNonQuery($"delete from tblColores where idColor={idColor}");
        }
    }
}
