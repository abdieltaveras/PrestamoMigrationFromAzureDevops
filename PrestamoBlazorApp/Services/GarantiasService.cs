
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;

namespace PrestamoBlazorApp.Services
{
    public class GarantiasService : ServiceBase
    {
        string apiUrl = "api/garantias";
        public async Task<IEnumerable<Garantia>> GetWithPrestamo(BuscarGarantiaParams buscarGarantiaParams)
        {
            buscarGarantiaParams.JsonGet = JsonConvert.SerializeObject(buscarGarantiaParams);
            //string searchToText = search.Search;
            var result = await GetAsync<Garantia>(apiUrl + "/GetWithPrestamo", buscarGarantiaParams);
            return result;
        }

        public async Task<IEnumerable<Garantia>> Get(GarantiaGetParams getParams)
        {
            getParams.JsonGet = JsonConvert.SerializeObject(getParams);
            var result =  await GetAsync<Garantia>(apiUrl+"/get", getParams);
            return result;
        }
        public async Task<IEnumerable<TipoGarantia>> GetTipoGarantia(TipoGetParams tipoGetParams)
        {
            tipoGetParams.JsonGet = JsonConvert.SerializeObject(tipoGetParams);
            var result = await GetAsync<TipoGarantia>("api/tipogarantia", tipoGetParams);
            return result;
        }
        public async Task<IEnumerable<Marca>> GetMarcasForGarantia(MarcaGetParams marcaGetParams)
        {
            marcaGetParams.JsonGet = JsonConvert.SerializeObject(marcaGetParams);
            var result = await GetAsync<Marca>("api/marcas", marcaGetParams);
            return result;
        }
        public async Task<IEnumerable<Modelo>> GetModelosForGarantias(ModeloGetParams modeloGetParams)
        {
            modeloGetParams.JsonGet = JsonConvert.SerializeObject(modeloGetParams);
            var result = await GetAsync<Modelo>("api/modelos", modeloGetParams);
            return result;
        }
        public async Task<IEnumerable<Color>> GetColoresForGarantia(ColorGetParams colorGetParams)
        {
            colorGetParams.JsonGet = JsonConvert.SerializeObject(colorGetParams);
            return await GetAsync<Color>("api/color", colorGetParams);
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
