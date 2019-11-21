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
            var result = Database.DataServer.ExecReaderSelSP<TasaInteres>("spGetTasasInteres", SearchRec.ToSqlParams(searchParam));

            return result;
        }
        public void insUpdTasaInteres(TasaInteres insUpdParam)
        {
            Database.DataServer.ExecSelSP("spInsUpdTasaInteres", SearchRec.ToSqlParams(insUpdParam));
        }
        public void DeleteTasaInteres(TasaInteresDelParams delParam)
        {
            Database.DataServer.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }
    }
}
