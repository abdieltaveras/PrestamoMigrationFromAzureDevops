
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using PrestamoBLL;
using PcpUtilidades;

namespace PrestamoBlazorApp.Services
{
    public class ModelosService : ServiceBase
    {
        string apiUrl = "api/Modelos";
        public async Task<IEnumerable<Marca>> GetMarcasForModelo()
        {
           var marcasParams = new MarcaGetParams();
            var result = await GetAsync<Marca>("api/marcas" , new { JsonGet = marcasParams.ToJson() });
            return result;
        }
        //public async Task<IEnumerable<Modelo>> GetModelosAsync(ModeloGetParams search)
        //{
        //    var result = await GetAsync<Modelo>(apiUrl, search);
        //    return result;
        //}

        public async Task<IEnumerable<Modelo>> Get(ModeloGetParams modeloGetParams)
        {
            
            var result =  await GetAsync<Modelo>(apiUrl, new { JsonGet = modeloGetParams.ToJson() });
            return result;
        }
        public ModelosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveModelo(Modelo Modelo)
        {
            try
            {
                await PostAsync<Modelo>(apiUrl, Modelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
