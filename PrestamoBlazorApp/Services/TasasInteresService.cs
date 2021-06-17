
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PcpUtilidades;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class TasasInteresService : ServiceBase
    {
      
        string apiUrl = "api/TasaInteres";
        
        public async Task<IEnumerable<TasaInteres>> Get(TasaInteresGetParams search)
        {
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            var result = await GetAsync<TasaInteres>(apiUrl, new { JsonGet = search.ToJson() });
            return result;
        }

        
        public TasasInteresService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration) { }

        public async Task SaveTasaInteres(TasaInteres TasaInteres)
        {
            try
            {
                await PostAsync<TasaInteres>(apiUrl, TasaInteres);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
