using Microsoft.AspNetCore.Components;
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
    public partial class CreateDivisionTerritorial : BaseForCreateOrEdit
    {
        [Inject]
        TerritoriosService territoriosService { get; set; }
        IEnumerable<Territorio> listadeterritorios { get; set; } = new List<Territorio>();
        [Parameter]
        public Territorio Territorio { get; set; }
        IEnumerable<Territorio> componenteDivision { get; set; } = new List<Territorio>();
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
            await LoadTree();
            
            // await JsInteropUtils.Territorio(jsRuntime);
        }
        private async Task LoadTreeV2()
        {
            TreeBuilder divisionTerritorialTree = null;
            IEnumerable<ITreeNode> treeNodes = null;
            var treeItems = new List<ITreeItem>();

            componenteDivision.First().IdLocalidadPadre = 0; // esto es para hacerlo el nodo raiz
            foreach (var item in componenteDivision)
            {
                var treeItem = new DivisionTerritorialTreeItem(item);
                treeItems.Add(treeItem);
            }
            divisionTerritorialTree = new TreeBuilder(treeItems);
            treeNodes = divisionTerritorialTree.GetTreeNodes();

        }
        private async Task LoadTree()
        {
            TreeItems = new HashSet<TreeItemData>();
            
            foreach (var item in componenteDivision)
            {
                if (TreeItems.Where(m=>m.Text == item.Nombre).Count()>0)
                {
                    return;
                }
                TreeItems.Add(new TreeItemData(item.Nombre) { TreeItems = new HashSet<TreeItemData>() { 
                    new TreeItemData(item.Nombre)
                
                } });
            }
        
        }
        public async Task VerTerritorios(string id)
        {
            await JsInteropUtils.Territorio(jsRuntime,id);
        }
        async Task SaveDivisionTerritorial()
        {
            if(this.Territorio.Nombre == string.Empty)
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
    }
}
