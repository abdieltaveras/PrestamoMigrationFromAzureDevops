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
using System.ComponentModel.DataAnnotations;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    
    public partial class CatalogoEditor : BaseForCreateOrEdit
    {
        // parameters
        [Parameter] public CatalogoInsUpd Catalogo { get; set; } = new CatalogoInsUpd();
        [Parameter] public bool UsarFormularioParaEliminar { get; set; } = false;
    
        [Parameter] public CatalogosService CatalogosService { get; set; }


        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        // injections

        // Members
        private int DeleteConfirmedValue { get; set; }
        [Compare("DeleteConfirmedValue")]
        private int DeleteValueToConfirm { get; }
        [Parameter] public Func<Task> UpdateList { get; set; }

        
        //private CatalogosService CatalogosService { get { return GetService(); } }
        private string ConfirmationMessage { get; set; } 
        private void CloseDlg()
        {
            MudDialog.Cancel();
        }

        public CatalogoEditor()
        {
            DeleteValueToConfirm = new Random().Next(1000, 9999);
        }


        public bool IsDisabledInput => UsarFormularioParaEliminar;

        async Task SaveCatalogo()
        {
            CloseDlg();
            await Handle_SaveData(async () => await CatalogosService.SaveCatalogo(this.Catalogo), null, null,false);
            await UpdateList();
            this.Catalogo = new CatalogoInsUpd();
      
        }
        private async Task CloseModal(int result = -1)
        {
            MudDialog.Close(DialogResult.Ok(result));
        }
        async Task DeleteCatalogo ()
        {
            if (DeleteValueToConfirm != DeleteConfirmedValue)
            {
                ConfirmationMessage = "Valor de confirmacion incorrecto, digitar de nuevo";
                return;
            }
            var deleteParams = new BaseCatalogoDeleteParams { IdRegistro = this.Catalogo.IdRegistro };
            await CatalogosService.DeleteCatalogo(deleteParams);
            await SweetAlertSuccess("Se elimino los datos indicados");
            await UpdateList();
            StateHasChanged();
            CloseDlg();
        }
        
        async void PrintListado(int reportType)
        {
            await BlockPage();
            var catalogoGetParams = new CatalogoReportGetParams();
            catalogoGetParams.ReportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, catalogoGetParams);
            await UnBlockPage();
        }
    }

    //public class OcupacionesEditor : CatalogoEditor
    //{
    //    protected override CatalogosService CatalogosService { get { return GetService(); } }
    //    private CatalogosService GetService()
    //    {
    //        return new OcupacionesServiceV2(CommomInjectionsService.ClientFactory, CommomInjectionsService.Configuration);
    //    }
    //}

}
