using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<Territorio> TerritoriosGet(TerritorioGetParams searchParam)
        {
            return BllAcciones.GetData<Territorio, TerritorioGetParams>(searchParam, "spGetTerritorios", GetValidation);
        }

        public IEnumerable<Territorio> TerritorioDivisionesTerritorialesGet(TerritorioGetParams searchParam)
        {
            return BllAcciones.GetData<Territorio, TerritorioGetParams>(searchParam, "spGetDivisionesTerritoriales", GetValidation);
        }

        public IEnumerable<Territorio> TerritorioDivisionesTerritorialesPaisesGet(TerritorioGetParams searchParam)
        {
            return BllAcciones.GetData<Territorio, TerritorioGetParams>(searchParam, "spGetPaisesDeDivisionTerritorial", GetValidation);
        }

        public IEnumerable<Territorio> TerritorioBuscarTerritoriosHijos(TerritorioSearchParams searchParam)
        {
            return BllAcciones.GetData<Territorio, TerritorioSearchParams>(searchParam, "spGetTerritorios", GetValidation);
        }

        public IEnumerable<Territorio> TerritorioBuscarComponentesDivisionesTerritoriales(DivisionSearchParams searchParam)
        {
            return BllAcciones.GetData<Territorio, DivisionSearchParams>(searchParam, "spGetComponentesDeDivisionTerritorial", GetValidation);
        }

        public void TerritorioInsUpd(Territorio insUpdParam)
        {
            BllAcciones.InsUpdData<Territorio>(insUpdParam, "spInsUpdTerritorios");
        }
    }
}
