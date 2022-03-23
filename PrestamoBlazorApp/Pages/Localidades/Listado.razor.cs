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

namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class Listado
    {
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        BuscarLocalidadParams buscarLocalidadParams { get; set; } = new BuscarLocalidadParams();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();
        IEnumerable<Territorio> territorios { get; set; } = new List<Territorio>();
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
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null,null,false,"/localidades/listado");
            //await Edit(this.Localidad.IdLocalidad);
            //await UnBlockPage();

        }
        async Task Edit(int idLocalidad = -1)
        {
            if (idLocalidad > 0)
            {
                await BlockPage();
                var localidad =  await localidadesService.Get(new LocalidadGetParams { IdLocalidad = idLocalidad });
                Localidad = localidad.FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                this.Localidad = new Localidad();
            }
            ShowDialog(true);
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
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
        void ShowDialog(bool value) 
        {
            ShowDialogCreate = value;
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
