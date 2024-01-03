using Blazored.LocalStorage;
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
    public class CodeudoresService : ServiceBase
    {
        readonly string apiUrl = "api/codeudores";
        string apiReportUrl = "api/reports";
        public async Task<IEnumerable<Codeudor>> SearchCodeudores(string search, bool cargarImagenes)
        {
            var result = await GetAsync<Codeudor>(apiUrl + "/searchCodeudores", new { textoABuscar = search, cargarImagenes = cargarImagenes });
            return result;
        }
        public async Task<IEnumerable<Codeudor>> GetCodeudoresAsync(CodeudorGetParams search, bool convertToObj=false)
        {
            var result = await GetAsync<Codeudor>(apiUrl+"/get", new { jsonGet = search.ToJson(), convertToObj= convertToObj } );
            //var result = await GetAsync<Cliente>(apiUrl + "/getByParam", search);
            return result;
        }
        
        public CodeudoresService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {

        }

        public async Task Post(Codeudor param)
        {
            try
            {
                await PostAsync<Codeudor>(apiUrl+"/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
        public async Task<string> ReportListado(IJSRuntime jSRuntime, BaseReporteParams search)
        {
            var d = await ReportGenerate(jSRuntime, apiReportUrl + "/CodeudoresReportList", search);
            return d.StatusCode.ToString();
        }
    }
}
