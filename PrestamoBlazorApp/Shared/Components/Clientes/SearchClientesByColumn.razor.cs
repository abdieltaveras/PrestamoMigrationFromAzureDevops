using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Clientes
{
    public partial class SearchClientesByColumn:BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string[] Columns { get; set; } = { };
        private string SearchText { get; set; }
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }

        [Inject]
        ClientesService ClientesService { get; set; }
        IEnumerable<Cliente> clientes { get; set; } = new List<Cliente>();
        [Parameter]
        public Cliente Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<Cliente> ValueChanged { get; set; }

        private Cliente _value;

        private async Task GetClientes()
        {
            clientes = await ClientesService.SearchClientesByColunm(SearchText, SelectedColumn, OrderBy);
        }
        private async Task onSearchClick()
        {
            await GetClientes();
        }
        private async Task SelectClient(Cliente cliente)
        {
            Value = cliente;
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(cliente));
            }
        }
    }
}
