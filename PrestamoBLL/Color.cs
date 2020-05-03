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
        public void ColorInsUpd(Color insUpdParam)
        {
            BllAcciones.InsUpdData<Color>(insUpdParam, "spInsUpdColor");
        }

        public IEnumerable<Color> ColoresGet(ColorGetParams searchParam)
        {
            return BllAcciones.GetData<Color, ColorGetParams>(searchParam, "spGetColores", GetValidation);
        }
    }
}
