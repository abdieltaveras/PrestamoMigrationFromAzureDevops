using DevBox.Core.Classes.UI;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test1
{
    public class ServiceBase : IHttpServiceBase
    {
        [Inject] IConfiguration Configuration { get; set; }

        protected readonly IHttpClientFactory _clientFactory;
        protected ServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
        }
        public async Task<TResult> PostAsync<Type, TResult>(string endpoint, Type body, object search = null, bool requiresAuth = true) where TResult : class
        {
            var baseUrl = Configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var client = _clientFactory.CreateClient();
            var uri = new UriBuilder(baseUrl) { Path = endpoint, Query = query }.ToString();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = JsonSerializer.Serialize(body, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            var response = await client.SendAsync(requestMessage);
            var resultStream = await response.Content.ReadAsStringAsync();
            if (resultStream.Length == 0) { return null; }
            var result = (TResult)JsonSerializer.Deserialize(resultStream, typeof(TResult), options); //DeserializeAsync<TResult>(resultStream);
            return result;
        }
        public async Task<IEnumerable<Type>> GetAsync<Type>(string endpoint, object search, bool requiresAuth = true)
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
        }

}