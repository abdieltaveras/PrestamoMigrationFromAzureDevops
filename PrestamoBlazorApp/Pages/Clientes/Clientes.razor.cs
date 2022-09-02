using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Reports;
using PrestamoBlazorApp.Shared.Components.Forms;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class Clientes : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }

        

        [Inject]
        ClientesService clientesService { get; set; }
        ClienteGetParams searchClientes { get; set; } = new ClienteGetParams();
        int totalClientes { get; set; }
        IEnumerable<Cliente> clientes;
        List<SelectClass> lstItemsToSearch { get; set; } = new List<SelectClass>();
        //private string _SearchDataBase { get; set; }

        //private string SearchDataBase { get { return _SearchDataBase; } set { _SearchDataBase = value; searchClientesDatabase(value).GetAwaiter(); } }

        private Cliente SelectedItem1 = null;
        private bool FilterFunc1(Cliente element) => FilterFunc(element, SearchStringTable);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        private int SelectedOptionSearch { get; set; } = -1;
        private int MinSearchLength = 3;
        List<eOpcionesSearchCliente> lstOpcionesSearch { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetClientes);
            FillOptions();
            await base.OnInitializedAsync();
        }
        private async Task GetClientes()
        {
            clientes = new List<Cliente>();
            this.searchClientes.CantidadRegistrosASeleccionar = 50;
            clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
            totalClientes = clientes.Count();
            StateHasChanged();
        }
        private async Task searchClientesDatabase(string search)
        {
            LoadingTable = true;
            if (search.Length >= MinSearchLength)
            {
                clientes = new List<Cliente>();
                clientes = await clientesService.SearchClientes(SelectedOptionSearch,search, false);
                totalClientes = clientes.Count();
            }
            else
            {
                this.searchClientes.CantidadRegistrosASeleccionar = 50;
                clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
                totalClientes = clientes.Count();
            }
            //StateHasChanged();
            LoadingTable = false;
        }
        async void PrintFicha(int idcliente, int reportType)
        {
            await BlockPage();
            var result = await clientesService.ReportFicha(jsRuntime, idcliente, reportType);
            await UnBlockPage();
        }

        private async Task ShowReportGenerator()
        {
            var parameters = new DialogParameters();
            //parameters.Add("IdLocalidad", id);
            var dialog = DialogService.Show<SearchReportGeneric>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (result.Data != null)
            {
                //if (result.Data.ToString() == "1")
                //{
                    
                //    StateHasChanged();
                //}
            }
        }
        private async Task SelectedOptionToSearch(SelectClass selected)
        {
            SelectedOptionSearch = Convert.ToInt32(selected.Value);
            //var value = selected.Value.ToString();
            //var text = selected.Text.ToString();
        }
        private void FillOptions()
        {
            var a = Enum.GetValues(typeof(eOpcionesSearchCliente)).Cast<eOpcionesSearchCliente>().ToList();
            foreach (var item in a)
            {
                lstItemsToSearch.Add(new SelectClass { Value = Convert.ToInt32(item), Text = item.ToString() });

            }
        }
        private bool FilterFunc(Cliente element, string searchString)
        {

            // evaluar el item seleccionado
            // si o este texto libre la opcion seleccionada ejecuta el searchDatabase

            if (string.IsNullOrWhiteSpace(searchString) || (searchString.Length <= 3))
                return true;
            if (element.NombreCompleto.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Nombres.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Apellidos.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            if (element.TelefonoCasa != null)
            {
                if (element.TelefonoCasa.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            if (element.TelefonoMovil != null)
            {

                if (element.TelefonoMovil.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            if (element.NoIdentificacion != null)
            {
                if (element.NoIdentificacion.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
