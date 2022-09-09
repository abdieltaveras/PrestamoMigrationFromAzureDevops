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
        [Inject]
        DivisionTerritorialService territoriosService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        BuscarLocalidadParams buscarLocalidadParams { get; set; } = new BuscarLocalidadParams();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();
        IEnumerable<PrestamoEntidades.DivisionTerritorial> territorios { get; set; } = new List<PrestamoEntidades.DivisionTerritorial>();
        private int _SelectedLocalidad = -1;
        public int SelectedLocalidad { get { return _SelectedLocalidad; } set { _SelectedLocalidad = value; } }
        [Parameter]
        public int IdLocalidad { get; set; } = -1;
        
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        IEnumerable<Localidad> localidadesByTipo { get; set; } = new List<Localidad>();
        private PrestamoEntidades.DivisionTerritorial _SelectedTipoLocalidad { get; set; }
        public PrestamoEntidades.DivisionTerritorial SelectedTipoLocalidad { get { return _SelectedTipoLocalidad; } set { _SelectedTipoLocalidad = value; GetLocalidadesByTipo().GetAwaiter(); } }
        public int IdLocalidadByTipoSelected { get; set; }
        protected override async Task OnInitializedAsync()
        {
            this.localidades = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.territorios = await localidadesService.GetComponentesTerritorio(); 
            await CreateOrEdit();
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
            this.Localidad.IdDivisionTerritorial = SelectedTipoLocalidad.IdDivisionTerritorial;
            this.Localidad.IdLocalidadPadre = IdLocalidadByTipoSelected;
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
                territorios = await localidadesService.GetComponentesTerritorio();
                var ter = territorios.Where(m => m.IdDivisionTerritorial == Localidad.IdDivisionTerritorial);
                SelectedTipoLocalidad = ter.FirstOrDefault();
                IdLocalidadByTipoSelected = Localidad.IdLocalidadPadre;
                await UnBlockPage();
            }
            else
            {
                this.Localidad = new Localidad();
            }
        }
        private async Task GetLocalidadesByTipo()
        {
            localidadesByTipo = new List<Localidad>();
            var loc  = await localidadesService.Get(new LocalidadGetParams { IdTipoLocalidad = (int)SelectedTipoLocalidad.IdDivisionTerritorialPadre });
            localidadesByTipo = loc;
            if (loc.Count()>0 && IdLocalidad<=0)
            {
                IdLocalidadByTipoSelected = localidadesByTipo.FirstOrDefault().IdLocalidad;
            }
            
            StateHasChanged();
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
