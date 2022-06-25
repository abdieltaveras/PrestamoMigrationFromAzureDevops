﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PcpSoft.System;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using PrestamoModelsForFrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class CreateDivisionTerritorialV2 : BaseForCreateOrEdit
    {
        [Inject]
        TerritoriosService territoriosService { get; set; }
        IEnumerable<Territorio> listadeterritorios { get; set; } = new List<Territorio>();
        [Parameter]
        public Territorio Territorio { get; set; }
        IEnumerable<Territorio> componenteDivision { get; set; } = new List<Territorio>();
        private HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();
        private TreeItemData ActivatedValue { get; set; }

        private HashSet<TreeItemData> SelectedValues { get; set; }

        void Clear() => listadeterritorios = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Territorio = new Territorio();
            //this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {

            listadeterritorios = await territoriosService.GetDivisionesTerritoriales();
            componenteDivision = await territoriosService.GetComponenteDeDivision();
            await LoadTreeV2();
            var tree = new TreeItemData();
            await tree.BuilBlazorTree(this.TreeNodes, null);
            this.TreeItems = tree.TreeItems;

            // await JsInteropUtils.Territorio(jsRuntime);
        }
        IEnumerable<ITreeNode> TreeNodes = null;
        private async Task LoadTreeV2()
        {
            TreeBuilder divisionTerritorialTree = null;

            var treeItems = new List<ITreeItem>();

            componenteDivision.First().IdLocalidadPadre = 0; // esto es para hacerlo el nodo raiz
            foreach (var item in componenteDivision)
            {
                var treeItem = new DivisionTerritorialTreeItem(item);
                treeItems.Add(treeItem);
            }
            divisionTerritorialTree = new TreeBuilder(treeItems);
            TreeNodes = divisionTerritorialTree.GetTreeNodes();

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
                this.Territorio = listadeterritorios.Where(m => m.IdDivisionTerritorial == idDivision).FirstOrDefault();
            }
            else
            {
                this.Territorio = new Territorio();
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

