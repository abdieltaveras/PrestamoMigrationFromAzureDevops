
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class NegociosService : ServiceBase
    {
        
        readonly string apiUrl = "api/negocios";
       
        public NegociosService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {
        }

        public async Task<IEnumerable<Negocio>> Get(NegociosGetParams param) 
        {
            var result = await GetAsync<Negocio>(apiUrl + "/get", param);
            return result;
        }

        public async Task Post(Negocio param)
        {
            try
            {
                await PostAsync<Negocio>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
      
    }
}
