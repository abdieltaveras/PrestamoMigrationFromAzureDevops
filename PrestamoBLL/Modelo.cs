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
        public void ModeloInsUpd(Modelo insUpdParam)
        {
            try
            {
                PrestamosDB.ExecSelSP("spInsUpdModelo", SearchRec.ToSqlParams(insUpdParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        public IEnumerable<ModeloWithMarca> ModelosGet(ModeloGetParams searchParam)
        {
            IEnumerable<ModeloWithMarca> result = new List<ModeloWithMarca>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<ModeloWithMarca>("spGetModelos", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
    }
}
