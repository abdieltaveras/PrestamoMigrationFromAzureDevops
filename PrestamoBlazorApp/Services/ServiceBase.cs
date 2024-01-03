using PrestamoBlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Services.BaseService;
using Blazored.LocalStorage;

namespace PrestamoBlazorApp.Services
{
    
    public class ServiceBase:IServiceBase
    {
       
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] ILocalStorageService _LocalStorageService { get; set; }

        [Inject] TokenAuthenticationStateProvider _AuthStateProvider { get; set; }
        JsInteropUtils JsInteropUtils { get; set; }
        protected readonly IHttpClientFactory _clientFactory;
        protected ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
            _LocalStorageService = localStorageService;
        }
        public ServiceBase( TokenAuthenticationStateProvider authToken)
        {
            _AuthStateProvider = authToken;
        }
        public async Task<@Type> PostAsync<@Type>(string endpoint, object body, object search = null)
        {
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response=null;
            string errorMessage = string.Empty;
            string token = await _LocalStorageService.GetItemAsync<string>(ConstsForProviders.TokenName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            response = await client.PostAsJsonAsync($"{baseUrl}/{endpoint}?{query}", body);
            var result = await response.Content.ReadFromJsonAsync<Type>();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase} {errorMessage}'");
            }
            return result;
        }
        public async Task<IEnumerable<@Type>> GetAsync<@Type>(string endpoint, object search)
        {

            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");

            IEnumerable<@Type> result;

            var client = _clientFactory.CreateClient();
            string token = await _LocalStorageService.GetItemAsync<string>(ConstsForProviders.TokenName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //using var responseStream = await response.Content.ReadAsStreamAsync();
                //result = await JsonSerializer.DeserializeAsync<IEnumerable<@Type>>(responseStream);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                result = await response.Content.ReadFromJsonAsync<IEnumerable<@Type>>(options);
            }
            else
            {
                result = Array.Empty<@Type>();
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase}'");
            }
            return result;
        }



        public async Task<string> DelAsync(string endpoint, object search, bool requiresAuth = true)
        {
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");
            var client = _clientFactory.CreateClient();
            //var client = await genClient(requiresAuth, ActionsValues);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return "";
            }
            else
            {
                return response.ReasonPhrase;
            }
        }


        public async Task<HttpResponseMessage> ReportGenerate(IJSRuntime jSRuntime,string endpoint, object search)
        {

            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage result;

            var client = _clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(4);
            string token = await _LocalStorageService.GetItemAsync<string>(ConstsForProviders.TokenName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.SendAsync(request);
            await JsInteropUtils.GoToUrl(jSRuntime,$"{baseUrl}/{endpoint}?{query}");
            if (response.IsSuccessStatusCode)
            {
                result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase}'");
            }
            return result;
        }
    }
}
