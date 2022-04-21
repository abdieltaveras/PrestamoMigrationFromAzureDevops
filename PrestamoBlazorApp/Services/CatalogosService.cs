
using Microsoft.AspNetCore.Mvc;
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
    public class CatalogosService : ServiceBase
    {
        private string ApiUrl { get; }
        string apiReportUrl = "api/reportes";
        public CatalogosService(IHttpClientFactory clientFactory, IConfiguration configuration, string apiUrl) : base(clientFactory, configuration)
        {
            this.ApiUrl = apiUrl;

        }
        public async Task SaveCatalogo(CatalogoInsUpd catalogo)
        {
            
            try
            {
                await PostAsync<CatalogoInsUpd>(ApiUrl+"/Post", catalogo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
        public async Task DeleteCatalogo(BaseCatalogoDeleteParams catalogo)
        {
            
            await  PostAsync(ApiUrl + "/Delete", catalogo);
        }
        //public async Task<IEnumerable<T>> Get<T>(CatalogoGetParams search) where T : class
        //{
        //    var d =  await GetAsync<T>(apiUrl, new { JsonGet = search.ToJson() });
        //    return d;
        //}
        public async Task<IEnumerable<CatalogoInsUpd>> Get(BaseCatalogoGetParams search) 
        {
            var d = await GetAsync<CatalogoInsUpd>(ApiUrl+"/get", search);
            return d;
        }

        //}
        public async Task<IEnumerable<CatalogoInsUpd>> Get2(BaseCatalogoGetParams search)
        {
            
            var d = await GetAsync<CatalogoInsUpd>(ApiUrl + "/get", search);
            return d;
        }
        public async Task<string> ReportListado(IJSRuntime jSRuntime,CatalogoReportGetParams search)
        {
            var d = await ReportGenerate(jSRuntime,apiReportUrl + "/CatalogoReportList", search);
            return d.StatusCode.ToString();
        }
    }
}
