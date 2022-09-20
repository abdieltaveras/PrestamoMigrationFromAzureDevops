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
using PcpSoft.MudBlazorHelpers;
using PcpSoft.System;


namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class ListadoLocalidadesV2
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        BuscarLocalidadParams buscarLocalidadParams { get; set; } = new BuscarLocalidadParams();
        IEnumerable<Localidad> Localidades { get; set; } = new List<Localidad>();
        IEnumerable<DivisionTerritorial> territorios { get; set; } = new List<DivisionTerritorial>();
        private int? _SelectedLocalidad = null;
        public int? SelectedLocalidad { get { return _SelectedLocalidad; } set { _SelectedLocalidad = value;  } }

        private string SearchString1 = "";
        private Localidad SelectedItem1 = null;
        private bool FilterFunc1(Localidad element) => FilterFunc(element, SearchString1);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        private bool Dense = true, Hover = true, Bordered = false, Striped = false;
        private HashSet<ITreeItemData> TreeItemsData { get; set; } = new HashSet<ITreeItemData>();
        protected override async Task OnInitializedAsync()
        {
            //this.localidades = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.Localidades = await localidadesService.Get(new LocalidadGetParams());
            this.territorios = await localidadesService.GetComponentesTerritorio();
            var localidadesTreeNodes = await CreateLocalidadesNodes();  // crear los ITreeItems especificos
            this.TreeItemsData = await new MudBlazorTreeBuilder(localidadesTreeNodes).GetTreeItems(); // pasar los ITreeNodes para que genere El tree
                                                                                                  // para mudBlazor
        }
        private async Task<IEnumerable<ITreeNode>> CreateLocalidadesNodes()
        {
            Localidades.First().IdLocalidadPadre = 0; // esto es para hacerlo el nodo raiz
            var treeItems = Localidades.Select(item => new TreeItem(item.IdLocalidad, item.IdLocalidadPadre, item.Nombre));
            return  new TreeBuilder(treeItems).GetTreeNodes();
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
            await ShowDialog(idLocalidad);
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
        private async Task ShowDialog(int id = -1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdLocalidad", id);
            var dialog = DialogService.Show<AddLocalidadesToPais>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (result.Data!= null)
            {
                if (result.Data.ToString() == "1")
                {
                    this.Localidades = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
                    StateHasChanged();
                }
            }
           
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
