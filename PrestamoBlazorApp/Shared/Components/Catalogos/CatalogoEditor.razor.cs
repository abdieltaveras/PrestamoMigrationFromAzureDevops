﻿using PrestamoEntidades;
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

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        // injections
        [Inject] CatalogosService CatalogosService { get; set; }
        // Members
        private int DeleteConfirmedValue { get; set; }
        [Compare("DeleteConfirmedValue")]
        private int DeleteValueToConfirm { get; } 
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
            await Handle_SaveData(async () => await CatalogosService.SaveCatalogo(this.Catalogo), null, null,false,"Reload");
            this.Catalogo = new CatalogoInsUpd();
            
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

}
