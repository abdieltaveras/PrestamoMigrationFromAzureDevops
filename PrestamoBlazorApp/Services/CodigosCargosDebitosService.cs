using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class CodigosCargosDebitosService : ServiceBase
    {
        readonly string apiUrl = "api/CodigosCargos";

        public CodigosCargosDebitosService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration, localStorageService)
        {

        }
        public async Task<IEnumerable<CodigosCargosDebitos>> Get(CodigosCargosGetParams param)
        {
            var result = await GetAsync<CodigosCargosDebitos>(apiUrl + "/get", param);
            return result;
        }
        public async Task Save(CodigosCargosDebitos param)
        {
            try
            {
               var result =  await PostAsync<int>(apiUrl + "/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
