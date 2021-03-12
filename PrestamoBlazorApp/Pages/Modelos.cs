using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Modelos
    {
        [Inject]
        ModelosService modelosService { get; set; }
        IEnumerable<Modelo> modelos { get; set; } = new List<Modelo>();
        [Parameter]
        public Modelo Modelo { get; set; } 
        bool loading = false;
        void Clear() => modelos = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Modelo = new Modelo();
        }

        async Task GetModelosByParam()
        {
            loading = true;
            var getAzul = new ModeloGetParams { IdModelo = 1 };
            modelos = await modelosService.GetModelosAsync(getAzul);
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            modelos = await modelosService.GetAll();
            loading = false;
        }

        async Task SaveModelo()
        {
            await modelosService.SaveModelo(this.Modelo);
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
