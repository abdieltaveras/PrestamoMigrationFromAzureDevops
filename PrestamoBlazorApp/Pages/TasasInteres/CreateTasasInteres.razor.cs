using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.TasasInteres
{
    public partial class CreateTasasInteres : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        TasasInteresService TasasInteresService { get; set; }
        IEnumerable<TasaInteres> tasasinteres { get; set; } = new List<TasaInteres>();
        [Parameter]
        public TasaInteres TasaInteres { get; set; } = new TasaInteres();
        [Parameter]
        public int IdTasaInteres { get; set; } = -1;
        private bool ChkRequiereAutorizacion { get; set; }
        private bool ChkEstatus { get; set; } = true;

        private decimal _Tasa { get; set; }
        public decimal Tasa { get { return _Tasa; } set { this.TasaInteres.Nombre = $"{Convert.ToDecimal(value)}% de interes"; _Tasa = Convert.ToDecimal(value); } }


        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };

        protected override async Task OnInitializedAsync()
        {
            await GetData();
            await CreateOrEdit();
        }
        async Task CreateOrEdit()
        {
            await BlockPage();
            if (IdTasaInteres > 0)
            {
                var tasainteres = await TasasInteresService.Get(new TasaInteresGetParams { idTasaInteres = IdTasaInteres });
                this.TasaInteres = tasainteres.FirstOrDefault();
                this.ChkRequiereAutorizacion = this.TasaInteres.RequiereAutorizacion;
                this.ChkEstatus = this.TasaInteres.Activo;
                this.Tasa = this.TasaInteres.InteresMensual;
                //StateHasChanged();
            }
            else
            {
                this.TasaInteres = new TasaInteres();
            }
            await UnBlockPage();
            //ShowDialog(true);
            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task SaveTasaInteres()
        {
            await BlockPage();
            //StateHasChanged();
            this.TasaInteres.Activo = ChkEstatus;
            this.TasaInteres.RequiereAutorizacion = ChkRequiereAutorizacion;
            this.TasaInteres.InteresMensual = this.Tasa;
            await TasasInteresService.SaveTasaInteres(this.TasaInteres);
            await NotifyMessageBySnackBar("Guardado Correctamente", MudBlazor.Severity.Success);
            await CloseModal(1);
            await GetData();
            await UnBlockPage();
        }


        async Task GetData()
        {
            tasasinteres = await TasasInteresService.Get(new TasaInteresGetParams());
        }
        private async Task CloseModal(int result = -1)
        {
            MudDialog.Close(DialogResult.Ok(result));
        }
    }
}
