
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;

using PcpUtilidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Services
{
    public class GarantiasService : ServiceBase
    {
        string apiUrl = "api/garantias";
        public async Task<IEnumerable<GarantiaConMarcaYModeloYPrestamos>> GetWithPrestamo(BuscarGarantiaParams buscarGarantiaParams)
        {
            var result = await GetAsync<GarantiaConMarcaYModeloYPrestamos>(apiUrl + "/GetWithPrestamo", new { JsonGet = buscarGarantiaParams.ToJson() });
            return result;
        }

        public async Task<IEnumerable<Garantia>> Get(GarantiaGetParams getParams)
        {
            var result =  await GetAsync<Garantia>(apiUrl+"/get", getParams);
            return result;
        }

        public async Task<IEnumerable<GarantiaConMarcaYModelo>> SearchGarantias(string search)
        {
            var result = await GetAsync<GarantiaConMarcaYModelo>(apiUrl + "/searchGarantias", new { searchText = search });
            return result;
        }

        public async Task<IEnumerable<GarantiaConMarcaYModelo>> GetGarantias(GarantiaGetParams getParam)
        {
            var result = await GetAsync<GarantiaConMarcaYModelo>(apiUrl + "/getGarantias", new { searchObject = getParam.ToJson() });
            return result;
        }
        public async Task<IEnumerable<TipoGarantia>> GetTipoGarantia(TipoGarantiaGetParams tipoGetParams)
        {
            
            var result = await GetAsync<TipoGarantia>("api/tipogarantia/get", new { JsonGet = tipoGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Marca>> GetMarcasForGarantia(MarcaGetParams marcaGetParams)
        {
            var result = await GetAsync<Marca>("api/marcas/get", new { JsonGet = marcaGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Modelo>> GetModelosForGarantias(ModeloGetParams modeloGetParams)
        {
            var result = await GetAsync<Modelo>("api/modelos/get", new { JsonGet = modeloGetParams.ToJson() });
            return result;
        }
        public async Task<IEnumerable<Color>> GetColoresForGarantia(ColorGetParams colorGetParams)
        {
            return await GetAsync<Color>("api/color/get", new { JsonGet = colorGetParams.ToJson() });
        }
        public async Task<IEnumerable<GarantiaConMarcaYModelo>> GetGarantiasByPrestamo(int IdPrestamo)
        {
            return await GetAsync<GarantiaConMarcaYModelo>(apiUrl+ "/GetGarantiasByPrestamo", new { IdPrestamo = IdPrestamo });
        }
        //GetGarantiasByPrestamo
        public async Task<bool> TienePrestamoVigente(int idGarantia)
        {
            var result = await GetAsync<bool>("api/Garantias/TienePrestamoVigentes", new { idGarantia = idGarantia});
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<GarantiasConPrestamo>> GetPrestamosVigentesForGarantia(int idGarantia)
        {
            return await GetAsync<GarantiasConPrestamo>("api/Garantias/GetPrestamosVigentes", new { idGarantia = idGarantia });
        }

        public GarantiasService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {
            
        }

        public async Task SaveGarantia(Garantia Garantia)
        {
            try
            {
                await PostAsync<Garantia>(apiUrl+"/post", Garantia);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }

        public async Task<string> ReportFicha(IJSRuntime jSRuntime, int idgarantia, int reportType)
        {
            var d = await ReportGenerate(jSRuntime, apiUrl + "/GarantiaFichaReport", new { idgarantia = idgarantia, reportType = reportType });
            return d.StatusCode.ToString();
        }
    }
}
