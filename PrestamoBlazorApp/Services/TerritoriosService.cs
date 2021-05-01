
using Microsoft.Extensions.Configuration;
using PrestamoBLL.Entidades;
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

        public async Task<IEnumerable<Territorio>> GetDivisionesTerritoriales()
        {
            return await GetAsync<Territorio>(apiUrl+ "/GetDivisionesTerritoriales", null);
        }
        public async Task<IEnumerable<Territorio>> GetComponenteDeDivision()
        {
            return await GetAsync<Territorio>(apiUrl + "/BuscarComponenteDeDivision", null);
        }
        public TerritoriosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveTerritorio(Territorio territorio)
        {
            try
            {
                await PostAsync<Territorio>(apiUrl+"/Post", territorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
        public async Task SaveDivisionTerritorial(Territorio territorio)
        {
            try
            {
                await PostAsync<Territorio>(apiUrl+ "/SaveDivisionTerritorial", territorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
