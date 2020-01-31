using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<Territorio> TerritoriosGet(TerritorioGetParams searchParam)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spGetTerritorios", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<Territorio> TerritorioDivisionesTerritorialesGet(TerritorioGetParams searchParam)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spGetDivisionesTerritoriales", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<Territorio> TerritorioDivisionesTerritorialesPaisesGet(TerritorioGetParams searchParam)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spGetPaisesDeDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<Territorio> TerritorioBuscarTerritoriosHijos(TerritorioSearchParams searchParam)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spGetTerritorios", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<Territorio> TerritorioBuscarComponentesDivisionesTerritoriales(DivisionSearchParams searchParam)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spComponentesDeDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<Territorio> TerritorioInsUpd(Territorio data)
        {
            IEnumerable<Territorio> result = new List<Territorio>();
            try
            {
                result = PrestamosDB.ExecReaderSelSP<Territorio>("spInsUpdTerritorios", SearchRec.ToSqlParams(data));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

    }
}
