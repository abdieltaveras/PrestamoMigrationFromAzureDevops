using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Pages.TasasInteres;
namespace PrestamoBlazorApp.Pages.CodigosCargosDebitosReservados
{
    public partial class CodigosCargosDebitosReservados : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        CodigosCargosDebitosReservadosService _codigosCargosDebitosReservadosService { get; set; }
        IEnumerable<PrestamoEntidades.CodigosCargosDebitos> _codigosCargosDebitosReservados { get; set; }
        [Parameter]
        public PrestamoEntidades.CodigosCargosDebitos _codigosCargosDebitosReservadosModel { get; set; } = new PrestamoEntidades.CodigosCargosDebitos();
        private bool ChkRequiereAutorizacion { get; set; }
        private bool ChkEstatus { get; set; } = true;



        private PrestamoEntidades.CodigosCargosDebitos SelectedItem1 = null;
        private bool FilterFunc1(PrestamoEntidades.CodigosCargosDebitos element) => FilterFunc(element, SearchStringTable);

        private bool ShowDialogCreate { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await GetDataList();
        }
        private async Task searchEstatusDatabase(string search)
        {
            LoadingTable = true;
            if (search.Length >= MinSearchLength)
            {
                _codigosCargosDebitosReservados = new List<PrestamoEntidades.CodigosCargosDebitos>();
  
            }
            //StateHasChanged();
            LoadingTable = false;
        }



        async Task GetDataList()
        {
            _codigosCargosDebitosReservados = await _codigosCargosDebitosReservadosService.Get(new CodigosCargosGetParams { IdCodigoCargo = -1 });
            StateHasChanged();
        }
        private bool FilterFunc(PrestamoEntidades.CodigosCargosDebitos element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            //if (element.Codigo != null)
            //{
            //    if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            //        return true;
            //}
            return false;
        }
        async Task CreateOrEdit(int id = -1)
        {
            //await BlockPage();
            var parameters = new DialogParameters();
            parameters.Add("IdCodigoCargo", id);
            dialogOptions.MaxWidth = MaxWidth.Medium;
            dialogOptions.DisableBackdropClick = true;
            dialogOptions.CloseButton = true;
            var dialog = DialogService.Show<Shared.Components.CodigosCargos.CreateOrEditCodigosCargos>("Crear Codigo Cargo", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetDataList();
                //await GetLocalidades();
            }
            //var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = idLocalidad });
            //Localidad = localidad.FirstOrDefault();
            //await UnBlockPage();
        }
    }
}
