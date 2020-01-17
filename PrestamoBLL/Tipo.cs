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
        public void InsUpdTipo(TipoInsUpdParams insUpdParam)
        {
            try
            {
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdTipo", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<Tipo> GetTipos(TipoGetParams searchParam)
        {
            IEnumerable<Tipo> result = new List<Tipo>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Tipo>("spGetTipos", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
