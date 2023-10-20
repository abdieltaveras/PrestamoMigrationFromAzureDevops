using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoCardInfo
{
    public partial class PrestamoCardInfo
    {
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
            Prestamo = await _PrestamosService.GetConDetallesForUiAsync(1);
            StateHasChanged();
        }
    }
}
