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
        public IEnumerable<Territorio> GetTerritorios(TerritorioGetParams searchParam)
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

        public IEnumerable<Territorio> GetDivisionesTerritoriales(TerritorioGetParams searchParam)
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

        public IEnumerable<Territorio> GetPaisesDivisionesTerritoriales(TerritorioGetParams searchParam)
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

        public IEnumerable<Territorio> BuscarTerritoriosHijos(TerritorioSearchParams searchParam)
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

        public IEnumerable<Territorio> BuscarComponentesDivisionesTerritoriales(DivisionSearchParams searchParam)
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

        public IEnumerable<Territorio> GuardarTerritorio(Territorio data)
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
