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
    public class ClientesService : ServiceBase
    {
        readonly string apiUrl = "api/clientes";
        string apiReportUrl = "api/reports";
        public async Task<IEnumerable<Cliente>> SearchClientes(string search, bool cargarImagenes)
        {
            var result = await GetAsync<Cliente>(apiUrl + "/searchClientes", new { textoABuscar = search, cargarImagenes = cargarImagenes });
            return result;
        }
        public async Task<IEnumerable<Cliente>> GetClientesAsync(ClienteGetParams search, bool convertToObj=false)
        {
            var result = await GetAsync<Cliente>(apiUrl+"/get", new { jsonGet = search.ToJson(), convertToObj= convertToObj } );
            //var result = await GetAsync<Cliente>(apiUrl + "/getByParam", search);
            return result;
        }
        
        public ClientesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveCliente(Cliente cliente)
        {
            try
            {
                await PostAsync<Cliente>(apiUrl+"/post", cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
        public async Task<string> ReportListado(IJSRuntime jSRuntime, BaseReporteParams search)
        {
            var d = await ReportGenerate(jSRuntime, apiReportUrl + "/ClienteReportList", search);
            return d.StatusCode.ToString();
        }

        public async Task<string> ReportFicha(IJSRuntime jSRuntime, int idcliente, int reportType)
        {
            var d = await ReportGenerate(jSRuntime, apiUrl + "/ClienteReportInfo", new { idcliente= idcliente, reportType = reportType });
            return d.StatusCode.ToString();
        }
    }
}
