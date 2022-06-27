
using Microsoft.Extensions.Configuration;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Services
{
    public class TerritoriosService : ServiceBase
    {
        string apiUrl = "api/territorio";
        //public async Task<IEnumerable<Color>> GetColoresAsync(ColorGetParams search)
        //{
        //    var result = await GetAsync<Color>(apiUrl, search);
        //    return result;
        //}

        public async Task<IEnumerable<DivisionTerritorial>> GetDivisionesTerritoriales()
        {
            return await GetAsync<DivisionTerritorial>(apiUrl+ "/GetDivisionesTerritoriales", null);
        }
        public async Task<IEnumerable<DivisionTerritorial>> GetComponenteDeDivision()
        {
            return await GetAsync<DivisionTerritorial>(apiUrl + "/BuscarComponenteDeDivision", null);
        }
        public TerritoriosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveTerritorio(DivisionTerritorial territorio)
        {
            try
            {
                await PostAsync<DivisionTerritorial>(apiUrl+"/Post", territorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
        public async Task SaveDivisionTerritorial(DivisionTerritorial territorio)
        {
            try
            {
                await PostAsync<DivisionTerritorial>(apiUrl+ "/SaveDivisionTerritorial", territorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
