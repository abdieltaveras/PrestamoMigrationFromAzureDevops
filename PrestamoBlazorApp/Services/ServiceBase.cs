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

namespace PrestamoBlazorApp.Services
{
    
    public abstract class ServiceBase
    {
        [Inject] IConfiguration Configuration { get; set; }

        protected readonly IHttpClientFactory _clientFactory;
        protected ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
        }
        protected async Task PostAsync<@Type>(string endpoint, @Type body, object search = null)
        {
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response=null;
            string errorMessage = string.Empty;
            
            response = await client.PostAsJsonAsync<@Type>($"{baseUrl}/{endpoint}?{query}", body);
            errorMessage = await response.Content.ReadAsStringAsync();
            
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase} {errorMessage}'");
            }
        }
        public async Task<IEnumerable<@Type>> GetAsync<@Type>(string endpoint, object search)
        {
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");

            IEnumerable<@Type> result;

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<IEnumerable<@Type>>(responseStream);
            }
            else
            {
                result = Array.Empty<@Type>();
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase}'");
            }
            return result;
        }
        public async Task<IEnumerable<string>> GetJsAsync(string endpoint, object search)
        {
           
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");

            IEnumerable<string> result;

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(responseStream);
            }
            else
            {
                
                throw new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase}'");
            }
            return result;
        }
    }
}
