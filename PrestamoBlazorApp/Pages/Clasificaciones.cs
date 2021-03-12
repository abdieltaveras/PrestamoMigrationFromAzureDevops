using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Clasificaciones
    {
        [Inject]
        ClasificacionesService ClasificacionesService { get; set; }
        IEnumerable<Clasificacion> clasificaciones { get; set; } = new List<Clasificacion>();
        [Parameter]
        public Clasificacion Clasificacion { get; set; } 
        bool loading = false;
        void Clear() => clasificaciones = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Clasificacion = new Clasificacion();
        }

        async Task GetClasificacionesByParam()
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

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
