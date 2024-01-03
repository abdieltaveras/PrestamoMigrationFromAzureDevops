
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PcpUtilidades;
using PrestamoBlazorApp.Shared;
using System.Linq;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace PrestamoBlazorApp.Services
{
    public class TasasInteresService : ServiceBase
    {
      
        string apiUrl = "api/TasaInteres";
        
        public async Task<IEnumerable<TasaInteres>> Get(TasaInteresGetParams search)
        {
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            var result = await GetAsync<TasaInteres>(apiUrl+"/get", search);
            return result;
        }

        public async Task<TasasInteresPorPeriodos> GetTasaInteresPorPeriodo(int idPeriodo, int idTasaDeInteres)
        {
            var result = await GetAsync<TasasInteresPorPeriodos>(apiUrl + "/GetTasaInteresPorPeriodo", new { idTasaDeInteres = idTasaDeInteres, idPeriodo = idPeriodo });

            return result.FirstOrDefault();
        }
        public TasasInteresService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService) { }

        public async Task SaveTasaInteres(TasaInteres TasaInteres)
        {
            try
            {
                await PostAsync<TasaInteres>(apiUrl+"/post", TasaInteres);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
