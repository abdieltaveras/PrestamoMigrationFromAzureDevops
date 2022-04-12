
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
        string apiUrl = "api/catalogo";
        string apiReportUrl = "api/reportes";
        public CatalogosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }
        public async Task SaveCatalogo(CatalogoInsUpd catalogo)
        {
            apiUrl = "api/Ocupaciones";
            try
            {
                await PostAsync<CatalogoInsUpd>(apiUrl+"/Post", catalogo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
        public async Task DeleteCatalogo(BaseCatalogoDeleteParams catalogo)
        {
            apiUrl = "api/Ocupaciones";
            await  PostAsync(apiUrl + "/Delete", catalogo);
        }
        //public async Task<IEnumerable<T>> Get<T>(CatalogoGetParams search) where T : class
        //{
        //    var d =  await GetAsync<T>(apiUrl, new { JsonGet = search.ToJson() });
        //    return d;
        //}
        public async Task<IEnumerable<CatalogoInsUpd>> Get(CatalogoGetParams search) 
        {
            var d = await GetAsync<CatalogoInsUpd>(apiUrl+"/get", search);
            return d;
        }

        //}
        public async Task<IEnumerable<CatalogoInsUpd>> Get2(BaseCatalogoGetParams search)
        {
            apiUrl = "api/Ocupaciones";
            var d = await GetAsync<CatalogoInsUpd>(apiUrl + "/get", search);
            return d;
        }
        public async Task<string> ReportListado(IJSRuntime jSRuntime,CatalogoReportGetParams search)
        {
            var d = await ReportGenerate(jSRuntime,apiReportUrl + "/CatalogoReportList", search);
            return d.StatusCode.ToString();
        }
    }
}
