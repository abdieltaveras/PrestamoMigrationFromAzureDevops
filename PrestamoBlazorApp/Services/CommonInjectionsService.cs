using Microsoft.Extensions.Configuration;
using System.Net.Http;
namespace PrestamoBlazorApp.Services
{
    public class CommonInjectionsService
    {

        public IHttpClientFactory ClientFactory { get; }
        public IConfiguration Configuration { get; }
        public CommonInjectionsService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.ClientFactory = clientFactory;
            this.Configuration = configuration;
        }
    }
    
}
