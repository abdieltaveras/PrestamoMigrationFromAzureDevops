using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoBlazorApp.Shared.Components.Reports;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class Clientes : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }
        ClienteGetParams searchClientes { get; set; } = new ClienteGetParams();
        IEnumerable<Cliente> clientes;
        List<SelectClass> lstItemsToSearch { get; set; } = new List<SelectClass>();
        int totalClientes { get; set; }
        private Cliente SelectedItem1 = null;
        private bool FilterFunc1(Cliente element) => FilterFunc(element, SearchStringTable);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        private eOpcionesSearchCliente SelectedOptionSearch { get; set; } = eOpcionesSearchCliente.TextoLibre;
        List<eOpcionesSearchCliente> lstOpcionesSearch { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MinSearchLength = 3;
            await Handle_GetDataForList(GetClientes);
            FillOptions();
            await base.OnInitializedAsync();
        }

        private async Task GetClientes()
        {
            clientes = await clientesService.GetClientesAsync(new ClienteGetParams());
            totalClientes = clientes.Count();
        }

        private async Task<ClienteGetParams> SearchFor(int SelectedProperty, string searchText)
        {
            bool isDefined = Enum.IsDefined(typeof(eOpcionesSearchCliente), SelectedProperty);
            ClienteGetParams param = new ClienteGetParams();
            if (isDefined)
            {
                eOpcionesSearchCliente enumOp = (eOpcionesSearchCliente)SelectedProperty;
                switch (enumOp)
                {
                    case eOpcionesSearchCliente.NoIdentificacion:
                        param.NoIdentificacion = searchText;
                        break;
                    case eOpcionesSearchCliente.Nombres:
                        param.Nombres = searchText;
                        break;
                    case eOpcionesSearchCliente.Apellidos:
                        param.Apellidos = searchText;
                        break;
                    case eOpcionesSearchCliente.NombreCompleto:
                        param.NombreCompleto = searchText;
                        break;
                    default:
                        break;
                }
            }
            return param;
        }

        private async Task searchClientesDatabase(string search)
        {
            LoadingTable = true;
            if (search.Length >= MinSearchLength)
            {
                ClienteGetParams param = new ClienteGetParams();
                param = await SearchFor(Convert.ToInt32(SelectedOptionSearch), search);
                clientes = await clientesService.SearchClienteByProperties(param);
            }
            else
            {
                this.searchClientes.CantidadRegistrosASeleccionar = 50;
                clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
            }
            totalClientes = clientes.Count();
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
            var dialog = DialogService.Show<SearchReportGeneric>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (result.Data != null)
            {
            }
        }

        private async Task SelectedOptionToSearch(SelectClass selected)
        {
            SelectedOptionSearch = (eOpcionesSearchCliente)selected.Value;
        }

        private void FillOptions()
        {
            var a = Enum.GetValues(typeof(eOpcionesSearchCliente)).Cast<eOpcionesSearchCliente>().ToList();
            foreach (var item in a)
            {
                lstItemsToSearch.Add(new SelectClass { Value = item, Text = item.ToString() });

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
