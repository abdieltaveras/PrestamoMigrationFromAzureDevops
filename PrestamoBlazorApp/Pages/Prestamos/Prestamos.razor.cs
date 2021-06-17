using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;

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

    }
}
