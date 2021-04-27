
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class CatalogosService : ServiceBase
    {
        string apiUrl = "api/color";
        public async Task<IEnumerable<Color>> GetColoresAsync(ColorGetParams search)
        {
            var result = await GetAsync<Color>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Color>> Get()
        {
            return await GetAsync<Color>(apiUrl, null);
        }
        public CatalogosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveCatalogo(Catalogo catalogo)
        {
            try
            {
                await PostAsync<Catalogo>(apiUrl, catalogo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
