
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class LocalidadesNegociosService : ServiceBase
    {
        
        readonly string apiUrl = "api/localidadesnegocios";
       
        public LocalidadesNegociosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {
        }

        public async Task<IEnumerable<LocalidadNegocio>> Get(LocalidadNegociosGetParams param) 
        {
            var result = await GetAsync<LocalidadNegocio>(apiUrl + "/get", param);
            return result;
        }

        public async Task Post(LocalidadNegocio param)
        {
            try
            {
                await PostAsync<LocalidadNegocio>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
      
    }
}
