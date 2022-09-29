using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PcpSoft.MudBlazorHelpers;
using PcpSoft.System;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.DivisionesTerritoriales
{
    public partial class CrudComponentsDivisionTerritorial : BaseForCreateOrEdit
    {
        [Inject]
        DivisionTerritorialService territoriosService { get; set; }
        [Parameter]
        public int IdDivisionTerritorial { get; set; }

        private string DivisionTerritorialName { get; set; }
        private DivisionTerritorial Territorio { get; set; }
        private DivisionTerritorial NewTerritorio { get; set; } = new DivisionTerritorial();
        IEnumerable<DivisionTerritorial> ComponentesDivision { get; set; } = new List<DivisionTerritorial>();
        private HashSet<ITreeItemData> TreeItems { get; set; } = new HashSet<ITreeItemData>();
        private ITreeItemData ActivatedValue { get; set; }
        private IEnumerable<ITreeItemData> SelectedValues { get; set; }
        
        void Clear() => ComponentesDivision = new DivisionTerritorial[0];
        
        
        protected override async Task OnInitializedAsync()
        {
            this.Territorio = new DivisionTerritorial();
            ComponentesDivision = await territoriosService.GetDivisionTerritorialComponents(IdDivisionTerritorial);
            DivisionTerritorialName = ComponentesDivision.FirstOrDefault().Nombre;
            var divisionesTreeNodes = await CreateDivisionesTerritorialesNodes();  // crear los ITreeItems especificos
            this.TreeItems = await new MudBlazorTreeBuilder(divisionesTreeNodes).GetTreeItems(); // pasar los ITreeNodes para que genere El tree para mudBlazor
            await base.OnInitializedAsync();
        }

        private async Task Guardar() { }

        private async Task Cancelar() { }
        private async Task<IEnumerable<ITreeNode>> CreateDivisionesTerritorialesNodes()
        {
            ComponentesDivision.First().IdDivisionTerritorialPadre=0; // esto es para hacerlo el nodo raiz
            var treeItems = ComponentesDivision.Select(item => 
            new TreeItem(item.IdDivisionTerritorial, item.IdDivisionTerritorialPadre, item.Nombre));
            return new TreeBuilder(treeItems).GetTreeNodes();
            
            //TreeBuilder divisionTerritorialTree = null;
            //var treeItems = new List<ITreeItem>();
            //ComponenteDivision.First().IdLocalidadPadre = 0; // esto es para hacerlo el nodo raiz
            //foreach (var item in ComponenteDivision)
            //{
            //    var treeItem = new DivisionTerritorialTreeItem(item);
            //    treeItems.Add(treeItem);
            //}
            //divisionTerritorialTree = new TreeBuilder(treeItems);
            //var items =  divisionTerritorialTree.GetTreeNodes();
            //return items;
        }


        async Task SaveDivisionTerritorial()
        {
            if (this.Territorio.Nombre == string.Empty)
            {
                await OnSaveNotification("Error Al Guardar, llene todos los campos");
            }
            else
            {
                await territoriosService.SaveDivisionTerritorial(this.Territorio);
                await SweetMessageBox("Datos Guardados", "success", "/Territorios/CreateDivisionTerritorial");
                //await SweetAlertSuccess(null, "/Territorios/CreateDivisionTerritorial");
                //await OnGuardarNotification();
                //NavManager.NavigateTo("/Territorios");
            }


        }

        void CreateOrEdit(int idDivision = -1)
        {
            if (idDivision > 0)
            {
                this.Territorio = ComponentesDivision.Where(m => m.IdDivisionTerritorial == idDivision).FirstOrDefault();
            }
            else
            {
                this.Territorio = new DivisionTerritorial();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {

        }

        //public static void DisplayTo(this IEnumerable<ITreeNode> nodes, TreeItemData mbTreeItem =null)
        //{
        //    foreach (var node in nodes)
        //    {

        //        //Trace.WriteLine($"{indent} {node.NodeId} {node.NodeText}");
        //        //DisplayTo(node.ChildNodes, displayAction, indent + " ");
        //    }
        //}

        protected void CheckedChanged(TreeItemData item)
        {
            item.IsChecked = !item.IsChecked;
            // checked status on any child items should mirrror this parent item
            if (item.HasChild)
            {
                foreach (TreeItemData child in item.TreeItems)
                {
                    child.IsChecked = item.IsChecked;
                }
            }
            // if there's a parent and all children are checked/unchecked, parent should match
            if (item.Parent != null)
            {
                item.Parent.IsChecked = !item.Parent.TreeItems.Any(i => !i.IsChecked);
            }
        }
    }
}

