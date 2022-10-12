
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
    public class EstatusService : ServiceBase
    {
     
        string apiUrl = "api/estatus";
        public async Task<IEnumerable<Estatus>> Get(EstatusGetParams search)
        {
            var result = await GetAsync<Estatus>(apiUrl + "/get", search);
            return result;
        }
        //public async Task<IEnumerable<Estatus>> ListEstatus()
        //{
        //    var result = await GetAsync<Estatus>(apiUrl + "/ListEstatus", null);
        //    return result;
        //}
        //ListEstatus
        //public async Task<IEnumerable<Color>> Get()
        //{
        //    return await GetAsync<Color>(apiUrl, null);
        //}
        public EstatusService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task Save(Estatus param)
        {
            try
            {
                await PostAsync<Estatus>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
