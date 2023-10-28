﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoCardInfo
{
    public partial class PrestamoCardInfo
    {
        [Inject]
        IDialogService DialogService { get; set; }
        string SearchText { get; set; }
        decimal MontoPagar { get; set; } = 0;
        public PrestamoConDetallesParaUIPrestamo Prestamo { get; set; } = new PrestamoConDetallesParaUIPrestamo();
        [Inject]
        PrestamosService _PrestamosService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //await GetPrestamo();
            //await base.OnInitializedAsync();
        }

        private async Task GetPrestamo()
        {
            Prestamo = await _PrestamosService.GetConDetallesForUiAsync(1);
            var garantias = new List<InfoGarantiaDrCr>();
            garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Toyota", NombreModelo = "Corolla" });
            garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Honda", NombreModelo = "Civic" });
 
            Prestamo.Prestamo.LlevaGarantia = true;
            Prestamo.infoGarantias = garantias;
            Prestamo.Cuotas = new List<CuotaModel>() {
                new CuotaModel { Balance=10, Monto=11, NumeroCuota=1 },
                new CuotaModel { Balance=20, Monto=21, NumeroCuota=2 },
                new CuotaModel { Balance=30, Monto=31, NumeroCuota=3 },
                new CuotaModel { Balance=40, Monto=41, NumeroCuota=4 },
            };
            StateHasChanged();
        }
        async Task ShowCuotasInfo()
        {
            DialogService.Show<CuotaCardInfo.CuotaCardInfo>("Informacion De Cuotas");
        }
    }
}
