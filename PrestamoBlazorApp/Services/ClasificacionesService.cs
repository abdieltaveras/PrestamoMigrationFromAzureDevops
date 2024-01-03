
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PcpUtilidades;

using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class ClasificacionesService : ServiceBase
    {
        string apiUrl = "api/Clasificacion";
        public async Task<IEnumerable<Clasificacion>> GetClasificacionesAsync(ClasificacionesGetParams search)
        {
            var result = await GetAsync<Clasificacion>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Clasificacion>> Get(ClasificacionesGetParams clasificacionesGetParams)
        {
            var result =  await GetAsync<Clasificacion>(apiUrl+"/get", clasificacionesGetParams);
            return result;
        }
        public ClasificacionesService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {

        }

        public async Task SaveClasificacion(Clasificacion Clasificacion)
        {
            try
            {
                await PostAsync<Clasificacion>(apiUrl+"/post", Clasificacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
