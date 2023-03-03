using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Modelos
{
    public partial class ModelosList
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Parameter]
        public List<Modelo> Modelos { get; set; } = new List<Modelo>();
        private string SearchString1 = "";
        private Modelo SelectedItem1 = null;
        private bool FilterFunc1(Modelo element) => FilterFunc(element, SearchString1);
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

        async Task CreateOrEdit(Modelo modelo = null)
        {

            //var modelos = await ModelosService.Get(new ModeloGetParams { IdMarca = idMarca });
            if(modelo == null)
            {
                modelo = new Modelo();
            }
            var parameters = new DialogParameters { ["Modelo"] = modelo };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<PrestamoBlazorApp.Shared.Components.Modelos.EditModelos>("Editor de Modelos", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            //await BlockPage();
            //if (idMarca > 0)
            //{
            //    var marca = await marcasService.Get(new MarcaGetParams { IdMarca = idMarca });
            //    this.Marca = marca.FirstOrDefault();
            //}
            //else
            //{
            //    this.Marca = new Marca();
            //}
            //await UnBlockPage();
            //await JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        }
    }
}
