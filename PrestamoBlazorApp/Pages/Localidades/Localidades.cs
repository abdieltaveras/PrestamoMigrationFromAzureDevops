﻿using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class Localidades : BaseForCreateOrEdit
    {
        LocalidadGetParams localidadGetParams { get; set; } = new LocalidadGetParams();
        [Inject]
        LocalidadesService localidadesService { get; set; }

        IEnumerable<LocalidadesHijas> LocalidadesHijas { get; set; } = new List<LocalidadesHijas>();
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        //void Clear() => localidades = null;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await VerLocalidades();
                await BlockPage();
                //localidades = await localidadesService.GetLocalidadesAsync(new LocalidadGetParams());
                await UnBlockPage();
                StateHasChanged();
            }

        }
        async Task SaveLocalidad()
        {
            await BlockPage();
            StateHasChanged();
            await localidadesService.SaveLocalidad(this.Localidad);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
        }
        public async Task VerLocalidades()
        {
            await JsInteropUtils.SearchLocalidad(jsRuntime);
        }
        public async Task HandleLocalidadSelected(BuscarLocalidad buscarLocalidad)
        {
            this.LocalidadesHijas = new List<LocalidadesHijas>();
            this.Localidad.IdLocalidadPadre = buscarLocalidad.IdLocalidad;
            this.LocalidadesHijas = await localidadesService.GetHijasLocalidades(buscarLocalidad.IdLocalidad);
        }
        //public async Task GetLocalidadesHijas(int selected)
        //{
        //   //var result = await localidadesService.GetHijasLocalidades(selected);
        //   //this.LocalidadesHijas = result;
        //}
        void RaiseInvalidSubmit()
        {

        }
    }
}
