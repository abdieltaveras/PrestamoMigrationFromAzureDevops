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
        BaseForCreateOrEdit BaseForCreateOrEdit = new BaseForCreateOrEdit();
        ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
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
        protected override async Task OnInitializedAsync()
        {
            await BlockPage();
            colores = await coloresService.Get(new ColorGetParams());
            await UnBlockPage();
        }
        //async Task GetColoresByParam()
        //{
        //    loading = true;
        //    var getAzul = new ColorGetParams { IdColor = 4 };
        //    colores = await coloresService.GetColoresAsync(getAzul);
        //    loading = false;
        //}

        //async List<Color> Get(ColorGetParams colorGetParams)
        //{
        //    //loading = true;
        //    return await coloresService.Get(colorGetParams);
        //    //loading = false;
        //}

        async Task SaveColor()
        {
            await BlockPage();
            await coloresService.SaveColor(this.Color);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
        }
        async void CreateOrEdit(int idColor = -1)
        {
            await BlockPage();
            //var color = new IEnumerable<Color>();
            if (idColor > 0)
            {
                var color = await coloresService.Get(new ColorGetParams { IdColor = idColor });
                this.Color = color.FirstOrDefault();
            }
            else
            {
                this.Color = new Color();
            }
            await UnBlockPage();
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
