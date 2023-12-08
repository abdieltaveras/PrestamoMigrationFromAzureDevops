using Blazored.LocalStorage;
using DevBox.Core.Classes.UI;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UIClient.Providers;

namespace UIClient.Services
{
    public class HttpServiceBase : IHttpServiceBase
    {
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] ILocalStorageService _localStorage { get; set; }
        [Inject] protected NotificationService _notificationsService { get; set; }
        //string TokenName => "NekoThtua";
        string TokenName => ConstsForProviders.TokenName;
        /// <summary>
        /// Semicolon separated list of action values
        /// </summary>
        protected string ActionsValues { get; private set; }

        protected readonly IHttpClientFactory _clientFactory;
        protected HttpServiceBase(string actionsValues, IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _localStorage = localStorageService;
            _notificationsService = notificationService;
            ActionsValues = actionsValues;
        }

        public async Task<TResult> PostAsync<@Type, TResult>(string endpoint, @Type body, object search = null, bool requiresAuth = true) where TResult : class
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var baseUrl = _configuration["BaseServerUrl"];
                var query = search.UrlEncode();
                var client = await genClient(requiresAuth, ActionsValues);
                var uri = new UriBuilder(baseUrl) { Path = endpoint, Query = query }.ToString();
                var json = JsonSerializer.Serialize(body, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
                var response = await client.SendAsync(requestMessage);
                var resultStream = await response.Content.ReadAsStringAsync();
                if (resultStream.Length == 0) { return null; }
                var result = (TResult)JsonSerializer.Deserialize(resultStream, typeof(TResult), options); //DeserializeAsync<TResult>(resultStream);
                return result;
            }
            catch (Exception ex)
            {                
                _notificationsService.NotifyError("Error mientras se ejecutaba la operación", ex);
                return null;
            }
        }

        private async Task<HttpClient> genClient(bool requiresAuth, string actionsValues)
        {
            var client = _clientFactory.CreateClient();
            if (requiresAuth)
            {
                var token = await GetAuthToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            client.DefaultRequestHeaders.Add("actionsValues", actionsValues);
            return client;
        }
        protected async Task<string> GetAuthToken()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(TokenName);
            return savedToken;
        }
        public async Task<string> DelAsync(string endpoint, object search, bool requiresAuth = true)
        {
            var baseUrl = _configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");
            var client = await genClient(requiresAuth, ActionsValues);

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
        public async Task<IEnumerable<@Type>> GetAsync<@Type>(string endpoint, object search, bool requiresAuth = true)
        {
            var baseUrl = _configuration["BaseServerUrl"];
            var query = search.UrlEncode();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{endpoint}?{query}");
            request.Headers.Add("Accept", "application/json");

            IEnumerable<@Type> result;

            var client = await genClient(requiresAuth, ActionsValues);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                using var responseStream = await response.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<IEnumerable<@Type>>(responseStream, options);
                //result = await response.Content.ReadFromJsonAsync<IEnumerable<@Type>>();
            }
            else
            {
                result = Array.Empty<@Type>();
                var ex = new Exception($"ErrorCode:'{response.StatusCode}', Error:'{response.ReasonPhrase}'");
                await _localStorage.SetItemAsStringAsync("ERROR", ex.Message);
                //_notificationsService.NotifyError("Error al optener datos", ex);
            }
            return result;
        }
    }
}
