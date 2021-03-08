using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Colores
    {
        [Inject]
        ColoresService coloresService { get; set; }
        IEnumerable<Color> colores;
        bool loading = false;
        void Clear() => colores = null;
        
        async Task GetColoresByParam()
        {
            loading = true;
            var getAzul = new ColorGetParams { IdColor = 4 };
            colores = await coloresService.GetColoresAsync(getAzul);
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            colores = await coloresService.GetAll();
            loading = false;
        }

        async Task SaveColor()
        {
            var color = new Color { Codigo = "azul", Nombre = "azul"};
            await coloresService.SaveColor(new Color());
            
        }
    }
}
