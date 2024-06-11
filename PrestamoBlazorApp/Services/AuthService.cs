
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PcpUtilidades;

using PrestamoEntidades;
using PrestamoEntidades.Auth;
using PrestamoEntidades.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class AuthService : ServiceBase
    {
        private string ApiUrl = "api/user";
        //public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {

        }
        public async Task<CustomHttpResponseFE<LoginResponseDto>> Login(LoginCredentialsDto loginCredentials)
        {
            
            try
            {
                var response = await PostCustomResponseAsync<LoginResponseDto>(ApiUrl+ "/LogIn", loginCredentials);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
    }
}
