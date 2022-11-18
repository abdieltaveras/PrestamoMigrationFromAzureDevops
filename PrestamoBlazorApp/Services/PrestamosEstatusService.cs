using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PcpUtilidades;

using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class PrestamosEstatusService : ServiceBase
    {
        readonly string apiUrl = "api/prestamoestatus";

        public PrestamosEstatusService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }
        public async Task<IEnumerable<PrestamoEstatusGet>> Get(PrestamoEstatusGetParams param)
        {
            var result = await GetAsync<PrestamoEstatusGet>(apiUrl + "/get", param);
            return result;
        }
        public async Task Save(PrestamoEstatus param)
        {
            try
            {
                await PostAsync<PrestamoEstatus>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }

    }
}
