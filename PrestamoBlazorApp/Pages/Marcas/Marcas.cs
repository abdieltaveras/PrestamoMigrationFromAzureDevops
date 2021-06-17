using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
//using PrestamoBlazorApp.Pages.Base;
//using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Marcas
{
    
    public partial class Marcas : BaseForCreateOrEdit
    {
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo { NombreTabla = "tblMarcas", IdTabla = "idmarca" };
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblMarcas", IdTabla = "idmarca" };
        //MarcaGetParams SearchMarca { get; set; } = new MarcaGetParams();
        //[Inject]
        //IJSRuntime jsRuntime { get; set; }

        //JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        //[Inject]
        //MarcasService marcasService { get; set; }
        //public IEnumerable<Marca> marcas { get; set; }=new List<Marca>();
        //[Parameter]
        //public Marca Marca { get; set; } 
        //bool loading = false;
        //void Clear() => marcas = new List<Marca>();
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Marca = new Marca();
        //}
        //protected override async Task OnInitializedAsync()
        //{
        //    marcas = await marcasService.Get(new MarcaGetParams());
        //}
        //async Task GetMarcasByParam()
        //{
        //    loading = true;
        //    var getAzul = new MarcaGetParams { IdMarca = 1 };
        //    marcas = await marcasService.GetMarcasAsync(getAzul);
        //    loading = false;
        //}

        //public async Task GetMarcas()
        //{
        //    //loading = true;
        //    await BlockPage();
        //    marcas = await marcasService.Get(new MarcaGetParams());
        //    await UnBlockPage();
        //    //loading = false;
        //}

        //async Task SaveMarca()
        //{
        //    await BlockPage();
        //    await marcasService.SaveMarca(this.Marca);
        //    await UnBlockPage();
        //    await SweetMessageBox("Guardado Correctamente", "success", "");
        //    //await JsInteropUtils.Reload(jsRuntime, true);
        //}
        //async Task CreateOrEdit(int idMarca = -1)
        //{
        //    await BlockPage();
        //    if (idMarca>0)
        //    { 
        //        var marca = await marcasService.Get(new MarcaGetParams { IdMarca = idMarca });
        //        this.Marca = marca.FirstOrDefault();
        //    }
        //    else
        //    {
        //        this.Marca = new Marca();
        //    }
        //    await UnBlockPage();
        //    await JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        //}
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
