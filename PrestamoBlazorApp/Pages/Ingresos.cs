using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Ingresos
    {
        [Inject]
        IngresosService ingresosService { get; set; }
        IEnumerable<Ingreso> ingresos;
        bool loading = false;
        async Task GetIngresosByParam()
        {
            loading = true;
            ingresos = await ingresosService.GetIngresosAsync(new IngresoGetParams());
            loading = false;
        }

        async Task GetIngresosById()
        {
            loading = true;
            ingresos = await ingresosService.GetIngresosAsync(1);
            loading = false;
        }

        async Task SaveIngreso()
        {
            
            await ingresosService.SaveIngreso(new Ingreso());
            
        }
    }
}
