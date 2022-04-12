using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public abstract class BaseCatalogoComponent : BaseForCreateOrEdit
    {
        protected abstract string CatalogoName();

        protected abstract Task<IEnumerable<CatalogoInsUpd>> GetCatalogos(BaseCatalogoGetParams getParam);
        protected abstract void ShowAddEditor(CatalogoInsUpd catalogo);

        protected abstract void ShowEditEditor(CatalogoInsUpd catalogo);

        protected abstract void ShowDeleteEditor(CatalogoInsUpd catalogo);
    }
}
