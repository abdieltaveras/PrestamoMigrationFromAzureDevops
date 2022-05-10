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

        public CatalogosServicesFactoryManager(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.HttpClientFactory = clientFactory;
            this.Configuration = configuration;
        }
        public CatalogosService OcupacionesService => new CatalogosService(this.HttpClientFactory, this.Configuration, "api/Ocupacion");

        public CatalogosService ColoresService => new CatalogosService(this.HttpClientFactory, this.Configuration, "api/Color");

        public CatalogosService TiposSexoService => new CatalogosService(this.HttpClientFactory, this.Configuration, "api/TipoSexo");

        public CatalogosService Marcas => new CatalogosService(this.HttpClientFactory, this.Configuration, "api/Marcas");

        public CatalogosService TiposTelefonoService => new CatalogosService(this.HttpClientFactory, this.Configuration, "api/TiposTelefonos");

    }
}
