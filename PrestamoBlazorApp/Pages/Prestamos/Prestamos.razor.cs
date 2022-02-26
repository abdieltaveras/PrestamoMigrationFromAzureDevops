using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
namespace PrestamoBlazorApp.Pages.Prestamos
{
    public partial class Prestamos : BaseForList
    {
        [Inject]
        PrestamosService prestamoService { get; set; }
        PrestamosGetParams searchPrestamos { get; set; } = new PrestamosGetParams();
        int totalPrestamos { get; set; }
        IEnumerable<Prestamo> prestamos;
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetPrestamos);
            await base.OnInitializedAsync();
        }
        private async Task GetPrestamos()
        {
            prestamos = new List<Prestamo>();
            prestamos = await prestamoService.GetAsync(this.searchPrestamos);
            totalPrestamos = prestamos.Count();
        }
        private async Task DatosFicha()
        {
           var datos = JsonConvert.SerializeObject(DocumentosIngresoData());
           var a = await FichaDetalleDrCr(datos);
        }
        public IEnumerable<DetalleDrCrImpresionDocumento> DocumentosIngresoData()
        {
            var documentosIngreso = new List<DetalleDrCrImpresionDocumento>();

            var doc1 = new DetalleDrCrImpresionDocumento
            {
                BalanceDocumentoCXC = 100000,
                MontoPagado = 10000,
                Capital = 5000,
                Interes = 2000,
                Moras = 1500,
                OtrosDebitos = 1500,
                CodigoCliente = "00000001",
                CuotasAtrasadas = 6,
                MontoAtrasado = 26000,
                Fecha = DateTime.Now,
                NombreDocumento = "Recibo Ingreso",
                NumeracionDocumento = "00007548",
                NumeracionGarantia = "lc6paga1526edfrt",
                TotalCuotasPorPagar = 15,
                NombreCompletoCliente = "Pedro Rodriguez Gutierrez Probando",
                NombreDocumentoCxC = "Prestamo",
                InteresDespuesDeVencido = 0,
                NombreUsuario = "Ramon Rutin",
                LoginName = "RamonR",
                NombreNegocio = "Prestamos Rapido",
                ClienteTaxId = "112108236"
            };

            documentosIngreso.Add(doc1);
            return documentosIngreso;
        }
    }
}
