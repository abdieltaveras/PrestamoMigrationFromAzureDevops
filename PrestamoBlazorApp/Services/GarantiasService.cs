
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
using PrestamoBLL;

namespace PrestamoBlazorApp.Services
{
    public class GarantiasService : ServiceBase
    {
        string apiUrl = "api/garantias";
        public async Task<IEnumerable<Garantia>> GetWithPrestamo(BuscarGarantiaParams buscarGarantiaParams)
        {
            var result = await GetAsync<Garantia>(apiUrl + "/GetWithPrestamo", new { JsonGet = buscarGarantiaParams.ToJson() });
            return result;
        }

        public async Task<IEnumerable<Garantia>> Get(GarantiaGetParams getParams)
        {
            
            var result =  await GetAsync<Garantia>(apiUrl+"/get", new { JsonGet = getParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<TipoGarantia>> GetTipoGarantia(TipoGetParams tipoGetParams)
        {
            
            var result = await GetAsync<TipoGarantia>("api/tipogarantia", new { JsonGet = tipoGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Marca>> GetMarcasForGarantia(MarcaGetParams marcaGetParams)
        {
            var result = await GetAsync<Marca>("api/marcas", new { JsonGet = marcaGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Modelo>> GetModelosForGarantias(ModeloGetParams modeloGetParams)
        {
            var result = await GetAsync<Modelo>("api/modelos", new { JsonGet = modeloGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Color>> GetColoresForGarantia(ColorGetParams colorGetParams)
        {
            return await GetAsync<Color>("api/color", new { JsonGet = colorGetParams.ToJson() });
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
