using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;

namespace PrestamoBlazorApp.Shared
{
    public partial class CreateOrEditCatalogo : BaseForCreateOrEdit
    {
      
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo();
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams();
        BaseForList BaseForList { get; set; }
        IEnumerable<Catalogo> catalogos { get; set; } = new List<Catalogo>();
     
        [Inject]
        CatalogosService CatalogosService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BlockPage();
                var lista = await CatalogosService.Get(CatalogoGetParams);
          
                catalogos = JsonConvert.DeserializeObject<IEnumerable< Catalogo>>(lista.FirstOrDefault().ToString() );
                await UnBlockPage();
                StateHasChanged();
            }

        }
        async Task SaveCatalogo()
        {
            await Handle_SaveData(async () => await CatalogosService.SaveCatalogo(this.Catalogo), null, null,false,"Reload");
            this.Catalogo = new Catalogo();
        }
        async Task CreateOrEdit(int Id = -1)
        {
            if (Id > 0)
            {
                await BlockPage();
                CatalogoGetParams.Id = Id;
                var lista = await CatalogosService.Get(CatalogoGetParams);
                var catalogo =  JsonConvert.DeserializeObject<IEnumerable<Catalogo>>(lista.FirstOrDefault().ToString()).FirstOrDefault();
                Catalogo.Nombre = catalogo.Nombre;
                Catalogo.Codigo = catalogo.Codigo;
                Catalogo.Id = catalogo.Id;
                await UnBlockPage();
            }
            else
            {
                this.Catalogo = new Catalogo { IdTabla = Catalogo.IdTabla, NombreTabla = Catalogo.NombreTabla };
            }
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {

        }
    }

}
