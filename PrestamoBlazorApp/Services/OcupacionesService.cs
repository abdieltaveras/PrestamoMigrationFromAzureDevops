
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class OcupacionesService : ServiceBase
    {
        string apiUrl = "api/Ocupacion";
        public async Task<IEnumerable<Ocupacion>> GetOcupacionesAsync(OcupacionGetParams search)
        {
            var result = await GetAsync<Ocupacion>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Ocupacion>> GetAll()
        {
            var result =  await GetAsync<Ocupacion>(apiUrl+"/getall", null);
            return result;
        }
        public OcupacionesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveOcupacion(Ocupacion Ocupacion)
        {
            try
            {
                await PostAsync<Ocupacion>(apiUrl, Ocupacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
