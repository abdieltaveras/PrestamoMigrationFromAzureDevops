
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrestamoBLL.Entidades;
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
            marcaGetParams.JsonGet = JsonConvert.SerializeObject(marcaGetParams);
            var result =  await GetAsync<Marca>(apiUrl, marcaGetParams);
            return result;
        }
        public MarcasService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveMarca(Marca Marca)
        {
            try
            {
                await PostAsync<Marca>(apiUrl, Marca);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
