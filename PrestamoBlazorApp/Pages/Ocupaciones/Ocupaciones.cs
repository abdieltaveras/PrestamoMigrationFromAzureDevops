using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages.Ocupaciones
{
    public partial class Ocupaciones
    {
        [Inject]
        OcupacionesService OcupacionesService { get; set; }
        IEnumerable<Ocupacion> ocupaciones { get; set; } = new List<Ocupacion>();
        [Parameter]
        public Ocupacion Ocupacion { get; set; } 
        bool loading = false;
        void Clear() => ocupaciones = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Ocupacion = new Ocupacion();
        }

        async Task GetOcupacionesByParam()
        {
            loading = true;
            var getAzul = new OcupacionGetParams { IdOcupacion = 4 };
            ocupaciones = await OcupacionesService.GetOcupacionesAsync(getAzul);
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            ocupaciones = await OcupacionesService.GetAll();
            loading = false;
        }

        async Task SaveOcupacion()
        {
            await OcupacionesService.SaveOcupacion(this.Ocupacion);
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
