
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
    public class CustomService : ServiceBase
    {
        private string ApiUrl = "api/user";
        public HttpClient HttpClient { get; set; }
        //public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        public CustomService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {
            HttpClient = clientFactory.CreateClient();
        }
       
    }
}
