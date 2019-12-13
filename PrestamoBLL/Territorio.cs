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
