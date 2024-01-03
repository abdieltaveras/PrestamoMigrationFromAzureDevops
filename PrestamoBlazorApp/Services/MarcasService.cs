
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PcpUtilidades;

using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class MarcasService : ServiceBase
    {
        string apiUrl = "api/marcas";
        //JsonGlobalParam JsonGlobalParam = new JsonGlobalParam();
        //public async Task<IEnumerable<Marca>> GetMarcasAsync(MarcaGetParams search)
        //{
        //    var result = await GetAsync<Marca>(apiUrl, search);
        //    return result;
        //}

        public async Task<IEnumerable<Marca>> Get(MarcaGetParams marcaGetParams)
        {
            
            var result =  await GetAsync<Marca>(apiUrl+"/get", marcaGetParams);
            return result;
        }
        public MarcasService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {

        }

        public async Task SaveMarca(Marca Marca)
        {
            try
            {
                await PostAsync<Marca>(apiUrl+"/post", Marca);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
