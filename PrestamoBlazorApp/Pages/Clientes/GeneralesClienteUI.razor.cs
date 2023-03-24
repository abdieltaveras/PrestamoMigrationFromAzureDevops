using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class GeneralesClienteUi :CommonBase
    {
        [Parameter]
        public Cliente cliente { get; set; }
        [Parameter]
        public IEnumerable<BaseInsUpdGenericCatalogo> Ocupaciones { get; set; } = new List<Ocupacion>();
        [Parameter]
        public bool AllowInputCodigo { get; set; } = false;

        [Parameter]
        public IEnumerable<Imagen> FotosRostroCliente { get; set; }
        [Parameter]
        public IEnumerable<Imagen> FotosDocIdentificacion { get; set; }
        [Parameter]
        public EventCallback<Imagen> SetImages { get; set; }

        [Parameter]
        public EventCallback<bool> OnTieneConyugeChange { get; set; }

        [Parameter]
        public EventCallback<int> OnEstadoCivilChange { get; set; }
        [Parameter]
        public EventCallback<Imagen> RemoveImages { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                StateHasChanged();
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private void Handle_EstadoCivilChange(int value)
        {
            OnEstadoCivilChange.InvokeAsync(cliente.IdEstadoCivil);
        }
    }

}

