using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    internal  class BLLValidatations
    {
        /// <summary>
        /// throw error if 
        /// </summary>
        /// <param name="idLocalidadNegocio"></param>
        /// <param name="usuario"></param>
        internal static void LocalidadNegocioNotEqualToZero(int idLocalidadNegocio)
        {
            if (idLocalidadNegocio == 0) throw new NullReferenceException("Id Localidad no puede ser igual a 0");
        }

        internal static void UsuarioNotEmptyOrNUll(string LoginName)
        {
            if (string.IsNullOrEmpty(LoginName))
                throw new NullReferenceException("El usuario no puede estar vacio o nulo");
        }
    }

    

    public abstract class BaseBLL 
    {
        public int IdLocalidadNegocioLoggedIn { get; }
        public string LoginName { get; }
        internal BaseBLL(int idLocalidadNegocioLoggedIn, string loginName)
        {
            BLLValidatations.LocalidadNegocioNotEqualToZero(idLocalidadNegocioLoggedIn);
            BLLValidatations.UsuarioNotEmptyOrNUll(loginName);
            this.IdLocalidadNegocioLoggedIn = idLocalidadNegocioLoggedIn;
            this.LoginName = loginName;
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

    public class DivisionTerritorialBLL  : BaseBLL 
    {
        public DivisionTerritorialBLL(int idLocalidadNegocioLoggedIn, string loginName): base (idLocalidadNegocioLoggedIn, loginName)  { }
        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales(DivisionTerritorialGetParams searchParam)
        {
            SetUsuario(searchParam);
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
        }
        public IEnumerable<DivisionTerritorial> GetDivisionTerritorialComponents(DivisionTerritorialComponentsGetParams searchParam)
        {
            SetUsuario(searchParam);
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetDivisionTerritorialComponents", SearchRec.ToSqlParams(searchParam));
        }
        public IEnumerable<DivisionTerritorial> GetTiposDivisionTerritorial()
        {
            var searchObj = new SearchRec();
            AddParamUsuario(searchObj);
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetTiposDivisionTerritorial", searchObj.ToSqlParams());
        }
        public int  SaveDivisionTerritorial(DivisionTerritorial insUpdParam)
        {
            SetIdLocalidadNegocioAndUsuario(insUpdParam);
            var result = ConexionDB.DBPrestamo.ExecReaderSelSP("spInsUpdDivisionTerritorial", SearchRec.ToSqlParams(insUpdParam));
            return GetId(result);
        }
    }
}
