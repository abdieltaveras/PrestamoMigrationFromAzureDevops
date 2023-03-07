using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Marcas
{
    public partial class EditMarcas : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Marca Marca { get; set; }
        [Inject]
        MarcasService MarcasService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };

        protected override async Task OnInitializedAsync()
        {
            //this.Marca = new Marca();
            //await GetMarcas();
            //marcas = await marcasService.Get(new MarcaGetParams());
        }

        async Task Save()
        {
            //await ModelosService.SaveModelo(Modelo);
            await form.Validate();
           
            if (form.Errors.Count() <= 0)
            {
                await Handle_SaveData(() => MarcasService.SaveMarca(Marca), mudDialogInstance:MudDialog);
            }
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
