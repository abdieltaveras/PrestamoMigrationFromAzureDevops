using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Localidades.Components
{
    public partial class CCreateLocalidad
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
        [Parameter]
        public int IdDivisionTerritorialPadre { get; set; } = -1;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        IEnumerable<Localidad> localidadesByTipo { get; set; } = new List<Localidad>();
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
            this.Localidad.IdDivisionTerritorial = SelectedTipoLocalidad.IdDivisionTerritorial;
            this.Localidad.IdLocalidadPadre = IdLocalidad;
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null, false, mudDialogInstance: MudDialog);

        }
        private async Task CloseModal()
        {
            MudDialog.Close(DialogResult.Ok(""));
        }
        async Task CreateOrEdit()
        {
            await BlockPage();
            await GetDivisionesByIdPadre();
            //var divisiones = await territoriosService.GetDivisionesTerritoriales(new DivisionTerritorialGetParams { IdDivisionTerritorialPadre = IdDivisionTerritorial });
            //territorios = await localidadesService.GetComponentesTerritorio();
            //if (IdLocalidad > 0)
            //{

            //    var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = IdLocalidad });
            //    Localidad = localidad.FirstOrDefault();
            //    //territorios = await localidadesService.GetComponentesTerritorio();
            //    SelectedTipoLocalidad = territorios.Where(m => m.IdDivisionTerritorial == Localidad.IdTipoDivisionTerritorial).FirstOrDefault();

            //}
            //else
            //{
            //    if (territorios.Count() == 1)
            //    {
            //        SelectedTipoLocalidad = territorios.FirstOrDefault();
            //    }
            //    this.Localidad = new Localidad();
            //}
            await UnBlockPage();
            StateHasChanged();
        }
        private async Task GetDivisionesByIdPadre()
        {
            var divisiones = await territoriosService.GetDivisionesTerritoriales(new DivisionTerritorialGetParams { IdDivisionTerritorialPadre = IdDivisionTerritorialPadre });
            territorios = divisiones;
            StateHasChanged();
        }
    }
}
