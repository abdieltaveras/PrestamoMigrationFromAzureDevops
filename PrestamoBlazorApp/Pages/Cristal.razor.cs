using MudBlazor;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages
{
    public partial class Cristal
    {
        private MudBlazor.Color colorActual { get; set; }

        private MudBlazor.Color proximoColor { get; set; }
        private string textoColor { get; set; }
        protected override async Task OnInitializedAsync()
        {
            colorActual = MudBlazor.Color.Error;
            proximoColor = MudBlazor.Color.Info;
            textoColor = "Poner color azul";
            await NotifyMessageBySnackBar("Solo mostrandole a Cristal", Severity.Warning);
            await base.OnInitializedAsync();
        }

        private async Task CambiarColor()
        {
            
            colorActual = proximoColor;
            textoColor = "Poner color rojo";
            proximoColor= MudBlazor.Color.Error; 
        }
    }
}

