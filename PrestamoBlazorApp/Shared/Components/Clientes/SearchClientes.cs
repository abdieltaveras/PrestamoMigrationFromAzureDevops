using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
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

            eOpcionesSearchCliente opcion = (eOpcionesSearchCliente)SelectedSearchOption;
            Clientes = new List<Cliente>();
            switch (opcion)
            {
                case eOpcionesSearchCliente.TextoLibre:
                    Clientes = await ClientesService.SearchClientes(opcion.ToString(),TextToSearch, cargarImagenes);
                    break;
                case eOpcionesSearchCliente.NoIdentificacion:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams { NoIdentificacion = TextToSearch });
                    break;
                case eOpcionesSearchCliente.Nombre:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams {Nombres = TextToSearch });
                    break;
                case eOpcionesSearchCliente.Apellidos:
                    Clientes = await ClientesService.GetClientesAsync(new ClienteGetParams { Apellidos = TextToSearch });
                    break;
                case eOpcionesSearchCliente.Apodo:
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

           

      
    }
}
