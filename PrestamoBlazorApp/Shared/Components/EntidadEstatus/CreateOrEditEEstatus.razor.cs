﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Shared.Components.EntidadEstatus
{
    public partial class CreateOrEditEEstatus : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        EstatusService EntidadEstatusService { get; set; }
        [Parameter]
        public int IdEntidadEstatus { get; set; }

        PrestamoEntidades.Estatus EntidadEstatus { get; set; } = new PrestamoEntidades.Estatus();
        protected override async Task OnInitializedAsync()
        {
            //EntidadEstatus = new PrestamoEntidades.EntidadEstatus();
            await CreateOrEdit();
            //await base.OnInitializedAsync();
        }

        async Task CreateOrEdit()
        {
            //await BlockPage();
            if (IdEntidadEstatus > 0)
            {
                var data = await EntidadEstatusService.Get(new EstatusGetParams { IdEstatus = IdEntidadEstatus });
                EntidadEstatus = data.FirstOrDefault();
            }
            else
            {
                this.EntidadEstatus = new PrestamoEntidades.Estatus();
            }
            StateHasChanged();
            //await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task Save()
        {

            await BlockPage();
            await Handle_SaveData(async () => await EntidadEstatusService.Save(this.EntidadEstatus), null, null, false, mudDialogInstance: MudDialog);
            await UnBlockPage();
         

            //await Handle_Funct(async () => await EntidadEstatusService.Save(this.EntidadEstatus));

            //await SweetAlertSuccess("Datos Guardados Correctamente");
            await UnBlockPage();
        }

    }
}
