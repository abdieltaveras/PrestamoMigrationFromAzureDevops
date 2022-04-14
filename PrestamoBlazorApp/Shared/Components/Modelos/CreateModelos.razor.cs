using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Pages.Clientes;
using PrestamoBlazorApp.Shared;
using MudBlazor;

namespace PrestamoBlazorApp.Shared.Components.Modelos
{
    public partial class CreateModelos : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        ModelosService MODELOSSERVICE { get; set; }
        [Parameter]
        public Modelo MODELO { get; set; }
        IEnumerable<Modelo> MODELOS { get; set; } = new List<Modelo>();
        IEnumerable<Marca> MARCAS { get; set; } = new List<Marca>();
        [Parameter]
        public int IDMODELO { get; set; }

       
        protected override async Task OnInitializedAsync()
        {
            MODELO = new Modelo();
            //await BlockPage();
            MARCAS = await MODELOSSERVICE.GetMarcasForModelo();
            MODELOS = await MODELOSSERVICE.Get(new ModeloGetParams());
            await CreateOrEdit();
            //await UnBlockPage();

        }
        async Task CreateOrEdit()
        {
            //await BlockPage();
            if (IDMODELO > 0)
            {
                var mod = await MODELOSSERVICE.Get(new ModeloGetParams { IdModelo = IDMODELO });
                this.MODELO = mod.FirstOrDefault();
            }
            else
            {
                this.MODELO = new Modelo();

            }
            StateHasChanged();
            //await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#edtMarca");
        }
        async Task SaveModelo()
        {
            //await BlockPage();
            await MODELOSSERVICE.SaveModelo(this.MODELO);
            //await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "/modelos");
        }
        private async Task CloseModal(int result = -1)
        {
            MudDialog.Close(DialogResult.Ok(result));
        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
