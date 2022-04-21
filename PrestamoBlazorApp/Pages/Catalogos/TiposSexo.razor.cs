
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class TiposSexo : CatalogosViewBase
    {
        override protected CatalogosService GetService => new CatalogosService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/TipoSexo");
            //new TiposCatalogosGenericService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/TipoSexo");

    }
}