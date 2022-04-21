
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class ColoresV2 : CatalogosViewBase
    {
        override protected CatalogosService GetService => new CatalogosService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/ColorV2");
        //new TiposCatalogosGenericService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/ColorV2"); 
        //new ColoresServiceV2(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration);
    }
}