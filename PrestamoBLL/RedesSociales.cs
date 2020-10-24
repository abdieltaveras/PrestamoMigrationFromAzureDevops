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
        public IEnumerable<RedesSociales> GetRedesSociales(RedesSocialesGetParams searchParam)
        {
            return BllAcciones.GetData<RedesSociales, RedesSocialesGetParams>(searchParam, "spGetRedesSociales", GetValidation);
        }
        public void InsUpdRedesSociales(RedesSociales insUpdParam)
        {
            BllAcciones.InsUpdData<RedesSociales>(insUpdParam, "spGetRedesSociales");
        }
    }
}
