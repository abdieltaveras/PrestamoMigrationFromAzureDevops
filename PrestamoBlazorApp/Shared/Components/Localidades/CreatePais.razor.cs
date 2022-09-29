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
    public partial class CreatePais : BaseForCreateOrEdit
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
        [Parameter]
        public int IdLocalidad { get; set; } = -1;

        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        IEnumerable<Localidad> localidadesByTipo { get; set; } = new List<Localidad>();
        public int IdLocalidadByTipoSelected { get; set; }

        private PrestamoEntidades.DivisionTerritorial _SelectedTipoLocalidad { get; set; }
        public PrestamoEntidades.DivisionTerritorial SelectedTipoLocalidad { get { return _SelectedTipoLocalidad; } set { _SelectedTipoLocalidad = value; GetLocalidadesByTipo().GetAwaiter(); } }
        protected override async Task OnInitializedAsync()
        {
            await CreateOrEdit();
        }

        private async Task Save()
        {
            //this.Localidad.IdTipoDivisionTerritorial = SelectedTipoLocalidad.IdDivisionTerritorial;
            this.Localidad.Activo = true;
            this.Localidad.Codigo = "-----";
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null, false, mudDialogInstance:MudDialog);

        }
        async Task CreateOrEdit()
        {
            await BlockPage();
            territorios = await localidadesService.GetComponentesTerritorio();
            if (IdLocalidad > 0)
            {
               
                var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = IdLocalidad });
                Localidad = localidad.FirstOrDefault();
                //territorios = await localidadesService.GetComponentesTerritorio();
                SelectedTipoLocalidad = territorios.Where(m => m.IdDivisionTerritorial == Localidad.IdTipoDivisionTerritorial).FirstOrDefault();
         
            }
            else
            {
                if (territorios.Count() == 1)
                {
                    SelectedTipoLocalidad = territorios.FirstOrDefault();
                }
                this.Localidad = new Localidad();
            }
            await UnBlockPage();
            StateHasChanged();
        }
        private async Task GetLocalidadesByTipo()
        {
            //localidadesByTipo = new List<Localidad>();
            //var loc = await localidadesService.Get(new LocalidadGetParams { IdTipoLocalidad = 2 });
            //localidadesByTipo = loc;
            //if (loc.Count() > 0 && IdLocalidad <= 0)
            //{
            //    IdLocalidadByTipoSelected = localidadesByTipo.FirstOrDefault().IdLocalidad;
            //}

            //StateHasChanged();
        }
    }
}
