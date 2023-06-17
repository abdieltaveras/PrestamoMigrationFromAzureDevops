using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoEntidades;
using PrestamoBlazorApp.Services;
using PcpUtilidades;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Shared.Components.Negocios
{
    public partial class CreateNegocios : BaseForCreateOrEdit
    {
        [Inject]
        public NegociosService NegociosService { get; set; }
        [Parameter]
        public Negocio Negocio { get; set; } = new Negocio();
        private LocalidadNegociosGetParams LocalidadNegociosGetParams { get; set; }
        private IEnumerable<LocalidadNegocio> localidadesnegocios { get; set; }
        [Parameter]
        public int IdNegocio { get; set; } = -1;


        public Guid Guid = Guid.NewGuid();
        public string ModalDisplay = "none;";
        public string ModalClass = "";
        public bool ShowBackdrop = false;

        protected override async Task OnInitializedAsync()
        {
            Negocio = new Negocio();
            await CreateOrEdit();
        }
        async Task CreateOrEdit(int id = -1)
        {
            //await BlockPage();
            if (IdNegocio > 0)
            {
                var datos = await NegociosService.Get(new NegociosGetParams { IdNegocio = IdNegocio });
                this.Negocio = datos.FirstOrDefault();
            }
            else
            {
                this.Negocio = new Negocio();
            }
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
            //await UnBlockPage();
        }
        private async Task<bool> SaveLocalidadNegocio()
        {
          
            try
            {
                await Handle_SaveData(()=> NegociosService.Post(this.Negocio),()=> NotifyMessageBySnackBar("Guardado Correctamente",MudBlazor.Severity.Success),()=>HandleInvalidSubmit(),false,"/negocios");
            }
            catch (ValidationObjectException e)
            {
     
            }
            return true;
        }

        private async Task HandleInvalidSubmit()
        {
        }

        public async Task ShowModal(int id =-1)
        {
            await CreateOrEdit(id);
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public async Task CloseModal()
        {
            //await JsInteropUtils.CloseModal(jsRuntime, "#ModalCreateOrEdit");
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }
    }
}
