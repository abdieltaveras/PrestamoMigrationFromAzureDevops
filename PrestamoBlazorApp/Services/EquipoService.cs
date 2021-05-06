
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class EquiposService : ServiceBase
    {
        string apiUrl = "api/Equipo";
        public async Task<IEnumerable<Equipo>> Get(EquiposGetParam search)
        {
            search.JsonGet = JsonConvert.SerializeObject(search);
            var result = await GetAsync<Equipo>(apiUrl, search);
            return result;
        }

        //public async Task<IEnumerable<Equipo>> GetAll()
        //{
        //    var result =  await GetAsync<Equipo>(apiUrl+"/getall", null);
        //    return result;
        //}
        public EquiposService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SaveEquipo(Equipo Equipo)
        {
            try
            {
                await PostAsync<Equipo>(apiUrl, Equipo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }
    }
}
