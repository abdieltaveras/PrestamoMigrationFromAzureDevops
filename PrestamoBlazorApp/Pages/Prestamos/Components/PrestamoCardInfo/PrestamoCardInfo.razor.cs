using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoCardInfo
{
    public partial class PrestamoCardInfo
    {
        decimal MontoPagar { get; set; } = 0;
        public PrestamoConDetallesParaUIPrestamo Prestamo { get; set; } = new PrestamoConDetallesParaUIPrestamo();
        [Inject]
        PrestamosService _PrestamosService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetPrestamo();
            //await base.OnInitializedAsync();
        }

        private async Task GetPrestamo()
        {
            var garantias = new List<InfoGarantiaDrCr>();
            garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Toyota", NombreModelo = "Corolla" });
            garantias.Add(new InfoGarantiaDrCr { NombreMarca = "Honda", NombreModelo = "Civic" });

            Prestamo = await _PrestamosService.GetConDetallesForUiAsync(1);
            Prestamo.Prestamo.LlevaGarantia = true;
            Prestamo.infoGarantias = garantias;
            StateHasChanged();
        }
    }
}
