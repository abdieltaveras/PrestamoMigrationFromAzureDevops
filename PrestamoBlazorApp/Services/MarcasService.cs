
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class MarcasService : ServiceBase
    {
        string apiUrl = "api/marca";
        public async Task<IEnumerable<Marca>> GetMarcasAsync(MarcaGetParams search)
        {
            var result = await GetAsync<Marca>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Marca>> GetAll()
        {
            return await GetAsync<Marca>(apiUrl+"/getall", null);
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
