using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
namespace PrestamoBlazorApp.Pages.Colores
{
    public partial class Colores : BaseForCreateOrEdit
    {
        IJSRuntime jSRuntime { get; set; }
        ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        [Inject]
        ColoresService coloresService { get; set; }
        [Inject]
        CatalogosService catalogosService { get; set; }
        IEnumerable<Color> colores { get; set; } = new List<Color>();
        //public Color Color { get; set; } = new Color();
        public Catalogo Catalogo { get; set; } = new Catalogo { NombreTabla = "tblColores", IdTabla = "idcolor" };
        
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblColores", IdTabla = "idcolor" };
        void Clear() => colores = null;
        void ClearCatalogo() 
        {
            Catalogo = new Catalogo { NombreTabla = "tblColores", IdTabla = "idcolor" };
            
        }
        async void PrintListado()
        {
            await BlockPage();
            CatalogoGetParams.reportType = 2;
            var result = await catalogosService.ReportListado(jsRuntime,CatalogoGetParams);
            await UnBlockPage();
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
