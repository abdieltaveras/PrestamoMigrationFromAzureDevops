
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
            var result =  await GetAsync<Garantia>(apiUrl, getParams);
            return result;
        }
        public async Task<IEnumerable<Marca>> GetMarcasForGarantia()
        {
            var result = await GetAsync<Marca>("api/marcas", null);
            return result;
        }
        public async Task<IEnumerable<Modelo>> GetModelosForGarantias(ModeloGetParams modeloGetParams)
        {
            var result = await GetAsync<Modelo>("api/modelos", modeloGetParams);
            return result;
        }
        public async Task<IEnumerable<Color>> GetColoresForGarantia()
        {
            return await GetAsync<Color>("api/color", null);
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
