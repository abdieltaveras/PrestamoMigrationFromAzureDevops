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
namespace PrestamoBlazorApp.Pages.EntidadEstatus
{
    public partial class EntidadEstatus : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        EstatusService EntidadEstatusService { get; set; }
        IEnumerable<PrestamoEntidades.Estatus> entidadesEstatus { get; set; }
        [Parameter]
        public PrestamoEntidades.Estatus eEntidadEstatus { get; set; } = new PrestamoEntidades.Estatus();
        private bool ChkRequiereAutorizacion { get; set; }
        private bool ChkEstatus { get; set; } = true;



        private PrestamoEntidades.Estatus SelectedItem1 = null;
        private bool FilterFunc1(PrestamoEntidades.Estatus element) => FilterFunc(element, SearchStringTable);

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
                entidadesEstatus = new List<PrestamoEntidades.Estatus>();
  
            }
            //StateHasChanged();
            LoadingTable = false;
        }



        async Task GetDataList()
        {
            entidadesEstatus = await EntidadEstatusService.Get(new EstatusGetParams());
            StateHasChanged();
        }
        private bool FilterFunc(PrestamoEntidades.Estatus element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
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
            parameters.Add("IdEntidadEstatus", id);
            dialogOptions.MaxWidth = MaxWidth.Medium;
            dialogOptions.DisableBackdropClick = true;
            dialogOptions.CloseButton = true;
            var dialog = DialogService.Show<Shared.Components.EntidadEstatus.CreateOrEditEEstatus>("Crear Entidad Estatus", parameters, dialogOptions);
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
