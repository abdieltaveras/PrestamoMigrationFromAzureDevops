using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
using PrestamoBlazorApp.Domain;
using PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoByColumnSelected;
using MudBlazor;
using PrestamoBlazorApp.Pages.Prestamos;

namespace PrestamoBlazorApp.Shared
{
    public partial class ProyeccionCuotasValoresInicialesV2 : BaseForList
    {

        [Inject]
        IDialogService DialogService { get; set; }
        [Parameter] public IEnumerable<DebitoPrestamoConDetallesViewModel> Cuotas { get; set; } = new List<DebitoPrestamoConDetallesViewModel>();
        //private List<CxCCuota> Cuotas { get; set; } = new List<CxCCuota>();
        
        
    }
}
