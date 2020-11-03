using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<RedSocial> GetRedesSociales(RedSocialGetParams searchParam)
        {
            return DBPrestamo.ExecReaderSelSP<RedSocial>("spGetRedesSociales");
        }
        public void InsUpdRedesSociales(RedSocial insUpdParam)
        {
            BllAcciones.InsUpdData<RedSocial>(insUpdParam, "spInsUpdRedesSociales");
        }
    }
}
