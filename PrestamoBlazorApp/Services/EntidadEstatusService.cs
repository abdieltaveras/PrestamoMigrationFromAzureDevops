
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
    public class EntidadEstatusService : ServiceBase
    {
     
        string apiUrl = "api/entidadestatus";
        public async Task<IEnumerable<EntidadEstatus>> Get(EntidadEstatusGetParams search)
        {
            var result = await GetAsync<EntidadEstatus>(apiUrl + "/get", search);
            return result;
        }

        //public async Task<IEnumerable<Color>> Get()
        //{
        //    return await GetAsync<Color>(apiUrl, null);
        //}
        public EntidadEstatusService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task Save(EntidadEstatus param)
        {
            try
            {
                await PostAsync<EntidadEstatus>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
