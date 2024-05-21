
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrestamoBlazorApp.Shared;

using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class TestService : ServiceBase
    {
      
        string apiUrl = "api/OnClickAdd";

        public TestService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration) { }

        
        public async Task EncodeNullParams()
        {
            var param = new EncodeObjectTest();
            param.Edad = 20;
            await PostAsync<EncodeObjectTest>(apiUrl + "/EncodeNullParams", param);
        }

        public async Task<int> GetTest01(int seconds)
        {
            Thread.Sleep(seconds);
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            //var result = await GetAsync<int>(apiUrl+"/test01",new { seconds = seconds} );
            return 1;
        }

        public async Task<IEnumerable<int>> GetTest02(int seconds)
        {
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            var result = await GetAsync<int>(apiUrl + "/test01", new { seconds = seconds });
            return result;
        }

        public async Task<string> GetTest03(int seconds)
        {
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            var result = await GetAsync<int>(apiUrl + "/test01", new { seconds = seconds });
            return "Proceso terminado";
        }


        public async Task<IEnumerable<LocalidadNegocio>> GetLocalidadesNegocioTest()
        {
            //var result = await GetAsync<Marca>(apiUrl, new { JsonGet = marcaGetParams.ToJson() });
            var result = await GetAsync<LocalidadNegocio>(apiUrl + "/GetLocalidadesNegocio", 
                null);
            return result;
        }



    }
}
