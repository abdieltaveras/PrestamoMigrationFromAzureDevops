using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class LocalidadesBLL:BaseBLL
    {
        public LocalidadesBLL(int idLocalidadNegocio, string loginName) : base(idLocalidadNegocio, loginName)
        {
        }

        /// <summary>
        /// obtiene las localidades indicando sus localidades Padres
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IEnumerable<Localidad> GetLocalidades(LocalidadGetParams searchParam)
        {
            return this.Get<Localidad>("spGetLocalidades",searchParam);
        }
        /// <summary>
        /// obtiene las localidades indicando sus localidades Padres
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public IEnumerable<Localidad> GetLocalidadesConSusPadres(LocalidadGetParams searchParam)
        {
            return this.Get<Localidad>("spGetLocalidadesConSusPadres", searchParam);
        }
        public ResponseData GetLocalidadesComponents(LocalidadesComponentGetParams searchParam)
        {
            try
            {
                var spName = "spGetLocalidadesComponents";
                var data = this.Get<Localidad>(spName, searchParam);
                if (data.Count() > 0)
                {
                    return new ResponseData(data,"Localidades");
                }
                return new ResponseData(false, "Data no encontrada", 404);
            }
            catch (Exception ex)
            {
                return new ResponseData(ex,false, ex.Message,400);
            }
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
            var result = this.GetLocalidadesConSusPadres(new LocalidadGetParams { IdLocalidad = idLocalidad});
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName= string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }
        public void InsUpdLocalidad(Localidad insUpdParam)
        {
            this.InsUpd("spInsUpdLocalidad", insUpdParam);
        }
        public IEnumerable<BuscarLocalidad> SearchLocalidad(BuscarLocalidadParams searchParam)
        {
            return this.Get<BuscarLocalidad>("spBuscarLocalidad", searchParam);
        }
        public IEnumerable<string> SearchLocalidadByName(BuscarNombreLocalidadParams searchParam)
        {
            return this.Get<string>("spGetLocalidadById", searchParam);

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
            return this.Get<Localidad>("LocalidadPaisesSpGet", searchParam);
        }

        public IEnumerable<LocalidadesHijas> GetHijasLocalidades(LocalidadGetParams searchParam)
        {
            return this.Get<LocalidadesHijas>("LocalidadLocalidadesHijasDeLocalidadSpGet", searchParam);
        }
    }
}
