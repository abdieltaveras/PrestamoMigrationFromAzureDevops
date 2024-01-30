using Blazored.LocalStorage;
using DevBox.Core.Classes.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIClient.Services;

namespace PrestamoBlazorApp.Services
{
    public class SystemPoliciesService : ServiceBase
    {
        const string endpoint="api/system/policies";

        public SystemPoliciesService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration, localStorageService)
        {
        }

        public async Task<IEnumerable<SysPolicyCategory>> GetPoliciesAsync()
        {
            try
            {
                var result = await GetAsync<SysPolicyCategory>(endpoint, new { });
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar acceder al servicio de datos.", ex);
            }
        }
        public async Task SavePolicies(SysPolicyCategory sysPolicy)
        {
            try
            {
                await PostAsync<object>(endpoint, sysPolicy);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar acceder al servicio de datos.", ex);
            }
            
        }


        //public SystemPoliciesService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, IJSRuntime jsRuntime, NotificationService notificationService) : 
        //    base("SysPolicies", clientFactory, configuration, localStorageService, jsRuntime, notificationService)
        //{

        //}
    }
}
