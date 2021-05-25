using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Clientes
{
    public partial class SearchClientes: BaseForSearch
    {
        [Inject]
        ClientesService ClientesService { get; set; }
        [Parameter]
        public bool showUI { get; set; } = false;
        [Parameter]
        public EventCallback<int> OnClienteSelected { get; set; }
        [Parameter]
        public EventCallback<int> OnModalClose { get; set; }

        IEnumerable<Cliente> Clientes { get; set; } = new List<Cliente>();

        bool cargarImagenes = false;

        int SelectedSearchOption { get; set; } = 1;

        string TextToSearch { get; set; } = string.Empty;
        private async Task OnTextSearchChange(ChangeEventArgs args)
        {
            TextToSearch = args.Value.ToString();
        }
        private async Task SearchCliente()
        {

            if (TextToSearch.Length <= 2)
            {
                await NotifyMessageBox("Debe digitar minimo 2 digitos (letras y/o numeros) para realizar la busqueda");
                return;
            }

            OpcionesSearchCliente opcion = (OpcionesSearchCliente)SelectedSearchOption;
            Clientes = new List<Cliente>();
            switch (opcion)
            {
                case OpcionesSearchCliente.TextoLibre:
                    Clientes = await ClientesService.SearchClientes(TextToSearch, cargarImagenes);
                    break;
                case OpcionesSearchCliente.NoIdentificacion:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams { NoIdentificacion = TextToSearch });
                    break;
                case OpcionesSearchCliente.Nombre:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams {Nombres = TextToSearch });
                    break;
                case OpcionesSearchCliente.Apellidos:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams { Apellidos = TextToSearch });
                    break;
                case OpcionesSearchCliente.Apodo:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams { Apodo = TextToSearch });
                    break;
                default:
                    break;
            }

        }

        private void OnSelectedOptionChange(ChangeEventArgs args)
        {
            SelectedSearchOption = Convert.ToInt32(args.Value);
        }

        private async Task SelectCliente(int idCliente)
        {
            await OnClienteSelected.InvokeAsync(idCliente);
        }

        private void onShowUiChange()
        {
            showUI = !showUI;
            OnModalClose.InvokeAsync();
        }

           

        enum OpcionesSearchCliente { TextoLibre = 1, NoIdentificacion, Nombre, Apellidos, Apodo }
    }
}
