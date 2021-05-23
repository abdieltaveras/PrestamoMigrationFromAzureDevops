
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class LocalidadesService : ServiceBase
    {
        public  TerritoriosService territoriosService { get; set; } 
        readonly string apiUrl = "api/localidades";

        public async Task<IEnumerable<Territorio>> GetComponentesTerritorio()
        {
            var result = await territoriosService.GetComponenteDeDivision();
            return result;
        }
        public async Task<IEnumerable<Localidad>> Get(LocalidadGetParams search)
        {
            var result = await GetAsync<Localidad>(apiUrl+"/get", search);
            return result;
        }

        public async Task<IEnumerable<BuscarLocalidad>> BuscarLocalidad(BuscarLocalidadParams searchParam)
        {
            var result = await GetAsync<BuscarLocalidad>(apiUrl+"/buscarLocalidad", searchParam );
            return result;
        }
        public async Task<IEnumerable<LocalidadesHijas>> GetHijasLocalidades(int idlocalidad)
        {
            var result = await GetAsync<LocalidadesHijas>(apiUrl + "/GetHijasLocalidades", new { idLocalidad = idlocalidad });
            return result;
        }
        
        public LocalidadesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {
            territoriosService = new TerritoriosService(clientFactory, configuration);
        }

        public async Task SaveLocalidad(Localidad Localidad)
        {
            try
            {
                await PostAsync<Localidad>(apiUrl, Localidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
      
    }
}
