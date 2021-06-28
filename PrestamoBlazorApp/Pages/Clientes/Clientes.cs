using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class Clientes : BaseForList
    {
        [Inject]
        ClientesService clientesService { get; set; }
        ClienteGetParams searchClientes { get; set; } = new ClienteGetParams();
        int totalClientes { get; set; }
        IEnumerable<Cliente> clientes;
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetClientes);
            await base.OnInitializedAsync();
        }
        private async Task GetClientes()
        {
            clientes = new List<Cliente>();
            this.searchClientes.CantidadRegistrosASeleccionar = 50;
            clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
            totalClientes = clientes.Count();
        }

    }
}
