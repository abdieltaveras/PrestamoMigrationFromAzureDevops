using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Pages.Clientes;
using PrestamoBlazorApp.Shared;
using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Modelos;
namespace PrestamoBlazorApp.Pages.Modelos
{
    public partial class Modelos : BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        ModelosService modelosService { get; set; }
        ModeloGetParams SearchModelo { get; set; } = new ModeloGetParams();
        IEnumerable<Modelo> modelos { get; set; } = new List<Modelo>();
        IEnumerable<Marca> marcas { get; set; } = new List<Marca>();
        [Parameter]
        public Modelo Modelo { get; set; } 
        
        void Clear() => modelos = new List<Modelo>();
        private bool Dense = true, Hover = true, Bordered = false, Striped = false;
        private string SearchString1 = "";
        private Modelo SelectedItem1 = null;
        private bool FilterFunc1(Modelo element) => FilterFunc(element, SearchString1);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Modelo = new Modelo();
            
        }
        protected override async Task OnInitializedAsync()
        {
            //await BlockPage();
            marcas = await modelosService.GetMarcasForModelo();
            modelos = await modelosService.Get(new ModeloGetParams());
            //await UnBlockPage();

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
            //loading = true;
            await BlockPage();
            var result = new ModeloGetParams { Nombre = SearchModelo.Nombre };
            modelos = await modelosService.Get(result);
            await UnBlockPage();
            //loading = false;
        }
        async Task GetMarcas()
        {
            //loading = true;
            await BlockPage();
            marcas = await modelosService.GetMarcasForModelo();
            //loading = false;
            await UnBlockPage();

        }
        async Task SaveModelo()
        {
            await BlockPage();
            await modelosService.SaveModelo(this.Modelo);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "/modelos");
        }
        async Task CreateOrEdit(int IdModelo = -1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IDMODELO", IdModelo);
            var dialog = DialogService.Show<CreateModelos>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (result.Data != null)
            {
                if (Convert.ToInt32(result.Data.ToString()) == 1)
                {
                    await GetModelos();
                    StateHasChanged();
                }
            }

            //await BlockPage();
            //if (IdModelo > 0)
            //{
            //    var mod = await modelosService.Get(new ModeloGetParams { IdModelo = IdModelo });
            //    this.Modelo = mod.FirstOrDefault();
            //}
            //else
            //{
            //    this.Modelo = new Modelo();

            //}
            //await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        }

        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            var selectedValue = Convert.ToInt32(str);
            Console.WriteLine($"{name} value changed to {str}");
        }
        private bool FilterFunc(Modelo element, string searchString)
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
        //private async Task CreateOrEdit(int id = -1)
        //{
        //    var parameters = new DialogParameters();
        //    parameters.Add("IdTasaInteres", id);
        //    var dialog = DialogService.Show<CreateTasasInteres>("", parameters, dialogOptions);
        //    var result = await dialog.Result;
        //    if (Convert.ToInt32(result.Data.ToString()) == 1)
        //    {
        //        await GetData();
        //        StateHasChanged();
        //    }
        //}
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
