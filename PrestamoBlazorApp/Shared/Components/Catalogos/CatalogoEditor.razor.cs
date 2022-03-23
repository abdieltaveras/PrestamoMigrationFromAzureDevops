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
    
    public partial class CatalogoEditor : BaseForCreateOrEdit
    {
        // parameters
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo();
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams();
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        // injections
        [Inject]
        CatalogosService CatalogosService { get; set; }
        // Members
        private string SearchString1 = "";
        
        //private Catalogo SelectedItem1 = null;
        
        //private BaseForList BaseForList { get; set; }
        //private IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();
        
        //private bool Dense=true, Hover=true, Bordered=false, Striped=false;


        //private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        //note CloseButton = true was removed cause when clicked the dialog does not close };
        
        bool validForm;
        string[] errors = { };
        MudForm form;
        private void CloseDlg()
        {
            MudDialog.Cancel();
        }

        async Task SaveCatalogo()
        {
            CloseDlg();
            await Handle_SaveData(async () => await CatalogosService.SaveCatalogo(this.Catalogo), null, null,false,"Reload");
            this.Catalogo = new Catalogo();
            
        }
        
        async Task DelCatalogo ()
        {
            var a = await OnDeleteConfirm("Desea Eliminar?", " (OnDeleteConfirm)"); //Funciona
            if (a == true)
            {
                await SweetMessageBox("Eliminado");
            }

        }
        
        async void PrintListado(int reportType)
        {
            await BlockPage();
            CatalogoGetParams.reportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, CatalogoGetParams);
            await UnBlockPage();
        }
    }

}
