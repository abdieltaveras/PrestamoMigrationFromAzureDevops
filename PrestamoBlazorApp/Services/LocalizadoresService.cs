
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class LocalizadoresService : ServiceBase
    {
        //public  DivisionTerritorialService territoriosService { get; set; } 
        readonly string apiUrl = "api/localizadores";

        public LocalizadoresService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {
        }

        public async Task<IEnumerable<Localizador>> Get(LocalizadorGetParams search)
        {
            var result = await GetAsync<Localizador>(apiUrl+"/get", search);
            return result;
        }


        public async Task Post(Localizador param)
        {
            try
            {
                await PostAsync<Localizador>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
      
    }
}
