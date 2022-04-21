
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class Ocupaciones : CatalogosViewBase
    {
        override protected CatalogosService GetService=> new CatalogosService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/Ocupacion");
          //new TiposCatalogosGenericService(base.CommomInjectionsService.ClientFactory, base.CommomInjectionsService.Configuration, "api/Ocupacion");//new OcupacionesServiceV2(CommomInjectionsService.ClientFactory, CommomInjectionsService.Configuration);
    }
}