using Blazored.LocalStorage;
using DevBox.Core.Classes.Identity;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UIClient.Providers;

namespace PrestamoBlazorApp.Providers
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject] ISiteResourcesService _siteResources { get; set; }
        [Inject] private NavigationManager _NavigationManager { get; set; }

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        //string TokenName => "NekoThtua";
        public string TokenName => ConstsForProviders.TokenName;

        public TokenAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage, ISiteResourcesService siteResource, NavigationManager navMan)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            // todo chequear como cambiar esta implementacion rigida
            _siteResources = siteResource;
            _NavigationManager = navMan;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _httpClient.DefaultRequestHeaders.Remove("bearer");
            string savedToken = await GetAuthToken();
            var tokenExpired = IsTokenExpired(savedToken);
            if (tokenExpired)
            {
                savedToken = null;
                await MarkUserAsLoggedOut();

            }
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity());

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                ClaimsPrincipal user = null;
                try
                {
                    user = TokenManager.GetPrincipal(savedToken);
                }
                catch (Exception e)
                {
                    var msj = e.Message;
                    throw;
                }

                if (user != null)

                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);
                    currentUser = user;
                    //IsTokenExpired(savedToken);
                    var isUserAuthenticated = user.Identity.IsAuthenticated;

                }
            }
            Thread.CurrentPrincipal = currentUser;
            var result = new AuthenticationState(currentUser);
            return result;
        }



        //public bool IsTokenExpired(string token)
        //{

        //    var expirationClaim = ParseClaimsFromJwt(token).Where(item => item.Type == ClaimTypes.Expiration).FirstOrDefault();
        //    var isExpired = true;
        //    if (expirationClaim != null)
        //    {
        //        var expirationTime = expirationClaim.Value;
        //        var ExpMomentDt = DateTime.Parse(expirationTime);
        //        isExpired = (DateTime.UtcNow.CompareTo(ExpMomentDt) > 0);
        //    }
        //    return isExpired;
        //}

        private bool IsTokenExpired(string savedToken)
        {
            var tokenExpired = true;
            if (savedToken != null)
            {
                var claims = ParseClaimsFromJwt(savedToken);
                var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
                var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
                tokenExpired = (datetime.UtcDateTime <= DateTime.UtcNow);
            }
            return tokenExpired;
        }

        public async Task<bool> IsTokenExpired()
        {
            string savedToken = await GetAuthToken();
            var tokenExpired = IsTokenExpired(savedToken);
            return tokenExpired;
        }

        public async Task LoggetOutWhenTokenExpiredAndnavigateTo(string url)
        {
            var isTokenExpired = await IsTokenExpired();
            if (isTokenExpired)
            {
                await MarkUserAsLoggedOut();
                _NavigationManager.NavigateTo(url);
            }
        }
        private void verifyotherRelatedClaims(
            IEnumerable<Claim> claims)
        {
            var notBefore = claims.Where(item => item.Type == "nbf").FirstOrDefault();
            var dtNotBefore = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(notBefore.Value));
            var issuedAt = claims.Where(item => item.Type == "iat").FirstOrDefault();
            var dtIssuedAt = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(issuedAt.Value));

        }

        public async Task<string> GetAuthToken()
        {
            string savedToken = null;
            try
            {
                savedToken = await _localStorage.GetItemAsync<string>(TokenName);
            }
            catch (Exception)
            {

            }

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

        public async Task MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            //PrLocalStorage.SetAsync(_siteResources.LoggedOutKey, _siteResources.LoggedOutValue);
            await _localStorage.SetItemAsStringAsync(_siteResources.LoggedOutKey, _siteResources.LoggedOutValue);
            await _localStorage.RemoveItemAsync(TokenName);
            //var x = result.Result;
            NotifyAuthenticationStateChanged(authState);
        }

        //private async Task<bool> IsTokenExpired()
        //{
        //    var token = await GetAuthToken();
        //    return false;
        //}

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var result = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            return result;
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
