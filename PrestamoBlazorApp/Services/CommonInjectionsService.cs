using Microsoft.Extensions.Configuration;
using System.Net.Http;
namespace PrestamoBlazorApp.Services
{
    public class CommonInjectionsService
    {

        public IHttpClientFactory HttpClientFactory { get; }
        public IConfiguration Configuration { get; }
        public CommonInjectionsService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.HttpClientFactory = clientFactory;
            this.Configuration = configuration;
        }
    }
    
}
