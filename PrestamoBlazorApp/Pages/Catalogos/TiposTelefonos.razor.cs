
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class TiposTelefonos : CatalogosViewBase
    {
        override protected CatalogosService GetService => new CatalogosService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/TiposTelefonos");
    }
}