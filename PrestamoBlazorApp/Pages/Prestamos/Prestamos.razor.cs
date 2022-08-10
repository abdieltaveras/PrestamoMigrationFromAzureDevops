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
        private Prestamo SelectedItem1 = null;

        [Inject] PrestamosService prestamoService { get; set; }
        [Inject] ClientesService ClientesService { get; set; }
        PrestamosGetParams searchPrestamos { get; set; } = new PrestamosGetParams();
        int totalPrestamos { get; set; }
        IEnumerable<Prestamo> prestamos = new List<Prestamo>();
        private bool FilterFunc1(Prestamo element) => FilterFunc(element, SearchStringTable);

        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetPrestamos);
            //await base.OnInitializedAsync();
        }
        private async Task GetPrestamos()
        {
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
        private bool FilterFunc(Prestamo element, string searchString)
        {
            //if (string.IsNullOrWhiteSpace(searchString))
            //    return true;
            //if (element.infoCliente.NombreCompleto.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            //    return true;
            //if (element.PrestamoNumero.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            //    return true;
            //if (element.FechaInsertado != null)
            //{
            //    if (element.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            //        return true;
            //}
            return false;
        }
        private async Task searchTableDataBase(string search)
        {
            LoadingTable = true;
            //if (search.Length > 2)
            //{
            //    prestamos = await prestamoService.GetAsync(this.searchPrestamos);
            //    //clientes = new List<Cliente>();
            //    //clientes = await clientesService.SearchClientes(search, false);
            //    //totalClientes = clientes.Count();
            //}
            //else
            //{
            //    //this.searchClientes.CantidadRegistrosASeleccionar = 50;
            //    //clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
            //    //totalClientes = clientes.Count();
            //}
            LoadingTable = false;
        }
    }
}
