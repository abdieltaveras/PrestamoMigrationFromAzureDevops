using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public abstract class BaseGarantiaComponent : BaseForCreateOrEdit
    {
        protected abstract Task<IEnumerable<Garantia>> GetGarantias(GarantiaGetParams getParam);
        protected abstract Task ShowAddEditor(Garantia catalogo, Func<Task> action);

        protected abstract Task ShowEditEditor(Garantia catalogo, Func<Task> action);

        protected abstract Task ShowDeleteEditor(Garantia catalogo, Func<Task> action);
    }
}
