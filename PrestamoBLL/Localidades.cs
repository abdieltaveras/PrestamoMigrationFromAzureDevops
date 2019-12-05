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
        public IEnumerable<Localidad> GetLocalidades(LocalidadGetParams searchParam)
        {
            IEnumerable<Localidad> result = new List<Localidad>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Localidad>("spGetLocalidades", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
            return result;
        }
        public IEnumerable<Localidad> GuardarLocalidad(Localidad localidad)
        {
            IEnumerable<Localidad> result = new List<Localidad>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Localidad>("spInsUpdLocalidad", SearchRec.ToSqlParams(localidad));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
            return result;
        }        

        public IEnumerable<BuscarLocalidad> BuscarLocalidad(BuscarLocalidadParams searchParam)
        {
            IEnumerable<BuscarLocalidad> result = new List<BuscarLocalidad>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<BuscarLocalidad>("spBuscarLocalidad", SearchRec.ToSqlParams(searchParam));
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
