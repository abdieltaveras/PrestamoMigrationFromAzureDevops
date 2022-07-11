using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrestamoBLL
{
    public abstract class BaseBLL
    {
        protected int IdLocalidadNegocioLoggedIn { get; }
        protected string LoginName { get; }

        protected Database DBPrestamo => ConexionDB.DBPrestamo;
        internal BaseBLL(int idLocalidadNegocioLoggedIn, string loginName)
        {
            BLLValidations.LocalidadNegocioNotEqualToZero(idLocalidadNegocioLoggedIn);
            BLLValidations.UsuarioNotEmptyOrNUll(loginName);
            this.IdLocalidadNegocioLoggedIn = idLocalidadNegocioLoggedIn;
            this.LoginName = loginName;
        }      
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

        internal void AddParamUsuario(SearchRec obj)
        {
            obj.AddParam("usuario", this.LoginName);
        }

        internal void IncluirSoloBorrados(SearchRec obj)
        {
            obj.AddParam("condicionBorrado",1 );
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
         
    }
}
