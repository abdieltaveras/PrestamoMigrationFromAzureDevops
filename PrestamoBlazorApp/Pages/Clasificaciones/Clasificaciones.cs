using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Pages.Clasificaciones
{
    public partial class Clasificaciones
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        ClasificacionesService ClasificacionesService { get; set; }
        IEnumerable<Clasificacion> clasificaciones { get; set; } = new List<Clasificacion>();
        [Parameter]
        public Clasificacion Clasificacion { get; set; } 
        bool loading = false;
        ClasificacionesGetParams SearchClasificacion { get; set; } = new ClasificacionesGetParams();
        void Clear() => clasificaciones = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Clasificacion = new Clasificacion();
        }
        protected override async Task OnInitializedAsync()
        {
            clasificaciones = await ClasificacionesService.GetClasificacionesAsync(new ClasificacionesGetParams { IdNegocio = 1});
        }
        async Task GetClasificaciones()
        {
            loading = true;
            var param = new ClasificacionesGetParams { IdNegocio = 1 };
            clasificaciones = await ClasificacionesService.GetClasificacionesAsync(param);
            loading = false;
        }

        //async Task GetAll()
        //{
        //    loading = true;
        //    clasificaciones = await ClasificacionesService.GetAll();
        //    loading = false;
        //}

        async Task SaveClasificacion()
        {
            await ClasificacionesService.SaveClasificacion(this.Clasificacion);
        }
        void CreateOrEdit(int idClasificacion = -1)
        {
            if (idClasificacion > 0)
            {
                this.Clasificacion = clasificaciones.Where(m => m.IdClasificacion == idClasificacion).FirstOrDefault();
            }
            else
            {
                this.Clasificacion = new Clasificacion();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
