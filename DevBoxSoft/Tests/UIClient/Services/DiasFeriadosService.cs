using Blazored.LocalStorage;
using DevBox.Core.Classes.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIClient.Providers;

namespace UIClient.Services
{
    public class DiasFeriadosService : HttpServiceBase
    {
        public async Task<IEnumerable<DiaFeriado>> GetDiasFeriadosAsync(int ano)
        {
            try
            {
                var result = await GetAsync<DiaFeriado>("api/diasferiados", new { ano });
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar acceder al servicio de datos.", ex);
            }
        }
        public async Task SaveDiaFeriadoAsync(DateTime Dia, string Descripcion)
        {
            try
            {
                await PostAsync<object, object>("api/diasferiados", new { idDiaFeriado = -1, Dia, Descripcion });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }
        }
        public DiasFeriadosService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService) : base("DiasFeriados", clientFactory, configuration, localStorageService, notificationService)
        {

        }

        internal List<DiaFeriado> GetDefDiasFeriados(int year)
        {
            return DiaFeriado.GetDefaultDiasRD(year);
        }
    }
}
