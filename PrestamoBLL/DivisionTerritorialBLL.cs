using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;

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

    internal class SetValues
    {
        static internal void SetLocalidadNegocioAndUsuario(BaseUsuarioEIdNegocio UsrAndIdNegocio, IUsuarioAndIdLocalidad value)
        {
            UsrAndIdNegocio.IdLocalidadNegocio = value.IdLocalidadNegocio;
            UsrAndIdNegocio.Usuario = value.LoginName;
        }
    }

    interface IUsuarioAndIdLocalidad
    {
        int IdLocalidadNegocio { get; }
        string LoginName { get; }
    }

    public abstract class BaseBLL : IUsuarioAndIdLocalidad
    {
        public int IdLocalidadNegocio { get; }
        public string LoginName { get; }
        internal BaseBLL(int idLocalidadNegocio, string loginName)
        {
            BLLValidatations.LocalidadNegocioNotEqualToZero(idLocalidadNegocio);
            BLLValidatations.UsuarioNotEmptyOrNUll(loginName);
            this.IdLocalidadNegocio = idLocalidadNegocio;
            this.LoginName = loginName;
        }

        protected void AddParamUsuario(SearchRec obj)
        {
            obj.AddParam("usuario", this.LoginName);
        }
    }

    public class DivisionTerritorialBLL  : BaseBLL 
    {
        
        public DivisionTerritorialBLL(int idLocalidadNegocio, string loginName): base (idLocalidadNegocio, loginName)  { }
        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales(DivisionTerritorialGetParams searchParam)
        {
            SetValues.SetLocalidadNegocioAndUsuario(searchParam, this);
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
        }
        public IEnumerable<DivisionTerritorial> GetDivisionTerritorialComponents(DivisionTerritorialComponentsGetParams searchParam)
        {
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetDivisionTerritorialComponents", SearchRec.ToSqlParams(searchParam));
        }
        public IEnumerable<DivisionTerritorial> GetTiposDivisonTerritorial()
        {
            var searchObj = new SearchRec();
            AddParamUsuario(searchObj);
            return ConexionDB.DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetTiposDivisionTerritorial", searchObj.ToSqlParams());
        }
        public void InsUpdDivisionTerritorial(DivisionTerritorial insUpdParam)
        {
            ConexionDB.DBPrestamo.ExecReaderSelSP("spInsUpdTerritorios", SearchRec.ToSqlParams(insUpdParam));
        }
    }


    public partial class BLLPrestamo
    {
        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales(DivisionTerritorialGetParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, DivisionTerritorialGetParams>(searchParam, "spGetDivisionTerritorial", GetValidation);
        }
        public IEnumerable<DivisionTerritorial> GetDivisionTerritorialComponents(DivisionTerritorialComponentsGetParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, DivisionTerritorialComponentsGetParams>(searchParam, "spGetDivisionTerritorialComponents", GetValidation);
        }
        public IEnumerable<DivisionTerritorial> GetTiposDivisonTerritorial(string usuario)
        {
            var searchObj = new SearchRec();
            searchObj.AddParam("Usuario", usuario);
            return DBPrestamo.ExecReaderSelSP<DivisionTerritorial>("spGetTiposDivisionTerritorial", searchObj.ToSqlParams());
        }

        public void InsUpdDivisionTerritorial(DivisionTerritorial insUpdParam)
        {
            BllAcciones.InsUpdData<DivisionTerritorial>(insUpdParam, "spInsUpdTerritorios");
        }
    }

}
