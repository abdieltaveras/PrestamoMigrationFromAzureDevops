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
        public void GuardarGarantia(Garantia garantia)
        {
            try
            {
                Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Localidad>("spInsUpdGarantias", SearchRec.ToSqlParams(garantia));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
        }

        public IEnumerable<Garantia> BuscarGarantia(BuscarGarantiaParams searchParam)
        {
            IEnumerable<Garantia> result = new List<Garantia>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Garantia>("spBuscarGarantias", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
            return result;
        }

    }
}
