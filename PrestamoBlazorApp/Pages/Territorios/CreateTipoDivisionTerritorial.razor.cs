using Microsoft.AspNetCore.Components;
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

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class CreateTipoDivisionTerritorial : BaseForCreateOrEdit
    {
        [Inject]
        DivisionTerritorialService territoriosService { get; set; }
        IEnumerable<DivisionTerritorial> DivisionesTerritoriales { get; set; } = new List<DivisionTerritorial>();
        [Parameter]
        public DivisionTerritorial Territorio { get; set; }
        IEnumerable<DivisionTerritorial> componenteDivision { get; set; } = new List<DivisionTerritorial>();
        void Clear() => DivisionesTerritoriales = new List<DivisionTerritorial>();

        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Territorio = new DivisionTerritorial();
            //this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {

            DivisionesTerritoriales = await territoriosService.GetTiposDivisionTerritorial();
            //.GetTiposDivisionTerritorial();
            if (DivisionesTerritoriales.Count() == 1)
            {
                componenteDivision = await territoriosService.GetDivisionTerritorialComponents(DivisionesTerritoriales.First().IdDivisionTerritorial);
            }
            await LoadTree();
            // await JsInteropUtils.Territorio(jsRuntime);
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
                this.Territorio = DivisionesTerritoriales.Where(m => m.IdDivisionTerritorial == idDivision).FirstOrDefault();
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
    }
}
