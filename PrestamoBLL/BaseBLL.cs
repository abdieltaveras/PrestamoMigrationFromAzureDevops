using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrestamoBLL
{
    internal class PropUsuario : IUsuario
    {
        public string Usuario { get; set; }
    }
    public abstract class BaseBLL
    {
        #region props and constructor
        internal int IdLocalidadNegocioLoggedIn { get; }
        internal string LoginName { get; }
        internal Database DBPrestamo => ConexionDB.DBPrestamo;
        internal BaseBLL(int idLocalidadNegocioLoggedIn, string loginName)
        {
            BLLValidations.LocalidadNegocioNotEqualToZero(idLocalidadNegocioLoggedIn);
            BLLValidations.UsuarioNotEmptyOrNUll(loginName);
            this.IdLocalidadNegocioLoggedIn = idLocalidadNegocioLoggedIn;
            this.LoginName = loginName;
        }
        #endregion
        #region Main Functions
        internal IEnumerable<TResult> Get<TResult>(string spName, object parameters) where TResult : class
        {
            var result = DBPrestamo.ExecReaderSelSP<TResult>(spName, SearchRec.ToSqlParams(parameters));
            return result;
        }

        internal int InsUpd(string spName, object parameters)
        {
            var result = DBPrestamo.ExecReaderSelSP(spName, SearchRec.ToSqlParams(parameters));
            return GetId(result);
        }
        #endregion

        #region Delete Methods
        internal bool SoftDelete(string spName, int idRegistro, string motivo)
        {
            var DeleteParams = new { IdRegistro = idRegistro, Motivo = motivo, Usuario = this.LoginName };
            var sqlParams = SearchRec.ToSqlParams(DeleteParams);
            DBPrestamo.ExecReaderSelSP(spName, SearchRec.ToSqlParams(DeleteParams));
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="obj"> el objeto que contiene los parametros</param>
        internal bool SoftDelete(string spName, object obj)
        {
            var sqlParams = SearchRec.ToSqlParams(obj);
            DBPrestamo.ExecReaderSelSP(spName, SearchRec.ToSqlParams(obj));
            return true;
        }

        /// <summary>
        /// Execute Delete based unde spDelete
        /// </summary>
        /// <param name="idRegistro"></param>
        /// <param name="motivo"></param>
        /// <param name="tableName"></param>
        internal bool SoftDeleteUsingCommonSP(int idRegistro, string IdRegistroColumnName, string motivo, string tableName)
        {
            var deleteParams = new { IdRegistroValor = idRegistro, idRegistroNombreColumna = IdRegistroColumnName, Motivo = motivo, Usuario = this.LoginName, NombreTabla = tableName };
            DBPrestamo.ExecReaderSelSP("spDeleteRegistro", SearchRec.ToSqlParams(deleteParams));
            return true;
        }
        #endregion

        #region Utils Functions
        internal PropUsuario CreatePropUsuario()
        {
            return new PropUsuario { Usuario = this.LoginName };
        }

        internal void AddParamUsuario(SearchRec obj)
        {
            obj.AddParam("usuario", this.LoginName);
        }

        internal void IncluirSoloBorrados(SearchRec obj)
        {
            obj.AddParam("condicionBorrado", 1);
        }

        internal void ExcluirBorrados(SearchRec obj)
        {
            obj.AddParam("condicionBorrado", 0);
        }
        internal void IncluirBorradosYNoBorrados(SearchRec obj)
        {
            obj.AddParam("condicionBorrado", -1);
        }
        internal void SetIdLocalidadNegocioAndUsuario(IUsuarioAndIdLocalidadNegocio obj)
        {
            obj.IdLocalidadNegocio = this.IdLocalidadNegocioLoggedIn;
            obj.Usuario = this.LoginName;
        }
        internal void SetUsuario(IUsuario obj)
        {
            obj.Usuario = this.LoginName;
        }
        internal int GetId(SqlDataReader sdr)
        {
            int id = -1;
            while (sdr.Read())
            {
                id = Convert.ToInt32(sdr[0].ToString());
            }
            return id;
        }
        internal int GetId(DataTable obj)
        {
            return Convert.ToInt32(obj.Rows[0][0]);
        }
        #endregion
    }
}
