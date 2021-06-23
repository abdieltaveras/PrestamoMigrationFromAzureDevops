
using Microsoft.Extensions.Configuration;
using PcpUtilidades;
using PrestamoBlazorApp.Models;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class PrestamosService : ServiceBase
    {
        readonly string apiUrl = "api/prestamos";

        public async Task<IEnumerable<Prestamo>> GetAsync(PrestamosGetParams search)
        {
            var result = await GetAsync<Prestamo>(apiUrl, search);
            return result;
        }

        public async Task<Prestamo> GetByIdAsync(int idPrestamo)
        {
            var result = await GetAsync<Prestamo>(apiUrl+"/GetById", new{ idPrestamo = idPrestamo });
            return result.FirstOrDefault();
        }

        public async Task<PrestamoConDetallesParaUIPrestamo> GetConDetallesForUiAsync(int idPrestamo)
        {
            var result = await GetAsync<PrestamoConDetallesParaUIPrestamo>(apiUrl+ "/GetConDetallesForUi", new { idPrestamo = idPrestamo});
            return result.FirstOrDefault();
        }

        public PrestamosService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {

        }

        public async Task SavePrestamo(Prestamo Prestamo)
        {
            try
            {
                await PostAsync<Prestamo>(apiUrl, Prestamo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar", ex);
            }

        }

        public async Task<IEnumerable<Cuota>>  GenerarCuotas(Prestamo prestamo)
        {
            var infoForGeneradorDeCuotas = new InfoGeneradorDeCuotas(prestamo);
            
            var result = await GetAsync<Cuota>(apiUrl+"/GenerarCuotas", new { jsonInfoGenCuotas = infoForGeneradorDeCuotas.ToJson() ,idPeriodo= prestamo.IdPeriodo, idTipoAmortizacion = prestamo.IdTipoAmortizacion});
            return result;
        }


        public async Task<IEnumerable<Cuota>> GenerarCuotas2(Prestamo prestamo)
        {
            var result = await GetAsync<Cuota>(apiUrl + "/GenerarCuotas2", new { Prestamo = prestamo.ToJson()});
            return result;
        }

        //public async Task<IEnumerable<Cuota>>  Calcular(Prestamo prestamo)
        //{
        //    var result = await GetSingleAsync<Pres IEnumerable<Cuota>>(apiUrl + "/Calcular", prestamo);
        //    return result;

        //}
    }
}
