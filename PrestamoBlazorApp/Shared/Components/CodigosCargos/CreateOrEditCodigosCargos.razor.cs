using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Shared.Components.CodigosCargos
{
    public partial class CreateOrEditCodigosCargos : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        CodigosCargosDebitosService _CodigosCargosDebitosReservadosService  { get; set; }
        [Parameter]
        public int IdCodigoCargo { get; set; }

        PrestamoEntidades.CodigosCargosDebitos CodigoCargoModel { get; set; } = new PrestamoEntidades.CodigosCargosDebitos();
        protected override async Task OnInitializedAsync()
        {
            //EntidadEstatus = new PrestamoEntidades.EntidadEstatus();
            await CreateOrEdit();
            //await base.OnInitializedAsync();
        }

        async Task CreateOrEdit()
        {
            //await BlockPage();
            if (IdCodigoCargo > 0)
            {
                var data = await _CodigosCargosDebitosReservadosService.Get(new CodigosCargosGetParams { IdCodigoCargo = IdCodigoCargo});
                CodigoCargoModel = data.FirstOrDefault();
            }
            else
            {
                this.CodigoCargoModel = new PrestamoEntidades.CodigosCargosDebitos();
            }
            StateHasChanged();
            //await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task Save()
        {

            await BlockPage();
            await Handle_SaveData(async () => await _CodigosCargosDebitosReservadosService.Save(this.CodigoCargoModel), null, null, false, mudDialogInstance: MudDialog);
            await UnBlockPage();
         

            //await Handle_Funct(async () => await EntidadEstatusService.Save(this.EntidadEstatus));

            //await SweetAlertSuccess("Datos Guardados Correctamente");
            await UnBlockPage();
        }

    }
}
