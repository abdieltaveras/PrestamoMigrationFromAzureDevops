
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class TipoGarantiaService : ServiceBase
    {
        string apiUrl = "api/tipogarantia";
        public async Task<IEnumerable<TipoGarantia>> Get(TipoGetParams search)
        {
            var result = await GetAsync<TipoGarantia>(apiUrl, search);
            return result;
        }

        //public async Task<IEnumerable<Color>> Get()
        //{
        //    return await GetAsync<Color>(apiUrl, null);
        //}
        public TipoGarantiaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveColor(Color color)
        {
            try
            {
                await PostAsync<Color>(apiUrl, color);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
