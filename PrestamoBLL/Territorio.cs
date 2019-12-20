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
        public IEnumerable<TerritoriosConHijo> GetTerritorios(TerritorioGetParams searchParam)
        {
            IEnumerable<TerritoriosConHijo> result = new List<TerritoriosConHijo>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<TerritoriosConHijo>("spGetTerritorios", SearchRec.ToSqlParams(searchParam));
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
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<TerritoriosConHijo>("spGetDivisionesTerritoriales", SearchRec.ToSqlParams(searchParam));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

        public IEnumerable<TerritoriosConPadre> GetPaisesDivisionesTerritoriales(TerritorioGetParams searchParam)
        {
            IEnumerable<TerritoriosConPadre> result = new List<TerritoriosConPadre>();
            try
            {
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<TerritoriosConPadre>("spGetPaisesDeDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
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
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Territorio>("spGetTerritorios", SearchRec.ToSqlParams(searchParam));
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
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Territorio>("spComponentesDeDivisionTerritorial", SearchRec.ToSqlParams(searchParam));
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
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Territorio>("spInsUpdTerritorios", SearchRec.ToSqlParams(data));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }

    }
}
