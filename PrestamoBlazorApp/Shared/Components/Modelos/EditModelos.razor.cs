using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Modelos
{
    public partial class EditModelos : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Modelo Modelo { get; set; }
        [Parameter]
        public Marca Marca { get; set; }
        [Inject]
        ModelosService ModelosService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };

        protected override async Task OnInitializedAsync()
        {
            //this.Marca = new Marca();
            //await GetMarcas();
            //marcas = await marcasService.Get(new MarcaGetParams());
        }

        async Task SaveModelo()
        {
            //await ModelosService.SaveModelo(Modelo); 
            this.form = null;
            Modelo.IdMarca = (int)Marca.IdMarca;
            await Handle_SaveData(() => ModelosService.SaveModelo(Modelo), mudDialogInstance:MudDialog);
            //await BlockPage();
            //await marcasService.SaveMarca(this.Marca);
            //await UnBlockPage();
            //await SweetMessageBox("Guardado Correctamente", "success", "");
            //await JsInteropUtils.Reload(jsRuntime, true);
        }
        async Task CloseDialog ()
        {
            MudDialog.Close();

        }
    }
}
