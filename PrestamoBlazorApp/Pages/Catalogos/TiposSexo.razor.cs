
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class TiposSexo : CatalogosViewBase
    {
        override protected CatalogosService GetService => CatalogosFactory.TiposSexoService;
            //new CatalogosService(base.CommomInjectionsService.HttpClientFactory, base.CommomInjectionsService.Configuration, "api/TipoSexo");
            //new TiposCatalogosGenericService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/TipoSexo");

    }
}