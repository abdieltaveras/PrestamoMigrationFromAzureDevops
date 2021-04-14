using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Pages.TiposMora
{
    public partial class TiposMora
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
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
            tiposmora = await TiposMoraService.Get(new TipoMoraGetParams());
        }
        async Task GetTiposMora()
        {
            loading = true;
            tiposmora = await TiposMoraService.Get(new TipoMoraGetParams());
            loading = false;
        }

        //async Task GetAll()
        //{
        //    loading = true;
        //    Garantias = await GarantiasService.GetAll();
        //    loading = false;
        //}

        async Task SaveTipoMora()
        {
            await TiposMoraService.SaveTipoMora(this.TipoMora);
        }
        void CreateOrEdit(int idTipoMora = -1)
        {
            if (idTipoMora > 0)
            {
                this.TipoMora = tiposmora.Where(m => m.IdTipoMora == idTipoMora).FirstOrDefault();
            }
            else
            {
                this.TipoMora = new TipoMora();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
