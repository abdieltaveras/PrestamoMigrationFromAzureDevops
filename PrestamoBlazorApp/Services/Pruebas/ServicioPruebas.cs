using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace PrestamoBlazorApp.Services.Pruebas
{
    public class ServicioPruebas : ServiceBase
    {
        public ServicioPruebas(IHttpClientFactory clientFactory, IConfiguration configuration) : 
            base(clientFactory, configuration)
        {
        }

        public string GetName(string name)
        {
            return name;
        }
    }
}
