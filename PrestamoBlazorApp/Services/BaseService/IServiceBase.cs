using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services.BaseService
{
    public interface IServiceBase
    {
        Task<T> PostAsync<T>(string endpoint, object body, object search = null);
        Task<IEnumerable<T>> GetAsync<T>(string endpoint, object search);
        Task<string> DelAsync(string endpoint, object search, bool requiresAuth = true);
        Task<HttpResponseMessage> ReportGenerate(IJSRuntime jSRuntime, string endpoint, object search);
    }
}
