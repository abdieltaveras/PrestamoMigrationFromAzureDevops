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
        public IEnumerable<TasaInteres> TasasInteresGet(TasaInteresGetParams searchParam)
        {
            return BllAcciones.GetData<TasaInteres, TasaInteresGetParams>(searchParam, "spGetTasasInteres", GetValidation);
        }
        public void TasaInteresInsUpd(TasaInteres insUpdParam)
        {
            BllAcciones.InsUpdData<TasaInteres>(insUpdParam, "spInsUpdTasaInteres");
        }
        
        public void TasaInteresDelete(TasaInteresDelParams delParam)
        {
            PrestamosDB.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }
    }
}
