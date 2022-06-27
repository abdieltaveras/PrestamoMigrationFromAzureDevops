using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales(DivisionTerritorialGetParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, DivisionTerritorialGetParams>(searchParam, "spGetTerritorios", GetValidation);
        }

        public IEnumerable<DivisionTerritorial> GetDivisionesTerritoriales2(DivisionTerritorialGetParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, DivisionTerritorialGetParams>(searchParam, "spGetDivisionesTerritoriales", GetValidation);
        }

        //public IEnumerable<DivisionTerritorial> TerritorioDivisionesTerritorialesPaisesGet(TerritorioGetParams searchParam)
        //{
        //    return BllAcciones.GetData<DivisionTerritorial, TerritorioGetParams>(searchParam, "spGetPaisesDeDivisionTerritorial", GetValidation);
        //}

        public IEnumerable<DivisionTerritorial> GetDivisonesTerritoriosHijas(TerritorioSearchParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, TerritorioSearchParams>(searchParam, "spGetTerritorios", GetValidation);
        }

        public IEnumerable<DivisionTerritorial> TerritorioBuscarComponentesDivisionesTerritoriales(DivisionSearchParams searchParam)
        {
            return BllAcciones.GetData<DivisionTerritorial, DivisionSearchParams>(searchParam, "spGetComponentesDeDivisionTerritorial", GetValidation);
        }

        public void InsUpdDivisionTerritorial(DivisionTerritorial insUpdParam)
        {
            BllAcciones.InsUpdData<DivisionTerritorial>(insUpdParam, "spInsUpdTerritorios");
        }
    }
}
