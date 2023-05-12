using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
using MudBlazor;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{
    
    public partial class CreateOrEditCatalogo : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

   
        // parameters
        [Parameter]
        public CatalogoInsUpd Catalogo { get; set; } = new CatalogoInsUpd();
        [Parameter]
        public BaseCatalogoGetParams CatalogoGetParams { get; set; } = new BaseCatalogoGetParams();
        // injections
        [Inject]
        CatalogosService CatalogosService { get; set; }
        // Members
        private string SearchString1 = "";
        private CatalogoInsUpd SelectedItem1 = null;
        private bool FilterFunc1(CatalogoInsUpd element) => FilterFunc(element, SearchString1);
       
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        private bool Dense = true, Hover = true, Bordered = false, Striped = false;
        private BaseForList BaseForList { get; set; }
        private IEnumerable<CatalogoInsUpd> Catalogos { get; set; } = new List<CatalogoInsUpd>();
        
      


       
        //note CloseButton = true was removed cause when clicked the dialog does not close };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BlockPage();
                
                Catalogos = await CatalogosService.Get(new BaseCatalogoGetParams());
                //JsonConvert.DeserializeObject<IEnumerable< Catalogo>>(lista.FirstOrDefault().ToString() );
                await UnBlockPage();
                StateHasChanged();
            }

        }
        async Task SaveCatalogo()
        {
            ShowDialog(false);
            await Handle_SaveData(async () => await CatalogosService.SaveCatalogo(this.Catalogo), null, null,false,"Reload");
            this.Catalogo = new CatalogoInsUpd();
            
        }
        async Task CreateOrEdit(int Id = -1)
        {
            if (Id > 0)
            {
                await BlockPage();
                
                var catalogoGetParams = new BaseCatalogoGetParams();
                catalogoGetParams.IdRegistro = Id;
                var lista = await CatalogosService.Get(catalogoGetParams);
                var catalogo = lista.ToList().FirstOrDefault();
                //JsonConvert.DeserializeObject<IEnumerable<Catalogo>>(lista.FirstOrDefault().ToString()).FirstOrDefault();
                Catalogo.Nombre = catalogo.Nombre;
                Catalogo.Codigo = catalogo.Codigo;
                Catalogo.IdRegistro = catalogo.IdRegistro;
                await UnBlockPage();
            }
            else
            {
                this.Catalogo = new CatalogoInsUpd();
            }
            ShowDialog(true);
            StateHasChanged();
        }
        //private async Task ShowCreateModal(bool value)
        //{
        //    ShowDialogCreate = value;
        //}
        async Task Eliminar ()
        {
            var a = await OnDeleteConfirm("Desea Eliminar?", " (OnDeleteConfirm)"); //Funciona
            if (a == true)
            {
                //await SweetMessageBox("Eliminado");
            }

        }
        void RaiseInvalidSubmit()
        {

        }
        async void PrintListado(int reportType)
        {
            await BlockPage();
            var catalogoReportParams = new CatalogoReportGetParams();
            catalogoReportParams.ReportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, catalogoReportParams);
            await UnBlockPage();
        }
        private bool FilterFunc(CatalogoInsUpd element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo!=null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        void ShowDialog(bool value) => ShowDialogCreate = value;
    }

}
