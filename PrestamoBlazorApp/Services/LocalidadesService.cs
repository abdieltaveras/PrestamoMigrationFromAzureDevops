
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class LocalidadesService : ServiceBase
    {
        public  DivisionTerritorialService territoriosService { get; set; } 
        readonly string apiUrl = "api/localidades";

        public async Task<IEnumerable<DivisionTerritorial>> GetComponentesTerritorio()
        {
            var result = await territoriosService.GetTiposDivisionTerritorial();
            return result;
        }
        public async Task<ResponseDataFE<IEnumerable<Localidad>>> GetLocalidadesComponents(LocalidadesComponentGetParams param)
        {
            var result = await CustomGetAsync<ResponseDataFE<IEnumerable<Localidad>>>(apiUrl + "/GetLocalidadesComponents", param);
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
            territoriosService = new DivisionTerritorialService(clientFactory, configuration);
        }

        public async Task SaveLocalidad(Localidad Localidad)
        {
            try
            {
                await PostAsync<Localidad>(apiUrl+"/post", Localidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
      
    }
}
