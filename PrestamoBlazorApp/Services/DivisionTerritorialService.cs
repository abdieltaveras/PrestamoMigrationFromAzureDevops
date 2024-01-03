
using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PcpSoft.System;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Services
{
    public class DivisionTerritorialService : ServiceBase
    {
        string apiUrl = "api/DivisionTerritorial";
        //public async Task<IEnumerable<Color>> GetColoresAsync(ColorGetParams search)
        //{
        //    var result = await GetAsync<Color>(apiUrl, search);
        //    return result;
        //}
        public DivisionTerritorialService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration,localStorageService)
        {

        }
        public async Task<IEnumerable<DivisionTerritorial>> GetDivisionesTerritoriales(DivisionTerritorialGetParams search)
        {
            return await GetAsync<DivisionTerritorial>(apiUrl+ "/Get", search);
        }
        public async Task<IEnumerable<DivisionTerritorial>> GetDivisionTerritorialComponents(int idDivisionTerritorial)
        {
            return await GetAsync<DivisionTerritorial>(apiUrl + "/GetDivisionTerritorialComponents", new { IdDivisionTerritorial = idDivisionTerritorial });
        }

        public async Task<IEnumerable<DivisionTerritorial>> GetTiposDivisionTerritorial()
        {
            return await GetAsync<DivisionTerritorial>(apiUrl + "/GetTiposDivisionTerritorial", null);
        }

        public async Task<bool> SaveDivisionTerritorial(DivisionTerritorial territorio)
        {
            var response = true;
            try
            {
                await PostAsync<DivisionTerritorial>(apiUrl + "/Post", territorio);
            }
            catch (Exception e)
            {
                response = false;
                    //JsonConvert.DeserializeObject<ResponseResult<int>>(e.Message);
            }
            return response;
        }
    }
}
