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
        public IEnumerable<TasaInteres> TasasInteresGet(TasaInteresGetParams searchParam)
        {
            IEnumerable<TasaInteres> result=new List<TasaInteres>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<TasaInteres>("spGetTasasInteres", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void TasaInteresInsUpd(TasaInteres insUpdParam)
        {
            try
            {
                PrestamosDB.ExecSelSP("spInsUpdTasaInteres", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
        
        public void TasaInteresDelete(TasaInteresDelParams delParam)
        {
            PrestamosDB.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }
    }
}
