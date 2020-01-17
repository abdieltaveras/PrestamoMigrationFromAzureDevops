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
        public void InsUpdModelo(ModeloInsUpdParams insUpdParam)
        {
            try
            {
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdModelo", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<ModeloWithMarca> GetModelos(ModeloGetParams searchParam)
        {
            IEnumerable<ModeloWithMarca> result = new List<ModeloWithMarca>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<ModeloWithMarca>("spGetModelos", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
