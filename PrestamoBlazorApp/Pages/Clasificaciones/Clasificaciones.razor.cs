using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Clasificaciones
{
    public partial class Clasificaciones : BaseForCreateOrEdit
    {
        [Inject]
        ClasificacionesService ClasificacionesService { get; set; }
        IEnumerable<Clasificacion> clasificaciones { get; set; } = new List<Clasificacion>();
        [Parameter]
        public Clasificacion Clasificacion { get; set; }
        ClasificacionesGetParams SearchClasificacion { get; set; } = new ClasificacionesGetParams();
        void Clear() => clasificaciones = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Clasificacion = new Clasificacion();
        }
        protected override async Task OnInitializedAsync()
        {
            clasificaciones = await ClasificacionesService.Get(new ClasificacionesGetParams { IdNegocio = 1 });
        }
        async Task GetClasificaciones()
        {
            loading = true;
            var param = new ClasificacionesGetParams { IdNegocio = 1 };
            clasificaciones = await ClasificacionesService.Get(param);
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            clasificaciones = await ClasificacionesService.Get(new ClasificacionesGetParams());
            loading = false;
        }

        async Task SaveClasificacion()
        {
            await BlockPage();
            await ClasificacionesService.SaveClasificacion(this.Clasificacion);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "/clasificaciones");
        }
        async Task CreateOrEdit(int idClasificacion = -1)
        {
            await BlockPage();
            if (idClasificacion > 0)
            {
                var datos = await ClasificacionesService.Get(new ClasificacionesGetParams { IdClasificacion = idClasificacion });
                this.Clasificacion = datos.FirstOrDefault();
            }
            else
            {
                this.Clasificacion = new Clasificacion();
            }
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
            await UnBlockPage();
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
