using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpSoft.MudBlazorHelpers;
using PcpSoft.System;
using PrestamoBlazorApp.Pages.DivisionesTerritoriales;
using PrestamoBlazorApp.Pages.Localidades.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class LocalidadesTree: BaseForCreateOrEdit
    {
        [Inject]
        LocalidadesService _LocalidadesService { get; set; }
        [Inject]
        DivisionTerritorialService _DivisionTerritorialService { get; set; }
        [Parameter]
        public int IdLocalidad { get; set; }
        IEnumerable<Localidad> ComponentesLocalidades { get; set; } = new List<Localidad>();
        private HashSet<ITreeItemData> TreeItems { get; set; } = new HashSet<ITreeItemData>();
        private ITreeItemData ActivatedValue { get; set; }
        private IEnumerable<ITreeItemData> SelectedValues { get; set; }
        private bool isLoadingTree { get; set; } = false;
        private string SelectedValue { get; set; } = string.Empty;
        private string DefaultIcon { get; set; } = MudBlazor.Icons.Material.Filled.Expand;
        IEnumerable<DivisionTerritorial> Territorials { get; set; } = new List<DivisionTerritorial>();
        protected override async Task OnInitializedAsync()
        {
            await GetData();

            await base.OnInitializedAsync();
        }
        private async Task GetData()
        {
            var divisiones = await _DivisionTerritorialService.GetDivisionesTerritoriales(new DivisionTerritorialGetParams { });
            Territorials = divisiones;
            this.isLoadingTree = true;
            //this.Territorio = new DivisionTerritorial();
            IdLocalidad = 1;
            var  data = await _LocalidadesService.GetLocalidadesComponents(new LocalidadesComponentGetParams { IdLocalidad = IdLocalidad});
            ComponentesLocalidades = data.Data;
            //DivisionTerritorialName = ComponentesDivision.FirstOrDefault().Nombre;
            var localidadesTreeNodes = await CreateDivisionesTerritorialesNodes();  // crear los ITreeItems especificos
            this.TreeItems = await new MudBlazorTreeBuilder(localidadesTreeNodes).GetTreeItemsWithIcon(DefaultIcon); // pasar los ITreeNodes para que genere El tree para mudBlazor
            this.isLoadingTree = false;
            StateHasChanged();
        }
        private bool AllowAdd(int id)
        {
            //si no tiene componenetes hijos el tipo de division territorial no debe permitir añadir
            var division = ComponentesLocalidades.Where(m => m.IdLocalidad == id).FirstOrDefault();
            var div = Territorials.Where(m => m.IdDivisionTerritorialPadre == division.IdDivisionTerritorial);
            if (div.Any())
            {
                return true;
            }
            return false;
        }
        private async Task OnClickAdd(int? idItem, string textItem)
        {
            
            var item = this.TreeItems.Where(m => m.IdTreeItem == idItem);
            DialogParameters parameters = new DialogParameters();
            
            var division = ComponentesLocalidades.Where(m => m.IdLocalidad == idItem).FirstOrDefault();
            parameters.Add("IdDivisionTerritorialPadre", division.IdDivisionTerritorial);
            parameters.Add("IdLocalidad", idItem);

            var dialog = await svrDialogService.ShowAsync<CCreateLocalidad>("", parameters, OptionsForDialog.SmallFullWidthCloseButtonCenter);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetData();
                this.StateHasChanged();
            }
            SelectedValue = $"{idItem}: {textItem}";
            //await NotifyMessageBySnackBar("agregar proceso para agregar componente a " + SelectedValue,Severity.Info );
        }
        private async Task OnClickDelete(int? idItem, string textItem)
        {
            SelectedValue = $"{idItem} {textItem}";
            await NotifyMessageBySnackBar("agregar proceso para Eliminar componente a " + SelectedValue, Severity.Info);
        }
        private async Task<IEnumerable<ITreeNode>> CreateDivisionesTerritorialesNodes()
        {
            ComponentesLocalidades.First().IdLocalidadPadre = 0; // esto es para hacerlo el nodo raiz
            var treeItems = ComponentesLocalidades.Select(item =>
            new TreeItem(item.IdLocalidad, item.IdLocalidadPadre, item.Nombre));
            var result = new TreeBuilder(treeItems).GetTreeNodes();
            return result;
        }

    }
}
