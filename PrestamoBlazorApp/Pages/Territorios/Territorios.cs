using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class Territorios
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        TerritoriosService territoriosService { get; set; }
        IEnumerable<Territorio> territorios { get; set; } = new List<Territorio>();
        [Parameter]
        public Territorio Territorio { get; set; }
        bool loading = false;
        void Clear() => territorios = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            //this.Ocupacion = new Ocupacion();
        }
        //protected override async Task OnInitializedAsync()
        //{
        //   // territorios = await territoriosService.GetComponenteDeDivision();
        //   // await JsInteropUtils.Territorio(jsRuntime);
        //}
        public async Task VerTerritorios()
        {
            await JsInteropUtils.Territorio(jsRuntime,"2");
        }
      
    }
}
