using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using MudBlazor;
//using PrestamoBlazorApp.Pages.Base;
//using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Marcas
{
    
    public partial class Marcas : BaseForList
    {
        private string SearchString1 = "";
        private Marca SelectedItem1 = null;
        private bool FilterFunc1(Marca element) => FilterFunc(element, SearchString1);
        [Parameter]
        public BaseCatalogoGetParams CatalogoGetParams { get; set; } = new BaseCatalogoGetParams();
        MarcaGetParams SearchMarca { get; set; } = new MarcaGetParams();
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        MarcasService marcasService { get; set; }
        [Inject]
        ModelosService ModelosService { get; set; }
        public IEnumerable<Marca> marcas { get; set; } = new List<Marca>();
        [Inject]
        IDialogService DialogService { get; set; }
        [Parameter]
        public Marca Marca { get; set; }
        bool loading = false;
        void Clear() => marcas = new List<Marca>();
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Marca = new Marca();
        //}
        protected override async Task OnInitializedAsync()
        {
            this.Marca = new Marca();
            //await GetMarcas();
            marcas = await marcasService.Get(new MarcaGetParams());
        }
        async Task GetMarcasByParam()
        {
            loading = true;
            var getAzul = new MarcaGetParams { IdMarca = 1 };
            marcas = await marcasService.Get(getAzul);
            loading = false;
        }

        public async Task GetMarcas()
        {
            //loading = true;
            await BlockPage();
            marcas = await marcasService.Get(new MarcaGetParams());
            await UnBlockPage();
            //loading = false;
        }

        async Task SaveMarca()
        {
            await BlockPage();
            await marcasService.SaveMarca(this.Marca);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await JsInteropUtils.Reload(jsRuntime, true);
        }
        async Task CreateOrEdit(Marca m)
        {
            await BlockPage();
            if (m.IdMarca > 0)
            {
                var marca = await marcasService.Get(new MarcaGetParams { IdMarca = Convert.ToInt32(m.IdMarca)});
                this.Marca = marca.FirstOrDefault();
            }
            else
            {
                this.Marca = new Marca();
            }
            await UnBlockPage();
            //await JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        }
        async Task ShowModelos(Marca m )
        {
            var modelos = await ModelosService.Get(new ModeloGetParams { IdMarca =  (int)m.IdMarca });

            var parameters = new DialogParameters { ["Modelos"] = modelos };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<PrestamoBlazorApp.Shared.Components.Modelos.ModelosList>("Listado de Modelos", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
           
            }
        
        }
        private bool FilterFunc(Marca element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
