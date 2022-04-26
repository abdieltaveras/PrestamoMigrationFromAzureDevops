
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class ColoresV2 : CatalogosViewBase
    {
        override protected CatalogosService GetService => new CatalogosService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/ColorV2");
        //new TiposCatalogosGenericService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/ColorV2"); 
        //new ColoresServiceV2(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration);

        protected override async Task ShowEditor(CatalogoInsUpd catalogo, bool usarFormularioParaEliminar = false, Func<Task> action = null)
        {
            await Dialog.ShowMessageBox(
            "Warning",
            "Form edition to show Luis to change the editor we need!",
            yesText: "Delete!", cancelText: "Cancel");
        }
    }
}