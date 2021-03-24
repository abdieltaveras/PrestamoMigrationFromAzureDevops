
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class GarantiasService : ServiceBase
    {
        string apiUrl = "api/garantias";
        public async Task<IEnumerable<Garantia>> GetWithPrestamo(BuscarGarantiaParams buscarGarantiaParams)
        {
            //string searchToText = search.Search;
            var result = await GetAsync<Garantia>(apiUrl + "/GetWithPrestamo", buscarGarantiaParams);
            return result;
        }

        public async Task<IEnumerable<Garantia>> Get(GarantiaGetParams getParams)
        {
            var result =  await GetAsync<GarantiaConMarcaYModeloYPrestamos>(apiUrl, getParams);
            return result;
        }
        public GarantiasService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveGarantia(Garantia Garantia)
        {
            try
            {
                await PostAsync<Garantia>(apiUrl, Garantia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
