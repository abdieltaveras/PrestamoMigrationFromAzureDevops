using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos.Components.Estatus
{
    public partial class PrestamoDialogEstatus
    {
        [Parameter]
        public IEnumerable<PrestamoEntidades.PrestamoEstatusGet> estatusesPrestamo { get; set; } = new List<PrestamoEntidades.PrestamoEstatusGet>();
 
    }
}
