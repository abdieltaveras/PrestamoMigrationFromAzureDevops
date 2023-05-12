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

namespace PrestamoBlazorApp.Pages.Clasificaciones
{
    public partial class CreateClasificaciones : BaseForCreateOrEdit
    {
        [Inject]
        ClasificacionesService ClasificacionesService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        //IEnumerable<Clasificacion> clasificaciones { get; set; } = new List<Clasificacion>();
        [Parameter]
        public int idClasificacion { get; set; } = -1;
        [Parameter]
        public Clasificacion Clasificacion { get; set; } = new Clasificacion();

        protected override async Task OnInitializedAsync()
        {
            await CreateOrEdit();
        }

        async Task CreateOrEdit()
        {
            //await BlockPage();
            if (idClasificacion > 0)
            {
                var datos = await ClasificacionesService.Get(new ClasificacionesGetParams { IdClasificacion = idClasificacion });
                this.Clasificacion = datos.FirstOrDefault();
            }
            else
            {
                this.Clasificacion = new Clasificacion();
            }
            //await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
            //await UnBlockPage();
        }
        async Task SaveClasificacion()
        {
            await BlockPage();
            await ClasificacionesService.SaveClasificacion(this.Clasificacion);
            await UnBlockPage();
            await CloseModal();
            
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
