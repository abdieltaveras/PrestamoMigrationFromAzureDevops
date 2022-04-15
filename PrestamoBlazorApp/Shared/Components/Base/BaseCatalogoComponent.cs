using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public abstract class BaseCatalogoComponent : BaseForCreateOrEdit
    {
        protected abstract string CatalogoName();

        protected abstract Task<IEnumerable<CatalogoInsUpd>> GetCatalogos(BaseCatalogoGetParams getParam);
        protected abstract  Task ShowAddEditor(CatalogoInsUpd catalogo, Func<Task> action);

        protected abstract  Task ShowEditEditor(CatalogoInsUpd catalogo, Func<Task> action);

        protected abstract  Task ShowDeleteEditor(CatalogoInsUpd catalogo, Func<Task> action);
    }
}
