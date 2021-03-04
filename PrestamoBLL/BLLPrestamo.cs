using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    /// <summary>
    /// Clase que contiene toda la logica de negocios y operaciones que insertan, actualizan, borran, buscan,  datos entre los objetos y la base de datos
    /// </summary>
    public partial class BLLPrestamo
    {
        /// <summary>
        /// instancia que tiene el objeto dataserver con la conexion de la base de datos
        /// la cual es obtenida del la propiedad Server del objeto ConexionDB
        /// </summary>


        internal static Database DBPrestamo => Database.AdHoc(ConexionDB.Server);
        #region StaticBLL
        private static BLLPrestamo _bll = null;
        public static  BLLPrestamo Instance
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
        /// <summary>
        /// Es un objeto que maneja los errores que se producen a nivel de la base de datos o del Bll
        /// </summary>
        /// <param name="e"></param>
        internal static void DatabaseError(Exception e)
        {
            var mensaje = e.Message;
            //if (mensaje == "Object reference not set to an instance of an object.")
            //    mensaje = $"La cadena de conexion [{ConexionDB.Server}] indicada no permitio establecer la conexion";
            
            var index1 = mensaje.IndexOf("Violation of UNIQUE KEY constraint");
            if (index1 >= 0)
            {
                /// donde inicia el nombre de la columna
                var nombreColumna = GetColumnNameFromSqlErrorMessage(mensaje, index1);
                var valueColumna = GetValueFromSqlErrorMessage(mensaje, index1);
                mensaje = $"Error valor {valueColumna} duplicado en el campo {nombreColumna}, ya existe para otro registro";
            }
            
            throw new Exception(mensaje,e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="index1"></param>
        /// <returns></returns>
        private static string GetColumnNameFromSqlErrorMessage(string mensaje, int index1)
        {
            var index2 = mensaje.IndexOf("UQ_") + 3;
            var index3 = mensaje.IndexOf(" ", index2);
            var nombreColumna = mensaje.Substring(index2, (index3 - index2));
            return nombreColumna;
        }
        private static string GetValueFromSqlErrorMessage(string mensaje, int index1)
        {
            var index2 = mensaje.IndexOf("is (")+4;
            var index3 = mensaje.IndexOf(")");
            var value = mensaje.Substring(index2, (index3 - index2));
            return value;
        }
        private static void ThrowErrorIfIdNotSet(int id)
        {
            if (id == 0)
            {
                throw new NullReferenceException("El parametro ID que indica que se anulara no esta asignado lo cual no es permitido");
            }
        }
        private static void ThrowErrorIfUsuarioEmptyOrNull(string usuario) 
        {
            if (usuario == null || usuario == string.Empty)
            {
                throw new NullReferenceException("El parametro usuario que indica quien esta realizando la accion esta nulo o vacio lo cual no es permitido");
            }
        }
        private static void ThrowErrorIfNegocioIsZero(int idNegocio)
        {
            if (idNegocio == 0)
            {
                throw new NullReferenceException("El valor de IdNegocio es cero");
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

        private static void CancelValidation(BaseAnularOrDeleteParams cancelParam)
        {
            ThrowErrorIfIdNotSet(cancelParam.Id);
        }
        //Luis estaba private
        public static void GetValidation(BaseGetParams getParam)
        {
            var idNegocio = getParam.IdNegocio;
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
            var result3 = DBPrestamo.ExecEscalar(query); 
            var valor = System.Convert.ToInt32(result3);
            return valor > 0;
        }
        //Luis Mod. estaba declara la clase como Protected
        /// <summary>
        /// Acciones comunes de Bll Get, Insert, Update
        /// </summary>
        public  class BllAcciones
        {

            public static IEnumerable<TInsert2> GetData<TInsert2, TGet2>(TGet2 searchParam, string storedProcedure, Action<BaseGetParams> getValidations, Action<Exception> databaseErrorMethod = null) where TInsert2 : class where TGet2 : class
            {
                
                if (searchParam is BaseGetParams) { getValidations(searchParam as BaseGetParams); }
                IEnumerable<TInsert2> result = new List<TInsert2>();
                try
                {
                   var searchSqlParams = SearchRec.ToSqlParams(searchParam);                   
                   result = DBPrestamo.ExecReaderSelSP<TInsert2>(storedProcedure, searchSqlParams);
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

            public static int InsUpdData<TInsert2>(TInsert2 insUpdParam, string storedProcedure, Action<Exception> databaseErrorMethod = null) where TInsert2 : BaseUsuarioEIdNegocio
            {
                InsUpdValidation(insUpdParam);
                var last_id = 0;
                try
                {
                    var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                    using (var response = DBPrestamo.ExecReaderSelSP(storedProcedure, _insUpdParam))
                    {
                        while (response.Read())
                        {
                            last_id = int.Parse(response[0].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    InvokeErrorMethod(databaseErrorMethod, e);
                }
                return last_id;
            }

            public static void CancelData<TInsert2>(TInsert2 CancelParam, string storedProcedure, Action<Exception> databaseErrorMethod = null) where TInsert2 : BaseAnularOrDeleteParams
            {
                CancelValidation(CancelParam);
                try
                {
                    var _cancelParam = SearchRec.ToSqlParams(CancelParam);
                    DBPrestamo.ExecSelSP(storedProcedure, _cancelParam);
                }
                catch (Exception e)
                {
                    InvokeErrorMethod(databaseErrorMethod, e);
                }
            }

        }
    }
}
