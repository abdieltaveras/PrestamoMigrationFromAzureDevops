using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components
{
    public partial class ClientePrestamosList
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        IEnumerable<PrestamoClienteUI> prestamos = new List<PrestamoClienteUI>();
        [Inject] PrestamosService PrestamosService { get; set; }
        [Parameter]
        public int IdCliente { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //await Handle_GetDataForList(GetPrestamos);
            //await base.OnInitializedAsync();
            await GetPrestamos();
        }

        private async Task GetPrestamos()
        {
            //prestamos = await PrestamosService.GetAsync(new PrestamosGetParams { idCliente = IdCliente});
            prestamos = await PrestamosService.GetPrestamoClienteUI(new PrestamoClienteUIGetParam { IdCliente = IdCliente});
            //totalPrestamos = prestamos.Count();
        }
        private async Task SelectPrestamo(PrestamoClienteUI prestamo)
        {
          
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(prestamo));
            }
        }
    }
}
