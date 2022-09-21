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
    public partial class AddLocalidadesToPais : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Inject]
        DivisionTerritorialService DivisionTerritorialService { get; set; }
        Localidad LocalidadTipoSelected { get; set; } = new Localidad();
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
        private PrestamoEntidades.DivisionTerritorial _SelectedDivisionTerritorial { get; set; } 
        public PrestamoEntidades.DivisionTerritorial SelectedDivisionTerritorial { get { return _SelectedDivisionTerritorial; } set { _SelectedDivisionTerritorial = value; GetLocalidadesByTipo().GetAwaiter(); } }
        //public int IdLocalidadByTipoSelected { get; set; }
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
            if (LocalidadTipoSelected.IdLocalidad > 0)
            {
                this.Localidad.IdDivisionTerritorial = SelectedDivisionTerritorial.IdDivisionTerritorial;
                this.Localidad.IdLocalidadPadre = LocalidadTipoSelected.IdLocalidad;
                await CloseModal("1");
                await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null, false, "/localidades/listadopaises");
            }
            else
            {
               await SweetMessageBox("Dene seleccionar una localidad para agregar el hijo.", "error", "");
            }
        }
        async Task CreateOrEdit()
        {
            var divTerr = await DivisionTerritorialService.GetDivisionTerritorialComponents(IdLocalidad);
            territorios = divTerr.Where(m => m.IdDivisionTerritorial != IdLocalidad && m.IdDivisionTerritorialPadre != IdLocalidad);
            //territorios = territorios.Where(m => m.IdDivisionTerritorialPadre != IdLocalidad);

            if (IdLocalidad > 0)
            {
                await BlockPage();
                //var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = IdLocalidad });
                //Localidad = localidad.FirstOrDefault();
                //territorios = await localidadesService.GetComponentesTerritorio();
                var ter = territorios.Where(m => m.IdDivisionTerritorial == Localidad.IdDivisionTerritorial);
                SelectedDivisionTerritorial = ter.FirstOrDefault();
                LocalidadTipoSelected = Localidad;
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
            var loc  = await localidadesService.Get(new LocalidadGetParams { IdDivisionTerritorial = (int)SelectedDivisionTerritorial.IdDivisionTerritorialPadre });
            localidadesByTipo = loc;
            if (loc.Count()>0)
            {
                LocalidadTipoSelected = localidadesByTipo.FirstOrDefault();
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
