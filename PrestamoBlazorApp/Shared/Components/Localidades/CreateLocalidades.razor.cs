using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using MudBlazor;

namespace PrestamoBlazorApp.Shared.Components.Localidades
{
    public partial class CreateLocalidades : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        BuscarLocalidadParams buscarLocalidadParams { get; set; } = new BuscarLocalidadParams();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();
        IEnumerable<Territorio> territorios { get; set; } = new List<Territorio>();
        private int _SelectedLocalidad = -1;
        public int SelectedLocalidad { get { return _SelectedLocalidad; } set { _SelectedLocalidad = value; } }
        [Parameter]
        public int IdLocalidad { get; set; } = -1;
        
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        IEnumerable<Territorio> Territorios { get; set; } = new List<Territorio>();
        private int _SelectedTipoLocalidad = -1;
        public int SelectedTipoLocalidad { get { return _SelectedTipoLocalidad; } set { _SelectedTipoLocalidad = value; } }

        protected override async Task OnInitializedAsync()
        {
            await CreateOrEdit();
            this.localidades = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.territorios = await localidadesService.GetComponentesTerritorio();
        }
        public async Task HandleLocalidadSelected(BuscarLocalidad buscarLocalidad)
        {
            var lst = await localidadesService.GetComponentesTerritorio();
            territorios = lst.ToList();
            var result = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = buscarLocalidad.IdLocalidad });
            var locate = result.Where(m => m.IdLocalidad == buscarLocalidad.IdLocalidad).FirstOrDefault();
            this.Localidad = locate;

        }
        async Task SaveLocalidad()
        {
            //await BlockPage();
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null, false, "/localidades/listado");
            await CloseModal("1");
            //await Edit(this.Localidad.IdLocalidad);
            //await UnBlockPage();

        }
        async Task CreateOrEdit()
        {
            if (IdLocalidad > 0)
            {
                await BlockPage();
                var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = IdLocalidad });
                Localidad = localidad.FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                this.Localidad = new Localidad();
            }
        }
        private async Task CloseModal(string result = "")
        {
            MudDialog.Close(DialogResult.Ok(result));
        }

        void RaiseInvalidSubmit()
        {

        }
    }
}
