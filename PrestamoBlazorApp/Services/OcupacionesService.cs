
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
    public class OcupacionesService : ServiceBase
    {
        string apiUrl = "api/Ocupaciones";
        public async Task<IEnumerable<Ocupacion>> Get(OcupacionGetParams ocupacionGetParams)
        {

            var result = await GetAsync<Ocupacion>(apiUrl+"/get", new { JsonGet = ocupacionGetParams.ToJson() });
            return result;
        }

        public async Task<IEnumerable<Ocupacion>> GetAll()
        {
            return await GetAsync<Ocupacion>(apiUrl+"/get", new OcupacionGetParams());
        }
        public OcupacionesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveOcupacion(Ocupacion Ocupacion)
        {
            try
            {
                await PostAsync<Ocupacion>(apiUrl+"/post", Ocupacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
