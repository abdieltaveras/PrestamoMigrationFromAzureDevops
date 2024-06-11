using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class TiposCargosDebito : CatalogosViewBase
    {

        
        override protected CatalogosService GetService => new CatalogosService(base.CommomInjectionsService.HttpClientFactory, base.CommomInjectionsService.Configuration, base.CommomInjectionsService.LocalStorageService, "api/TiposCargosDebito");
        
        //protected override async Task ShowEditor(CatalogoInsUpd catalogo, bool usarFormularioParaEliminar = false, Func<Task> action = null)
        //{
        //    await Dialog.ShowMessageBox(
        //    "Warning",
        //    "Form edition to show Luis to change the editor we need!",
        //    yesText: "Delete!", cancelText: "Cancel");
        //}
    }
}