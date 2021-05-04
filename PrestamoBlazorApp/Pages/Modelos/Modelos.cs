using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using PrestamoBlazorApp.Pages.Clientes;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Modelos
{
    public partial class Modelos : BaseForCreateOrEdit
    {
        [Inject]
        DialogService dialogService { get; set; }
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        ModelosService modelosService { get; set; }
        ModeloGetParams SearchModelo { get; set; } = new ModeloGetParams();
        IEnumerable<Modelo> modelos { get; set; } = new List<Modelo>();
        IEnumerable<Marca> marcas { get; set; } = new List<Marca>();
        [Parameter]
        public Modelo Modelo { get; set; } 
        bool loading = false;
        void Clear() => modelos = new List<Modelo>();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Modelo = new Modelo();
            
        }
        protected override async Task OnInitializedAsync()
        {
            await BlockPage();

            marcas = await modelosService.GetMarcasForModelo();
            modelos = await modelosService.Get(new ModeloGetParams());
            await UnBlockPage();

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
            await SweetMessageBox("Guardado Correctamente", "success", "");


        }
        void CreateOrEdit(int IdModelo = -1)
        {
         
            if (IdModelo > 0)
            {
                this.Modelo = modelos.Where(m => m.IdModelo == IdModelo).FirstOrDefault();
            }
            else
            {
                this.Modelo = new Modelo();
               
            }
             JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        }

        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            var selectedValue = Convert.ToInt32(str);
            Console.WriteLine($"{name} value changed to {str}");
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
