
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Pages.Catalogos
{
    public partial class Marcas : CatalogosViewBase
    {
        override protected CatalogosService GetService => CatalogosFactory.Marcas;

    }
}