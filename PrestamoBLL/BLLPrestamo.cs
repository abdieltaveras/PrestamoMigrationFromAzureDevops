using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public static Database PrestamosDB => Database.AdHoc(ConexionDB.Server);
        #region StaticBLL
        static private BLLPrestamo _bll = null;
        static public BLLPrestamo Instance
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new BLLPrestamo();
                }
                return _bll;
            }
        }
        #endregion StaticBLL
        internal static void DatabaseError(Exception e)
        {
            
            var mensaje = e.Message;
            if (mensaje == "Object reference not set to an instance of an object.")
                mensaje = $"La cadena de conexion [{ConexionDB.Server}] indicada no permitio establecer la conexion";
            
            var index1 = mensaje.IndexOf("Violation of UNIQUE KEY constraint");
            if (index1 >= 0)
            {
                /// donde inicia el nombre de la columna
                var nombreColumna = ExtractColumnName(mensaje, index1);
                var valueColumna = ExtractValue(mensaje, index1);
                mensaje = $"Error valor {valueColumna} duplicado en el campo {nombreColumna}, ya existe para otro registro";
            }
            throw new Exception(mensaje);
        }
        private static string ExtractColumnName(string mensaje, int index1)
        {
            var index2 = mensaje.IndexOf("UQ_") + 3;
            var index3 = mensaje.IndexOf(" ", index2);
            var nombreColumna = mensaje.Substring(index2, (index3 - index2));
            return nombreColumna;
        }
        private static string ExtractValue(string mensaje, int index1)
        {
            var index2 = mensaje.IndexOf("is (")+4;
            var index3 = mensaje.IndexOf(")");
            var value = mensaje.Substring(index2, (index3 - index2));
            return value;
        }
        private static void ThrowErrorIfUsuarioEmptyOrNull(string usuario) 
        {
            if (usuario == null || usuario == string.Empty)
            {
                throw new NullReferenceException("El parametro usuario que indica quien esta realizando la accion esta nulo o vacio lo cual no es permitido");
            }
        }
        private void ThrowErrorIfNegocioIsZero(int negocio)
        {
            if (negocio == 0)
            {
                throw new NullReferenceException("El valor de IdNegocio es nulo o es cero");
            }
        }
        /// <summary>
        /// realiza validaciones generales de la insercion como no permitir usuario vacio o nulo
        /// </summary>
        /// <param name="insUpdParam"></param>
        private static void InsUpdValidation(BaseUsuario insUpdParam)
        {
            ThrowErrorIfUsuarioEmptyOrNull(insUpdParam.Usuario);
        }
        /// <summary>
        /// realiza validaciones generales de la insercion como no permitir usuario vacio o nulo
        /// </summary>
        /// <param name="insUpdParam"></param>
        private void GetValidation(BaseGetParams getParam)
        {
            ThrowErrorIfNegocioIsZero(getParam.IdNegocio);
        }

        /// <summary>
        /// Check if a table exist data for a table
        /// and for the idNegocio if it is suplied
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public bool ExistDataForTable(string table, int idNegocio=-1)
        {
            string query = string.Empty;
            if (idNegocio > 0)
            {
                query = string.Format("SELECT top 1 idNegocio FROM " + table + " (nolock) where idNegocio={0}", idNegocio);
            }
            else
            {
                query = string.Format("select case when exists(select 1 from " + table + ")  then 1 else 0 end");
                //query = string.Format("SELECT count(*) FROM " + table);
            }
            //var result2 = Database.DataServer.ExecNonQuery(query);
            var result3 = PrestamosDB.ExecEscalar(query); 
            var valor = System.Convert.ToInt32(result3);
            return valor>0;
        }

        //new Exception("Lo siento ha ocurrido un error a nivel de la base de datos");
        protected static class BllAcciones
        {
            

            public static IEnumerable<TInsert2> GetData<TInsert2, TGet2>(TGet2 searchParam, string storedProcedure, Action<Exception> databaseErrorMethod = null) where TInsert2 : class where TGet2 : class
            {

                IEnumerable<TInsert2> result = new List<TInsert2>();
                try
                {
                    var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                    result = PrestamosDB.ExecReaderSelSP<TInsert2>(storedProcedure, searchSqlParams);
                }
                catch (Exception e)
                {
                    InvokeErrorMethod(databaseErrorMethod, e);
                }
                return result;
            }

            private static void InvokeErrorMethod(Action<Exception> databaseErrorMethod, Exception e)
            {
                if (databaseErrorMethod == null) { DatabaseError(e); }
                else { databaseErrorMethod(e); };
            }

            public static void insUpdData<TInsert2>(TInsert2 insUpdParam, string storedProcedure, Action<Exception> databaseErrorMethod = null) where TInsert2 : BaseUsuarioEIdNegocio
            {
                InsUpdValidation(insUpdParam);
                try
                {
                    var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                    PrestamosDB.ExecSelSP(storedProcedure, _insUpdParam);
                }
                catch (Exception e)
                {
                    InvokeErrorMethod(databaseErrorMethod, e);
                }
            }

        }
    }
}
