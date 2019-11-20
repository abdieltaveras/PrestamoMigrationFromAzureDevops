using emtSoft.DAL;
using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoDAL
{
    public partial class DALPrestamoMSSql
    {
        #region StaticBLL
        static private DALPrestamoMSSql _bll = null;
        static public DALPrestamoMSSql Instance
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new DALPrestamoMSSql();
                }
                return _bll;
            }
        }
        #endregion StaticBLL
        public IEnumerable<TasaInteres> GetTasasInteres(TasaInteresGetParams searchdata)
        {
            //checkSqlParams<TasaInteresGetParams>(searchdata);
            var result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<TasaInteres>("spGetTasasInteres", SearchRec.ToSqlParams(searchdata));
            return result;
        }

        public void insUpdTasaInteres(TasaInteres data)
        {
           Database.DataServer.ExecSelSP("spInsUpdTasaInteres",  SearchRec.ToSqlParams(data));
        }

        public void DeleteTasaInteres(TasaInteresDelParams data)
        {
            Database.DataServer.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(data));
        }

        private void checkSqlParams<T>(T data)
        { 
            var sqlParams = SearchRec.ToSqlParams(data);
        }
    }
}
