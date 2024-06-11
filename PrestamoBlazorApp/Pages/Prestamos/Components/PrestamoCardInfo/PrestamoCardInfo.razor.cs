using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Prestamos;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoCardInfo
{
    public partial class PrestamoCardInfo
    {
        [Inject]
        IDialogService DialogService { get; set; }
        string SearchText { get; set; }
        decimal MontoPagar { get; set; } 
        List<CuotaModel> Cuotas { get; set; } = new List<CuotaModel>();
        public PrestamoConDetallesParaUIPrestamo Prestamo { get; set; } = new PrestamoConDetallesParaUIPrestamo();
        [Inject]
        PrestamosService _PrestamosService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //await GetPrestamo();
            //await base.OnInitializedAsync();
        }
        private async Task OnEnter(KeyboardEventArgs args)
        {
            if(args.Key.ToLower() == "enter")
            {
                await GetPrestamo(Convert.ToInt32(SearchText));
            }
        }
        private async Task GetPrestamo(int id)
        {
            Prestamo = new PrestamoConDetallesParaUIPrestamo();
            Prestamo = await _PrestamosService.GetConDetallesForUiAsync(id);
            //var garantias = new List<InfoGarantiaDrCr>();
            //garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Toyota", NombreModelo = "Corolla" });
            //garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Honda", NombreModelo = "Civic" });

            //Prestamo.Prestamo.LlevaGarantia = true;
            //Prestamo.infoGarantias = garantias;
            //Prestamo.Cuotas = new List<CuotaModel>() {
            //    new CuotaModel { Balance=10, Monto=11, NumeroCuota=1 },
            //    new CuotaModel { Balance=20, Monto=21, NumeroCuota=2 },
            //    new CuotaModel { Balance=30, Monto=31, NumeroCuota=3 },
            //    new CuotaModel { Balance=40, Monto=41, NumeroCuota=4 },
            //};
            StateHasChanged();
        }
        
        async Task ShowCuotasInfo()
        {
            DialogService.Show<CuotaCardInfo.CuotaCardInfo>("Informacion De Cuotas");
        }
        private async Task ShowSearchPrestamo()
        {
            var parameters = new DialogParameters {  };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchPrestamoByProperty>("Seleccionar Prestamo", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var pres = (PrestamoClienteUI)result.Data;
                await GetPrestamo(pres.IdPrestamo);
            }
        }
    }
}
