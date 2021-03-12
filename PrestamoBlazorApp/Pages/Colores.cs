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
        IEnumerable<Color> colores { get; set; } = new List<Color>();
        [Parameter]
        public Color Color { get; set; } 
        bool loading = false;
        void Clear() => colores = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Color = new Color();
        }

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
            await coloresService.SaveColor(this.Color);
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
