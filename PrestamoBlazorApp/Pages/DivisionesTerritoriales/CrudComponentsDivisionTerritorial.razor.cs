﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using PcpSoft.MudBlazorHelpers;
using PcpSoft.System;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
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

        private string SelectedValue { get; set; } = string.Empty;
        private string DivisionTerritorialName { get; set; }
        private DivisionTerritorial Territorio { get; set; }
        private DivisionTerritorial NewTerritorio { get; set; } = new DivisionTerritorial();
        IEnumerable<DivisionTerritorial> ComponentesDivision { get; set; } = new List<DivisionTerritorial>();
        private HashSet<ITreeItemData> TreeItems { get; set; } = new HashSet<ITreeItemData>();
        private ITreeItemData ActivatedValue { get; set; }
        private IEnumerable<ITreeItemData> SelectedValues { get; set; }

        private string DefaultIcon { get; set; } = MudBlazor.Icons.Material.Filled.Expand;


        void Clear() => ComponentesDivision = new DivisionTerritorial[0];
        
        private async Task OnClickAdd(int? idItem, string textItem)
        {
            
            SelectedValue = $"{idItem}: {textItem}";
            await NotifyMessageBySnackBar("agregar proceso para agregar componente a " + SelectedValue,Severity.Info );
        }
        protected override async Task OnInitializedAsync()
        {
            
            this.Territorio = new DivisionTerritorial();
            ComponentesDivision = await territoriosService.GetDivisionTerritorialComponents(IdDivisionTerritorial);
            DivisionTerritorialName = ComponentesDivision.FirstOrDefault().Nombre;
            var divisionesTreeNodes = await CreateDivisionesTerritorialesNodes();  // crear los ITreeItems especificos
            this.TreeItems = await new MudBlazorTreeBuilder(divisionesTreeNodes).GetTreeItemsWithIcon(DefaultIcon); // pasar los ITreeNodes para que genere El tree para mudBlazor
            await base.OnInitializedAsync();
        }

        private async Task Guardar() { }

        private async Task Cancelar() { }
        private async Task<IEnumerable<ITreeNode>> CreateDivisionesTerritorialesNodes()
        {
            ComponentesDivision.First().IdDivisionTerritorialPadre=0; // esto es para hacerlo el nodo raiz
            var treeItems = ComponentesDivision.Select(item => 
            new TreeItem(item.IdDivisionTerritorial, item.IdDivisionTerritorialPadre, item.Nombre));
            var result = new TreeBuilder(treeItems).GetTreeNodes();
            return result;
            
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
            //JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
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

        protected void CheckedChanged(ITreeItemData item)
        {
            item.IsChecked = !item.IsChecked;
            // checked status on any child items should mirrror this parent item
            if (item.HasChild)
            {
                foreach (ITreeItemData child in item.TreeItems)
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

