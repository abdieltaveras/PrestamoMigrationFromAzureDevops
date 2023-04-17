using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using MudBlazor;

namespace PrestamoBlazorApp.Shared.Components.TiposMora
{
    public partial class CreateTiposMora : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        TiposMoraService TiposMoraService { get; set; }
        IEnumerable<TipoMora> tiposmora { get; set; } = new List<TipoMora>();
        [Parameter]
        public TipoMora TipoMora { get; set; } = new TipoMora();
        [Parameter]
        public int IdTipoMora { get; set; }
        TipoMoraGetParams SearchGarantia { get; set; } = new TipoMoraGetParams();
        void Clear() => tiposmora = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
           
        }
        protected override async Task OnInitializedAsync()
        {
            TipoMora = new TipoMora();
            await BlockPage();
            tiposmora = await TiposMoraService.Get(new TipoMoraGetParams());
            await CreateOrEdit();
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
            await CloseModal();
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await OnGuardarNotification();
            //NavManager.NavigateTo("/TiposMora");
        }
        async Task CreateOrEdit()
        {
            if (IdTipoMora > 0)
            {
                var param = await TiposMoraService.Get(new TipoMoraGetParams { IdTipoMora = IdTipoMora });
                this.TipoMora = param.FirstOrDefault();
            }
            else
            {
                this.TipoMora = new TipoMora();
            }
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        private async Task CloseModal(int result = -1)
        {
            MudDialog.Close(DialogResult.Ok(result));
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
