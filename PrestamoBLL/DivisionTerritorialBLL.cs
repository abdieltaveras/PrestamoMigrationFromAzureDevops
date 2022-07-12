using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    internal class PropUsuario : IUsuario
    {
        public string Usuario { get; set; }
    }

    public class DivisionTerritorialBLL : BaseBLL
    {
        public DivisionTerritorialBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales(DivisionTerritorialGetParams searchParam)
        {
            SetUsuario(searchParam);
            var spName = "spGetDivisionTerritorial";
            return this.Get<DivisionTerritorial>(spName, searchParam);
        }
        public IEnumerable<DivisionTerritorial> GetDivisionTerritorialComponents(DivisionTerritorialComponentsGetParams searchParam)
        {
            SetUsuario(searchParam);
            var spName = "spGetDivisionTerritorialComponents";
            return this.Get<DivisionTerritorial>(spName, searchParam);
        }

        public IEnumerable<DivisionTerritorial> GetTiposDivisionTerritorial()
        {
            PropUsuario userObj = CreatePropUsuario();
            var spName = "spGetTiposDivisionTerritorial";
            return this.Get<DivisionTerritorial>(spName, userObj);
        }

        public int InsUpdDivisionTerritorial(DivisionTerritorial insUpdParam)
        {
            SetIdLocalidadNegocioAndUsuario(insUpdParam);
            var spName = "spInsUpdDivisionTerritorial";
            return this.InsUpd(spName, insUpdParam);
        }

        public bool DeleteDivisionTerritorial(int idRegistro, string motivo)
        {
             return this.SoftDeleteUsingCommonSP(idRegistro, "IdDivisionTerritorial",motivo, "tblDivisionTerritorial");
        }
    }
}
