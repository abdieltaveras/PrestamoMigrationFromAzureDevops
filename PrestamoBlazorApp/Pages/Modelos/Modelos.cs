using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages.Modelos
{
    public partial class Modelos
    {
        [Inject]
        ModelosService modelosService { get; set; }
        ModeloGetParams SearchModelo { get; set; } = new ModeloGetParams();
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

        //async Task GetModelosByParam()
        //{
        //    loading = true;
        //    var getAzul = new ModeloGetParams { Nombre = SearchModelo.Nombre };
        //    modelos = await modelosService.GetModelosAsync(getAzul);
        //    loading = false;
        //}

        async Task GetModelos()
        {
            loading = true;
            var result = new ModeloGetParams { Nombre = SearchModelo.Nombre };
            modelos = await modelosService.GetAll(result);
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
