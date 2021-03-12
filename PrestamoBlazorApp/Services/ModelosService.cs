
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Web;

namespace PrestamoBlazorApp.Services
{
    public class ModelosService : ServiceBase
    {
        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
        string apiUrl = "api/Modelos";
        public async Task<IEnumerable<Modelo>> GetModelosAsync(ModeloGetParams search)
        {
            var result = await GetAsync<Modelo>(apiUrl, search);
            return result;
        }

        public async Task<IEnumerable<Modelo>> GetAll(ModeloGetParams modeloGetParams)
        {
            var result =  await GetAsync<Modelo>(apiUrl+"/getall", modeloGetParams );
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
