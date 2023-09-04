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

namespace PrestamoBlazorApp.Shared
{
    public partial class ProyeccionCuotasValoresInicialesV2 : BaseForList
    {
        [Parameter] public IEnumerable<CxCCuota> Cuotas { get; set; } = new List<CxCCuota>();


    }
}
