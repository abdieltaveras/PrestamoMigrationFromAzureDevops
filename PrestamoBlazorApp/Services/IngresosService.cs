
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class IngresosService : ServiceBase
    {
        public async Task<IEnumerable<Ingreso>> GetIngresosAsync(IngresoGetParams search)
        {
            var result = await GetAsync<Ingreso>("api/ingresos", search);
            return result;
        }

        public async Task<IEnumerable<Ingreso>> GetIngresosAsync(int id)
        {
            var search = new IngresoGetParams { IdIngreso = id };
            return await GetIngresosAsync(search);
        }
        public IngresosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveIngreso(Ingreso ingreso)
        {
            try
            {
                await PostAsync<Ingreso>("api/ingresos", ingreso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
