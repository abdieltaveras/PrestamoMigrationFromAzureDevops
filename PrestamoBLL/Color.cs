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
        public void InsUpdColor(Color insUpdParam)
        {
            try
            {
                PrestamosDB.ExecSelSP("spInsUpdColor", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<Color> GetColores(ColorGetParams searchParam)
        {
            IEnumerable<Color> result = new List<Color>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Color>("spGetColores", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
