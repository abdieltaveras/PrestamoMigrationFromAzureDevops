using PrestamoBLL.Entidades;
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
        bool loading = false;

        protected override async Task OnInitializedAsync()
        {
            Handle_GetData(GetClientes);
            //await GetClientes();
            await base.OnInitializedAsync();
        }
        private async Task GetClientes()
        {
            clientes = new List<Cliente>();
            this.searchClientes.CantidadRegistrosASeleccionar = 50;
            this.searchClientes.ConvertJsonToObj = false ;
            clientes = await clientesService.GetClientesAsync(this.searchClientes);
            totalClientes = clientes.Count();
        }

    }
}
