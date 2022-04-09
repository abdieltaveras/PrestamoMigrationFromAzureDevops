﻿using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using MudBlazor;
using PrestamoBlazorApp.Shared.Components.TiposMora;
namespace PrestamoBlazorApp.Pages.TiposMora
{
    public partial class TiposMora : BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        [Inject]
        TiposMoraService TiposMoraService { get; set; }
        IEnumerable<TipoMora> tiposmora { get; set; } = new List<TipoMora>();
        [Parameter]
        public TipoMora TipoMora { get; set; } 
        
        TipoMoraGetParams SearchGarantia { get; set; } = new TipoMoraGetParams();
        void Clear() => tiposmora = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.TipoMora = new TipoMora();
        }
        protected override async Task OnInitializedAsync()
        {
            await BlockPage();
            tiposmora = await TiposMoraService.Get(new TipoMoraGetParams());
            await UnBlockPage();
        }
        async Task GetTiposMora()
        {
            //loading = true;
            await BlockPage();

            tiposmora = await TiposMoraService.Get(new TipoMoraGetParams());
            await UnBlockPage();

            //loading = false;
        }

        //async Task GetAll()
        //{
        //    loading = true;
        //    Garantias = await GarantiasService.GetAll();
        //    loading = false;
        //}

        async Task SaveTipoMora()
        {
            await BlockPage();
            await TiposMoraService.SaveTipoMora(this.TipoMora);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await OnGuardarNotification();
            //NavManager.NavigateTo("/TiposMora");
        }
        async Task CreateOrEdit(int id = -1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdTipoMora", id);
            var dialog = DialogService.Show<CreateTiposMora>("", parameters, dialogOptions);
            var result = await dialog.Result;
            //if (Convert.ToInt32(result.Data.ToString()) == 1)
            //{
            //    //await GetData();
            //    StateHasChanged();
            //}
            //if (idTipoMora > 0)
            //{
            //    var param = await TiposMoraService.Get(new TipoMoraGetParams { IdTipoMora = idTipoMora });
            //    this.TipoMora = param.FirstOrDefault();
            //}
            //else
            //{
            //    this.TipoMora = new TipoMora();
            //}
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }

        //private async Task ShowDialog(int id = -1)
        //{
        //    var parameters = new DialogParameters();
        //    parameters.Add("IdTipoMora", id);
        //    var dialog = DialogService.Show<CreateTiposMora>("", parameters, dialogOptions);
        //    var result = await dialog.Result;
        //    if (Convert.ToInt32(result.Data.ToString()) == 1)
        //    {
        //        //await GetData();
        //        StateHasChanged();
        //    }
        //}
    }
}
