using emtSoft.DAL;
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
        
        public IEnumerable<TipoMora> TiposMorasGet(TipoMoraGetParams  searchParam)
        {
            IEnumerable<TipoMora> result=new List<TipoMora>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<TipoMora>("spGetTiposMora", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void TipoMoraInsUpd(TipoMora insUpdParam)
        {
            try
            {
                PrestamosDB.ExecSelSP("spInsUpdTipoMora", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
        
        public void TipoMoraCancel(TipoMoraDelParams delParam)
        {
            PrestamosDB.ExecSelSP("spAnularTipoMora", SearchRec.ToSqlParams(delParam));
        }

        public void TipoMoraDelete(TipoMoraDelParams delParam)
        {
            PrestamosDB.ExecSelSP("spDelTipoMora", SearchRec.ToSqlParams(delParam));
        }
    }
}
