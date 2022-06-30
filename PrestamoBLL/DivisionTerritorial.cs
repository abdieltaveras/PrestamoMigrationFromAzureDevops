using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
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
