using DevBox.Core.DAL.SQLServer;
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
        /// <summary>
        /// obtiene las localidades indicando sus localidades Padres
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IEnumerable<Localidad> GetLocalidades(LocalidadGetParams searchParam)
        {
            return BllAcciones.GetData<Localidad, LocalidadGetParams>(searchParam, "spGetLocalidades", GetValidation);
        }
        /// <summary>
        /// obtiene las localidades indicando sus localidades Padres
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IEnumerable<Localidad> GetLocalidadesConSusPadres(LocalidadGetParams searchParam)
        {
            return BllAcciones.GetData<Localidad, LocalidadGetParams>(searchParam, "spGetLocalidadesConSusPadres", GetValidation);
        }

        /// <summary>
        /// Busca las localidades padres y devuelve el nombre de la localida mas los padres de ellas, permitiendo
        /// identificar si el nombre de una localidad existe para mas de una localidad padre, identificar que claramente
        /// se selecciono o es la correct ejemplo villa Duarte en Santo Domingo o villa Duarte en Santiago
        /// </summary>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public string GetFullNameLocalidad(int idLocalidad)
        {
            var result = BLLPrestamo.Instance.GetLocalidadesConSusPadres(new LocalidadGetParams { IdLocalidad = idLocalidad});
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName= string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }
        public void InsUpdLocalidad(Localidad insUpdParam)
        {
            BllAcciones.InsUpdData<Localidad>(insUpdParam, "spInsUpdLocalidad");
        }
        public IEnumerable<BuscarLocalidad> SearchLocalidad(BuscarLocalidadParams searchParam)
        {
            return BllAcciones.GetData<BuscarLocalidad, BuscarLocalidadParams>(searchParam, "spBuscarLocalidad", GetValidation);
        }
        public IEnumerable<string> SearchLocalidadByName(BuscarNombreLocalidadParams searchParam)
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

        public IEnumerable<Localidad> GetPaises(LocalidadPaisesGetParams searchParam)
        {
            return BllAcciones.GetData<Localidad, LocalidadPaisesGetParams>(searchParam, "LocalidadPaisesSpGet", GetValidation);
        }

        public IEnumerable<LocalidadesHijas> GetHijasLocalidades(LocalidadGetParams searchParam)
        {
            return BllAcciones.GetData<LocalidadesHijas, LocalidadGetParams>(searchParam, "LocalidadLocalidadesHijasDeLocalidadSpGet", GetValidation);
        }
    }
}
