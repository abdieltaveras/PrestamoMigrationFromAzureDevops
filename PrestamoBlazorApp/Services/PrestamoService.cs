
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class PrestamosService : ServiceBase
    {
        readonly string apiUrl = "api/prestamos";

        public async Task<IEnumerable<Prestamo>> GetPrestamosAsync(PrestamosGetParams search)
        {
            var result = await GetAsync<Prestamo>(apiUrl, search);
            return result;
        }

        
        public PrestamosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SavePrestamo(Prestamo Prestamo)
        {
            try
            {
                await PostAsync<Prestamo>(apiUrl, Prestamo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
