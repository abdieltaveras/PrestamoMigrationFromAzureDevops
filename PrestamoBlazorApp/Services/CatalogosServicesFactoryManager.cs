using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class CatalogosServicesFactoryManager
    {
        private IHttpClientFactory HttpClientFactory { get; }
        private IConfiguration Configuration { get; }
        private ILocalStorageService LocalStorageService { get; }

        public CatalogosServicesFactoryManager(IHttpClientFactory clientFactory, IConfiguration configuration,ILocalStorageService localStorageService)
        {
            this.HttpClientFactory = clientFactory;
            this.Configuration = configuration;
            this.LocalStorageService = localStorageService;
        }
        public CatalogosService OcupacionesService => new CatalogosService(this.HttpClientFactory, this.Configuration, LocalStorageService, "api/Ocupacion");

        public CatalogosService ColoresService => new CatalogosService(this.HttpClientFactory, this.Configuration, LocalStorageService, "api/Color");

        public CatalogosService TiposSexoService => new CatalogosService(this.HttpClientFactory, this.Configuration, LocalStorageService, "api/TipoSexo");

        public CatalogosService Marcas => new CatalogosService(this.HttpClientFactory, this.Configuration, LocalStorageService, "api/Marcas");

        public CatalogosService TiposTelefonoService => new CatalogosService(this.HttpClientFactory, this.Configuration, LocalStorageService, "api/TiposTelefonos");

    }
}
