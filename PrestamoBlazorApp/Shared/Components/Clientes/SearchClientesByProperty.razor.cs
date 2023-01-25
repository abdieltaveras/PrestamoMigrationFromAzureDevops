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
    public partial class SearchClientesByProperty:BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string[] Columns { get; set; } = { };
        private string SearchText { get; set; }
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }
        private int SelectedPropertySearch { get; set; } = 1;

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
            ClienteGetParams param = new ClienteGetParams();
            param = await SearchFor(SelectedPropertySearch, SearchText);
            clientes = await ClientesService.SearchClienteByProperties(param);
        }
        private async Task onSearchClick()
        {
            await GetClientes();
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
                    default:
                        break;
                }
            }
            return param;
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
