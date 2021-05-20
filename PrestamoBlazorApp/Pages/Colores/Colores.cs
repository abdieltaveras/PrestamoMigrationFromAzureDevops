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
        void Clear() => colores = null;
        
        protected override async Task OnInitializedAsync()
        {
                     
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await BlockPage();
                colores = await coloresService.Get(new ColorGetParams());
                //await UnBlockPage();
                StateHasChanged();
            }
            
        }

        async Task SaveColor()
        {
            var colorExiste = colores.ToList().Find(color => color.Nombre.ToLower() == Color.Nombre.ToLower()) == null ? false : true; ;
            if (colorExiste) {
                await SweetMessageBox("el color que digitaste ya existe, no puedo aceptarlo", "warning","",5000);
                return;
            }

            //await BlockPage();
            await Handle_SaveData(async()=> await coloresService.SaveColor(this.Color),null,null);
            await CreateOrEdit(-1);
            //await UnBlockPage();

        }

        

        async Task CreateOrEdit(int idColor = -1)
        {
            if (idColor > 0)
            {
                await BlockPage();
                var color = await coloresService.Get(new ColorGetParams { IdColor = idColor });
                Color = color.FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                this.Color = new Color();
            }
             await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
