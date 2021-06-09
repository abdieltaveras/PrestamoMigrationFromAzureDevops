using PrestamoBLL.Entidades;
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
        private int? _SelectedTipoLocalidad = null;

        public int? SelectedTipoLocalidad { get { return _SelectedTipoLocalidad; } set { _SelectedTipoLocalidad = value; } }
        IEnumerable<LocalidadesHijas> LocalidadesHijas { get; set; } = new List<LocalidadesHijas>();
        IEnumerable<Territorio> Territorios { get; set; } = new List<Territorio>();
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
            //if (this.Localidad.)
            //{

            //}
            this.Localidad.IdTipoLocalidad = Convert.ToInt32( SelectedTipoLocalidad);
            if (this.Localidad.IdLocalidadPadre <= 0)
            {
                await SweetMessageBox("Debe seleccionar un una localidad", "error", "/localidades");
            }
            else
            {
                await localidadesService.SaveLocalidad(this.Localidad);
                await UnBlockPage();
                await SweetMessageBox("Guardado Correctamente", "success", "/localidades");
            }
        
        }
        public async Task VerLocalidades()
        {
            await JsInteropUtils.SearchLocalidad(jsRuntime);
        }
        public async Task HandleLocalidadSelected(BuscarLocalidad buscarLocalidad)
        {
            
            this.LocalidadesHijas = new List<LocalidadesHijas>();
            this.Territorios = new List<Territorio>();
            this.Localidad.IdLocalidadPadre = buscarLocalidad.IdLocalidad;
            this.LocalidadesHijas = await localidadesService.GetHijasLocalidades(buscarLocalidad.IdLocalidad);
            var ter = await localidadesService.GetComponentesTerritorio();
            this.Territorios = ter.Where(m => m.IdLocalidadPadre == buscarLocalidad.IdTipoLocalidad);
            if (this.Territorios.Count() == 1)
            {
                SelectedTipoLocalidad = this.Territorios.FirstOrDefault().IdDivisionTerritorial;
            }
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
