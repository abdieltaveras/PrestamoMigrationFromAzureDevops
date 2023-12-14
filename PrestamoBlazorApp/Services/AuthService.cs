
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PcpUtilidades;

using PrestamoEntidades;
using PrestamoEntidades.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class AuthService : ServiceBase
    {
        private string ApiUrl = "api/user";
    
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }
        public async Task<LoginResponseDto> Login(LoginCredentialsDto loginCredentials)
        {
            
            try
            {
                var response = await PostAsync<LoginResponseDto>(ApiUrl+ "/LogIn", loginCredentials);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
    }
}
