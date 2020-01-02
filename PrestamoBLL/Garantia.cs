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
        public void GuardarGarantia(GarantiaInsUptParams garantia)
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

        public IEnumerable<GarantiaInsUptParams> BuscarGarantia(BuscarGarantiaParams searchParam)
        {
            IEnumerable<GarantiaInsUptParams> result = new List<GarantiaInsUptParams>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<GarantiaInsUptParams>("spBuscarGarantias", SearchRec.ToSqlParams(searchParam));
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
