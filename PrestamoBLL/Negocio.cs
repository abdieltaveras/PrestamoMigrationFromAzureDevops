using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<Negocio> GetNegocios(NegociosGetParams searchParam)
        {
            GetValidation(searchParam);
            IEnumerable<Negocio> result = new List<Negocio>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Negocio>("spGetNegocios", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void insUpdNegocio(Negocio insUpdParam)
        {
            InsUpdValidation(insUpdParam);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdNegocio", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

    }
}
