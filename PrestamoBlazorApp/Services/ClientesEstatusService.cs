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
    public class ClientesEstatusService : ServiceBase
    {
        readonly string apiUrl = "api/clienteestatus";

        public ClientesEstatusService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }
        public async Task<IEnumerable<ClienteEstatusGet>> Get(ClienteEstatusGetParams param)
        {
            var result = await GetAsync<ClienteEstatusGet>(apiUrl + "/get", param);
            return result;
        }
        public async Task Save(ClienteEstatus param)
        {
            try
            {
                await PostAsync<ClienteEstatus>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }

    }
}
