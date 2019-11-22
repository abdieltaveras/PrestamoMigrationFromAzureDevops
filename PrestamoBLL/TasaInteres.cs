using emtSoft.DAL;
using PrestamoDAL;
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
        public IEnumerable<TasaInteres> GetTasasInteres(TasaInteresGetParams searchParam)
        {
            IEnumerable<TasaInteres> result=new List<TasaInteres>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<TasaInteres>("spGetTasasInteres", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void insUpdTasaInteres(TasaInteres insUpdParam)
        {
            try
            {
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdTasaInteres", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
        
        public void DeleteTasaInteres(TasaInteresDelParams delParam)
        {
            Database.AdHoc(ConexionDB.Server).ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }
    }
}
