
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class ModelosService : ServiceBase
    {
        string apiUrl = "api/Modelos";
        public async Task<IEnumerable<Modelo>> GetModelosAsync(ModeloGetParams search)
        {
            var result = await GetAsync<Modelo>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Modelo>> GetAll()
        {
            var result =  await GetAsync<Modelo>(apiUrl+"/getall", null);
            return result;
        }
        public ModelosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveModelo(Modelo Modelo)
        {
            try
            {
                await PostAsync<Modelo>(apiUrl, Modelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
