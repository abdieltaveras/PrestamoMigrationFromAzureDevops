using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
namespace PrestamoBlazorApp.Services
{
    public class CommonInjectionsService
    {

        public IHttpClientFactory HttpClientFactory { get; }
        public IConfiguration Configuration { get; }
        public ILocalStorageService LocalStorageService { get; }

        public CommonInjectionsService(IHttpClientFactory clientFactory, IConfiguration configuration,ILocalStorageService localStorageService)
        {
            this.HttpClientFactory = clientFactory;
            this.Configuration = configuration;
            this.LocalStorageService = localStorageService;
        }
    }
    
}
