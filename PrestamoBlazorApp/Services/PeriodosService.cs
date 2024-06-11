
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class PeriodosService : ServiceBase
    {
        BaseForCreateOrEdit BaseForCreateOrEdit;
        string apiUrl = "api/Periodos";
        
        public async Task<IEnumerable<Periodo>> Get(PeriodoGetParams search)
        {
            var result = await GetAsync<Periodo>(apiUrl+"/get", search);
            return result;
        }
        public PeriodosService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService) { }

        public async Task SavePeriodo(Periodo Periodo)
        {
            try
            {
                await PostAsync<Periodo>(apiUrl+"/post", Periodo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
