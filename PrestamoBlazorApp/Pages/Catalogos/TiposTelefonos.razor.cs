
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class TiposTelefonos : CatalogosViewBase
    {
        override protected CatalogosService GetService => CatalogosFactory.TiposTelefonoService;
    }
}