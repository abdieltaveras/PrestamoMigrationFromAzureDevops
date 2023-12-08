using Blazored.LocalStorage;
using DevBox.Core.Classes.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace UIClient.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        
        string TokenName => ConstsForProviders.TokenName;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _httpClient.DefaultRequestHeaders.Remove("bearer");
            var savedToken = await _localStorage.GetItemAsync<string>(TokenName);
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                var user = TokenManager.GetPrincipal(savedToken);
                if (user != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);
                    currentUser = user;
                }
            }
            Thread.CurrentPrincipal = currentUser;
            return new AuthenticationState(currentUser);
        }
        public async Task<string> GetAuthToken()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(TokenName);
            return savedToken;
        }
        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await _localStorage.RemoveItemAsync(TokenName);
            }
            else
            {
                await _localStorage.SetItemAsStringAsync(TokenName, token);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            _localStorage.RemoveItemAsync(TokenName);
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
