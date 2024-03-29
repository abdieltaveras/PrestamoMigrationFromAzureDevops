using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class CodigosCargosDebitosReservadosService : ServiceBase
    {
        readonly string apiUrl = "api/CodigosCargos";

        public CodigosCargosDebitosReservadosService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration, localStorageService)
        {

        }
        public async Task<IEnumerable<CodigosCargosDebitosReservados>> Get(CodigosCargosGetParams param)
        {
            var result = await GetAsync<CodigosCargosDebitosReservados>(apiUrl + "/get", param);
            return result;
        }
        public async Task Save(CodigosCargosDebitosReservados param)
        {
            try
            {
               var result =  await PostAsync<int>(apiUrl + "/post", param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
