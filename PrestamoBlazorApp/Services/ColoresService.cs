
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrestamoBLL;

namespace PrestamoBlazorApp.Services
{
    
    public class ColoresService : ServiceBase
    {
        string apiUrl = "api/color";
        //JsonGlobalParam JsonGlobalParam = new JsonGlobalParam();
        //public async Task<IEnumerable<Color>> GetColoresAsync(ColorGetParams search)
        //{
        //    var result = await GetAsync<Color>(apiUrl, search);
        //    return result;
        //}

        public async Task<IEnumerable<Color>> Get(ColorGetParams colorGetParams)
        {
            return await GetAsync<Color>(apiUrl, new { JsonGet =  colorGetParams.ToJson()});
        }
        public ColoresService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveColor(Color color)
        {
            try
            {
                await PostAsync<Color>(apiUrl, color);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
