using emtSoft.DAL;
using PrestamoBLL.Entidades;
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

        /// <summary>
        /// Busca las localidades padres y devuelve el nombre de la localida mas los padres de ellas, permitiendo
        /// identificar si el nombre de una localidad existe para mas de una localidad padre, identificar que claramente
        /// se selecciono o es la correct ejemplo villa Duarte en Santo Domingo o villa Duarte en Santiago
        /// </summary>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public string LocalidadGetFullName(int idLocalidad)
        {
            var result = BLLPrestamo.Instance.LocalidadesGet(new LocalidadGetParams { IdLocalidad = idLocalidad});
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName= string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }
        public void LocalidadInsUpd(Localidad insUpdParam)
        {
            BllAcciones.InsUpdData<Localidad>(insUpdParam, "spInsUpdLocalidad");
        }
        public IEnumerable<BuscarLocalidad> LocalidadSearch(BuscarLocalidadParams searchParam)
        {
            return BllAcciones.GetData<BuscarLocalidad, BuscarLocalidadParams>(searchParam, "spBuscarLocalidad", GetValidation);
        }
        public IEnumerable<string> LocalidadSearchName(BuscarNombreLocalidadParams searchParam)
        {
            return BllAcciones.GetData<string, BuscarNombreLocalidadParams>(searchParam, "spGetLocalidadById", GetValidation);

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

        public IEnumerable<Localidad> PaisesGet(LocalidadPaisesGetParams searchParam)
        {
            return BllAcciones.GetData<Localidad, LocalidadPaisesGetParams>(searchParam, "LocalidadPaisesSpGet", GetValidation);
        }

        public IEnumerable<LocalidadesHijas> LocalidadesHijasGet(LocalidadGetParams searchParam)
        {
            return BllAcciones.GetData<LocalidadesHijas, LocalidadGetParams>(searchParam, "LocalidadLocalidadesHijasDeLocalidadSpGet", GetValidation);
        }

        
    }
}
