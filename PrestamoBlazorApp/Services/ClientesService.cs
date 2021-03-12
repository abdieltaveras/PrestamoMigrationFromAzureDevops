
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class ClientesService : ServiceBase
    {
        readonly string apiUrl = "api/clientes";

        public async Task<IEnumerable<Cliente>> GetClientesAsync(ClienteGetParams search)
        {
            var result = await GetAsync<Cliente>(apiUrl, search);
            return result;
        }

        
        public ClientesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveCliente(Cliente cliente)
        {
            try
            {
                await PostAsync<Cliente>(apiUrl, cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
