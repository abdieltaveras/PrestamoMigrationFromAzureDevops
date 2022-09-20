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
using PrestamoBlazorApp.Shared.Components.Localidades;
namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class Localidades: BaseForCreateOrEdit
    {
     
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        BuscarLocalidadParams buscarLocalidadParams { get; set; } = new BuscarLocalidadParams();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();
        IEnumerable<DivisionTerritorial> territorios { get; set; } = new List<DivisionTerritorial>();
        private int? _SelectedLocalidad = null;
        public int? SelectedLocalidad { get { return _SelectedLocalidad; } set { _SelectedLocalidad = value;  } }

        private string SearchString1 = "";
        private Localidad SelectedItem1 = null;
        private bool FilterFunc1(Localidad element) => FilterFunc(element, SearchString1);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };

        private bool Dense = true, Hover = true, Bordered = false, Striped = false;
        protected override async Task OnInitializedAsync()
        {
            //this.localidades = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.localidades = await localidadesService.Get(new LocalidadGetParams());

            //this.territorios = await localidadesService.GetComponentesTerritorio();
        }
        public async Task HandleLocalidadSelected(BuscarLocalidad buscarLocalidad)
        {
            var lst = await localidadesService.GetComponentesTerritorio();
            territorios = lst.ToList();
            var result = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = buscarLocalidad.IdLocalidad });
            var locate = result.Where(m => m.IdLocalidad == buscarLocalidad.IdLocalidad).FirstOrDefault();
            this.Localidad = locate;
        
        }
        async Task GetLocalidades()
        {
            this.localidades = await localidadesService.Get(new LocalidadGetParams());

        }
        private bool FilterFunc(Localidad element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        async Task CreateOrEdit(int idLocalidad = -1)
        {
            await BlockPage();
            var parameters = new DialogParameters();
            parameters.Add("IdLocalidad", idLocalidad);

            dialogOptions.MaxWidth = MaxWidth.Medium;
            var dialog =  DialogService.Show<Shared.Components.Localidades.CreateLocalidad>("Crear Pais", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetLocalidades();
            }
            //var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = idLocalidad });
            //Localidad = localidad.FirstOrDefault();
            await UnBlockPage();
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
