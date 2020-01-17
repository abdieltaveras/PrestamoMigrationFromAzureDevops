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
        public void InsUpdMarca(MarcaInsUpdParams insUpdParam)
        {
            try
            {
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdMarca", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<Marca> GetMarcas(MarcaGetParams searchParam)
        {
            IEnumerable<Marca> result = new List<Marca>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Marca>("spGetMarcas", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
