using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class Clientes
    {
        [Inject]
        ClientesService clientesService { get; set; }
        ClienteGetParams searchClientes { get; set; } = new ClienteGetParams();
        int totalClientes { get; set; }
        IEnumerable<Cliente> clientes;
        bool loading = false;

        async Task GetClientes()
        {
            loading = true;
            clientes = await clientesService.GetClientesAsync(this.searchClientes);
            totalClientes = clientes.Count();
            loading = false;
        }
    }
}
