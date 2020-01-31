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
        public void MarcaInsUpd(Marca insUpdParam)
        {
            try
            {
                PrestamosDB.ExecSelSP("spInsUpdMarca", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<Marca> MarcasGet(MarcaGetParams searchParam)
        {
            IEnumerable<Marca> result = new List<Marca>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Marca>("spGetMarcas", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
