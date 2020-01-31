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
        public IEnumerable<Localidad> LocalidadesGet(LocalidadGetParams searchParam)
        {
            IEnumerable<Localidad> result = new List<Localidad>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Localidad>("spGetLocalidades", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
            return result;
        }
        public void LocalidadInsUpd(Localidad localidad)
        {
            try
            {
                PrestamosDB.ExecReaderSelSP<Localidad>("spInsUpdLocalidad", SearchRec.ToSqlParams(localidad));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
        }        

        public IEnumerable<BuscarLocalidad> LocalidadSearch(BuscarLocalidadParams searchParam)
        {
            IEnumerable<BuscarLocalidad> result = new List<BuscarLocalidad>();
            try
            {
                 result = PrestamosDB.ExecReaderSelSP<BuscarLocalidad>("spBuscarLocalidad", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                //DatabaseError(e);
                throw e;
            }
            return result;
        }

        public List<string> LocalidadSearchName(BuscarNombreLocalidadParams searchParam)
        {
            List<string> result = new List<string>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<string>("spGetLocalidadById", SearchRec.ToSqlParams(searchParam));
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
