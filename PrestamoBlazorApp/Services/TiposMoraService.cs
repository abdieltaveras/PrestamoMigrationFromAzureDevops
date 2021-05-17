
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class TiposMoraService : ServiceBase
    {
     
        string apiUrl = "api/tipomoras";
        public async Task<IEnumerable<TipoMora>> Get(TipoMoraGetParams search)
        {
            var result = await GetAsync<TipoMora>(apiUrl, new  { JsonGet = search.ToJson() });
            return result;
        }

        //public async Task<IEnumerable<Color>> Get()
        //{
        //    return await GetAsync<Color>(apiUrl, null);
        //}
        public TiposMoraService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveTipoMora(TipoMora tipoMora)
        {
            try
            {
                await PostAsync<TipoMora>(apiUrl, tipoMora);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
