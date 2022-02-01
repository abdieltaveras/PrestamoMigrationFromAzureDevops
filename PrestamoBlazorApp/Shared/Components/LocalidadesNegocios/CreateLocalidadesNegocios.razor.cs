using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoEntidades;
using PrestamoBlazorApp.Services;
using PcpUtilidades;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Shared.Components.LocalidadesNegocios
{
    public partial class CreateLocalidadesNegocios : BaseForCreateOrEdit
    {
        [Inject]
        public LocalidadesNegociosService LocalidadesNegociosService { get; set; }
        public LocalidadNegocio LocalidadNegocio { get; set; }
        private LocalidadNegociosGetParams LocalidadNegociosGetParams { get; set; }
        private IEnumerable<LocalidadNegocio> localidadesnegocios { get; set; }
        [Parameter]
        public int IdLocalidadNegocio { get; set; } = -1;


        public Guid Guid = Guid.NewGuid();
        public string ModalDisplay = "none;";
        public string ModalClass = "";
        public bool ShowBackdrop = false;

        protected override async Task OnInitializedAsync()
        {
            LocalidadNegocio = new LocalidadNegocio();
            await CreateOrEdit();
        }
        async Task CreateOrEdit(int id = -1)
        {
            //await BlockPage();
            if (IdLocalidadNegocio > 0)
            {
                var datos = await LocalidadesNegociosService.Get(new LocalidadNegociosGetParams { Opcion = 2, IdLocalidadNegocio = IdLocalidadNegocio });
                this.LocalidadNegocio = datos.FirstOrDefault();
            }
            else
            {
                this.LocalidadNegocio = new LocalidadNegocio();
            }
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
            //await UnBlockPage();
        }
        private async Task<bool> SaveLocalidadNegocio()
        {
           
            LocalidadNegocio.IdLocalidadNegocio = 1;
            try
            {
                await Handle_SaveData(()=> LocalidadesNegociosService.Post(this.LocalidadNegocio),null,()=>HandleInvalidSubmit(),false,"/localidadesnegocios");
            }
            catch (ValidationObjectException e)
            {
                await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }
            return true;
        }

        private async Task HandleInvalidSubmit()
        {
           await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Error al validar los datos.");
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
