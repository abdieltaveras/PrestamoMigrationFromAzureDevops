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
        public IEnumerable<Localidad> LocalidadesGet(LocalidadGetParams searchParam)
        {
            return BllAcciones.GetData<Localidad, LocalidadGetParams>(searchParam, "spGetLocalidades", GetValidation);
        }
        public void LocalidadInsUpd(Localidad insUpdParam)
        {
            BllAcciones.InsUpdData<Localidad>(insUpdParam, "spInsUpdLocalidad");
        }

        public IEnumerable<BuscarLocalidad> LocalidadSearch(BuscarLocalidadParams searchParam)
        {
            return BllAcciones.GetData<BuscarLocalidad, BuscarLocalidadParams>(searchParam, "spGetLocalidades", GetValidation);
        }

        public IEnumerable<string> LocalidadSearchName(BuscarNombreLocalidadParams searchParam)
        {
            return BllAcciones.GetData<string, BuscarNombreLocalidadParams>(searchParam, "spGetLocalidades", GetValidation);

            //List<string> result = new List<string>();
            //try
            //{
            //    result = PrestamosDB.ExecReaderSelSP<string>("spGetLocalidadById", SearchRec.ToSqlParams(searchParam));
            //}
            //catch (Exception e)
            //{
            //    //DatabaseError(e);
            //    throw e;
            //}
            //return result;
        }
    }
}
