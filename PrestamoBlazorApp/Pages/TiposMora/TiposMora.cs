using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.TiposMora
{
    public partial class TiposMora : BaseForCreateOrEdit
    {
    
        [Inject]
        TiposMoraService TiposMoraService { get; set; }
        IEnumerable<TipoMora> tiposmora { get; set; } = new List<TipoMora>();
        [Parameter]
        public TipoMora TipoMora { get; set; } 
        bool loading = false;
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
        async Task CreateOrEdit(int idTipoMora = -1)
        {
            if (idTipoMora > 0)
            {
                var param = await TiposMoraService.Get(new TipoMoraGetParams { IdTipoMora = idTipoMora });
                this.TipoMora = param.FirstOrDefault();
            }
            else
            {
                this.TipoMora = new TipoMora();
            }
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
