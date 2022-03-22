using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
using MudBlazor;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{
    
    public partial class CreateOrEditCatalogo : BaseForCreateOrEdit
    {
        private bool ShowDialogCreate { get; set; } = false;
        // parameters
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo();
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams();
        
        // injections
        [Inject]
        CatalogosService CatalogosService { get; set; }
        // Members
        private string SearchString1 = "";
        private Catalogo SelectedItem1 = null;
        private bool FilterFunc1(Catalogo element) => FilterFunc(element, SearchString1);
        private BaseForList BaseForList { get; set; }
        private IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();
        private MudForm Form;
        private bool Success, Dense=true, Hover=true, Bordered, Striped;
        private string[] Errors = { };

        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Large, FullWidth = true };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BlockPage();
                Catalogos = await CatalogosService.Get(CatalogoGetParams);
                //JsonConvert.DeserializeObject<IEnumerable< Catalogo>>(lista.FirstOrDefault().ToString() );
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
                var catalogo = lista.ToList().FirstOrDefault();
                //JsonConvert.DeserializeObject<IEnumerable<Catalogo>>(lista.FirstOrDefault().ToString()).FirstOrDefault();
                Catalogo.Nombre = catalogo.Nombre;
                Catalogo.Codigo = catalogo.Codigo;
                Catalogo.Id = catalogo.Id;
                await UnBlockPage();
            }
            else
            {
                this.Catalogo = new Catalogo { IdTabla = Catalogo.IdTabla, NombreTabla = Catalogo.NombreTabla };
            }
            ShowDialogCreate = true;
            StateHasChanged();
        }
        private async Task ShowCreateModal(bool value)
        {
            ShowDialogCreate = value;
        }
        async Task Eliminar ()
        {
            //*********Sweet confirm retorna int, esto es debido a que utilizamos 3 botones si enviamos el parametro DenyButtonText************//
            //desde aqui SweetConfirm
            //var a = await JsInteropUtils.SweetConfirm(jsRuntime,"Deseas Eliminar?","No quiero no");
            //if (a == 1)
            //{
            //    await SweetMessageBox("Eliminado");
            //}
            //else
            //{
            //    if(a == 2)
            //    {
            //        await SweetMessageBox("No quiere eliminar");
            //    }
            //}
            //hasta aqui esta el SweetConfirm 

            //*********************************************************//

            //var  a = await JsInteropUtils.SweetConfirmWithIcon(jsRuntime, "Desea Eliminar?", ""); //Funciona
            var a = await OnDeleteConfirm("Desea Eliminar?", " (OnDeleteConfirm)"); //Funciona
            if (a == true)
            {
                await SweetMessageBox("Eliminado");
            }

        }
        void RaiseInvalidSubmit()
        {

        }
        async void PrintListado(int reportType)
        {
            await BlockPage();
            CatalogoGetParams.reportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, CatalogoGetParams);
            await UnBlockPage();
        }
        private bool FilterFunc(Catalogo element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo!=null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

    }

}
