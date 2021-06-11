using PrestamoBLL.Entidades;
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
        ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        [Inject]
        ColoresService coloresService { get; set; }
        IEnumerable<Color> colores { get; set; } = new List<Color>();
        [Parameter]
        public Color Color { get; set; } = new Color();
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo { NombreTabla = "tblColores", IdTabla = "idcolor" };
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblColores", IdTabla = "idcolor" };
        void Clear() => colores = null;
        void ClearCatalogo() 
        {
            Catalogo = new Catalogo { NombreTabla = "tblColores", IdTabla = "idcolor" };
            
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
