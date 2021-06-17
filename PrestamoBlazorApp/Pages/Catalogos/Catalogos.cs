using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class Catalogos
    {
        //ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        //[Inject]
        //IJSRuntime jsRuntime { get; set; }

        //JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        //[Inject]
        //CatalogosService catalogosService { get; set; }
        //IEnumerable<Color> colores { get; set; } = new List<Color>();
        //[Parameter]
        //public Color Color { get; set; } 
        //bool loading = false;
        //void Clear() => colores = null;
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Color = new Color();
        //}
        //protected override async Task OnInitializedAsync()
        //{
        //    colores = await coloresService.Get();
        //}
        //async Task GetColoresByParam()
        //{
        //    loading = true;
        //    var getAzul = new ColorGetParams { IdColor = 4 };
        //    colores = await coloresService.GetColoresAsync(getAzul);
        //    loading = false;
        //}

        //async Task Get()
        //{
        //    loading = true;
        //    colores = await coloresService.Get();
        //    loading = false;
        //}

        //async Task SaveColor()
        //{
        //    await coloresService.SaveColor(this.Color);
        //}
        //void CreateOrEdit(int idColor = -1)
        //{
        //    if (idColor > 0)
        //    {
        //        this.Color = colores.Where(m => m.IdColor == idColor).FirstOrDefault();
        //    }
        //    else
        //    {
        //        this.Color = new Color();
        //    }
        //    JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        //}

        //void RaiseInvalidSubmit()
        //{
            
        //}
    }
}
