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
        
        public IEnumerable<TipoMora> TiposMorasGet(TipoMoraGetParams  searchParam)
        {
            return BllAcciones.GetData<TipoMora, TipoMoraGetParams>(searchParam, "spGetTiposMora", GetValidation);
        }
        public void TipoMoraInsUpd(TipoMora insUpdParam)
        {
            BllAcciones.InsUpdData<TipoMora>(insUpdParam, "spInsUpdTipoMora");
        }
        
        public void TipoMoraCancel(TipoMoraDelParams delParam)
        {
            BllAcciones.CancelData<TipoMoraDelParams>(delParam, "spAnularTipoMora");

            //PrestamosDB.ExecSelSP("spAnularTipoMora", SearchRec.ToSqlParams(delParam));
        }

        public void TipoMoraDelete(TipoMoraDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelTipoMora", SearchRec.ToSqlParams(delParam));
        }
    }
}
