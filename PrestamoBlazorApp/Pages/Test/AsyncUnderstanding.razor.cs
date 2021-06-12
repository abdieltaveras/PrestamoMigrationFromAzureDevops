using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Test
{
    public partial class AsyncUnderstanding : BaseForCreateOrEdit
    {
        [Inject]
        ClasificacionesService clasificacionesService { get; set; }

        [Inject]
        TiposMoraService tiposMorasService { get; set; }

        [Inject]
        TasasInteresService tasasInteresService { get; set; }
        [Inject]
        PeriodosService periodosService { get; set; }


        [Inject]
        GarantiasService GarantiasService { get; set; }

        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }



        private async Task TestAsync()
        {
            var elapseTime = new Stopwatch();
            elapseTime.Start();
            Clasificaciones = await clasificacionesService.Get(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres = TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            Periodos = await periodosService.Get(new PeriodoGetParams());
            elapseTime.Stop();
            
            await SweetMessageBox($"async se tardo {elapseTime.ElapsedMilliseconds / 1000}");

            elapseTime.Start();
            clasificacionesService.Get(new ClasificacionesGetParams());
            tiposMorasService.Get(new TipoMoraGetParams());
            tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            periodosService.Get(new PeriodoGetParams());
            elapseTime.Stop();
            await SweetMessageBox($"sync se tardo {elapseTime.ElapsedMilliseconds / 1000}");
        }

    }
}
