using Microsoft.AspNetCore.Components;
using PrestamoEntidades;

namespace PrestamoBlazorApp.Pages.Clientes.Components.ClientCard
{
    public partial class ClientCardInfo
    {
        [Parameter]
        public InfoClienteDrCr Cliente { get; set; } = new InfoClienteDrCr();
    }
}
