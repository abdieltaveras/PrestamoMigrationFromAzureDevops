using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared.Components.TasasInteres;
using PrestamoBlazorApp.Domain;

namespace PrestamoBlazorApp.Pages.TasasInteres
{
    public partial class TasasInteres : BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        TasasInteresService TasasInteresService { get; set; }
        IEnumerable<TasaInteres> tasasinteres { get; set; } = new List<TasaInteres>();
        [Parameter]
        public TasaInteres TasaInteres { get; set; } = new TasaInteres();
        private bool ChkRequiereAutorizacion { get; set; }
        private bool ChkEstatus { get; set; } = true;
    
        private decimal _Tasa { get; set; }
        public decimal Tasa { get { return _Tasa; } set { this.TasaInteres.Nombre = $"{Convert.ToDecimal(value)}% de interes"; _Tasa = Convert.ToDecimal(value);  } }

        private string SearchString1 = "";
        private TasaInteres SelectedItem1 = null;
        private bool FilterFunc1(TasaInteres element) => FilterFunc(element, SearchString1);

        private bool ShowDialogCreate { get; set; } = false;
     
        private bool Dense = true, Hover = true, Bordered = false, Striped = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.TasaInteres = new TasaInteres();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }
        async Task CreateOrEdit(int idTasaInteres = -1)
        {
            await BlockPage();
            if (idTasaInteres > 0)
            {
                var tasainteres = await TasasInteresService.Get(new TasaInteresGetParams { idTasaInteres = idTasaInteres });
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
            ShowDialog(idTasaInteres);
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
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await JsInteropUtils.CloseModal(jsRuntime, "#MyModal");
            ShowDialog();
            await GetData();
            await UnBlockPage();
        }


        async Task GetData()
        {
            tasasinteres = await TasasInteresService.Get(new TasaInteresGetParams());
        }
        private bool FilterFunc(TasaInteres element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        private async Task ShowDialog(int id = -1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdTasaInteres", id);
            var dialog = DialogService.Show<CreateTasasInteres>("", parameters, Showdialogs.BasicOptions);
            var result = await dialog.Result;
            if (Convert.ToInt32(result.Data.ToString()) == 1)
            {
                await GetData();
                StateHasChanged();
            }
        }
    }
}
