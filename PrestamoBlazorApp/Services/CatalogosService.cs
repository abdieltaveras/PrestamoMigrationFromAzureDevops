
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PcpUtilidades;
using PrestamoBLL;
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
       
        public CatalogosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveCatalogo(Catalogo catalogo)
        {
            try
            {
                await PostAsync<Catalogo>(apiUrl, catalogo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
        //public async Task<IEnumerable<T>> Get<T>(CatalogoGetParams search) where T : class
        //{
        //    var d =  await GetAsync<T>(apiUrl, new { JsonGet = search.ToJson() });
        //    return d;
        //}
        public async Task<IEnumerable<Catalogo>> Get(CatalogoGetParams search) 
        {
            var d = await GetAsync<Catalogo>(apiUrl+"/get", new { JsonGet = search.ToJson() });
            return d;
        }
    }
}
