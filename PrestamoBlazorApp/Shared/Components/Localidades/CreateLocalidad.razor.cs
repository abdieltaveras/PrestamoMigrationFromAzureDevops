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
    public partial class CreateLocalidad : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        IEnumerable<PrestamoEntidades.DivisionTerritorial> territorios { get; set; } = new List<PrestamoEntidades.DivisionTerritorial>();
        [Parameter]
        public int IdLocalidad { get; set; } = -1;

        public int IdLocalidadByTipoSelected { get; set; }

        private PrestamoEntidades.DivisionTerritorial _SelectedTipoLocalidad { get; set; }
        public PrestamoEntidades.DivisionTerritorial SelectedTipoLocalidad { get { return _SelectedTipoLocalidad; } set { _SelectedTipoLocalidad = value;  } }
        protected override async Task OnInitializedAsync()
        {
            await CreateOrEdit();
        }

        private async Task Save()
        {
            //this.Localidad.IdTipoDivisionTerritorial = SelectedTipoLocalidad.IdDivisionTerritorial;
            this.Localidad.Activo = true;
            this.Localidad.Codigo = "-----";
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad),null, null, false, mudDialogInstance:MudDialog);
        }
        async Task CreateOrEdit()
        {
            territorios = await localidadesService.GetComponentesTerritorio();
            if (IdLocalidad > 0)
            {
                await BlockPage();
                var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = IdLocalidad });
                Localidad = localidad.FirstOrDefault();
                //territorios = await localidadesService.GetComponentesTerritorio();
                SelectedTipoLocalidad = territorios.Where(m => m.IdDivisionTerritorial == Localidad.IdTipoDivisionTerritorial).FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                if (territorios.Count() == 1)
                {
                    SelectedTipoLocalidad = territorios.FirstOrDefault();
                }
                this.Localidad = new Localidad();
            }
            StateHasChanged();
        }
    }
}
