
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PcpUtilidades;
using PrestamoBlazorApp.Shared;

using PrestamoEntidades;
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
            var result = await GetAsync<TipoMora>(apiUrl + "/get", search);
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
                await PostAsync<TipoMora>(apiUrl+"/post", tipoMora);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
