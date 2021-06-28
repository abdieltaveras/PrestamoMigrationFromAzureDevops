using DevBox.Core.DAL.SQLServer;
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
        
        public IEnumerable<TipoMora> GetTiposMoras(TipoMoraGetParams  searchParam)
        {
            return BllAcciones.GetData<TipoMora, TipoMoraGetParams>(searchParam, "spGetTiposMora", GetValidation);
        }
        public int InsUpdTipoMora(TipoMora insUpdParam)
        {
            return BllAcciones.InsUpdData<TipoMora>(insUpdParam, "spInsUpdTipoMora");
        }
        
        public void CancelTipoMora(TipoMoraDelParams delParam)
        {

            BllAcciones.CancelData<TipoMoraDelParams>(delParam, "spAnularTipoMora");
            //PrestamosDB.ExecSelSP("spAnularTipoMora", SearchRec.ToSqlParams(delParam));
        }

        public void DeleteTipoMora(TipoMoraDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelTipoMora", SearchRec.ToSqlParams(delParam));
            
        }
    }
}
