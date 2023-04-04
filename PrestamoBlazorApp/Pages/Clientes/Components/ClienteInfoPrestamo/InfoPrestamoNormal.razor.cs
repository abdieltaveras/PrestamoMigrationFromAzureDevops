using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Prestamos;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes.Components.ClienteInfoPrestamo
{
    //  esto es un componente, pero lo puse en una pagina para desarrollarlo sin tener que perder tiempo invocando
    public partial class InfoPrestamoNormal
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private string SearchText { get; set; }
        [Inject]
        GarantiasService _GarantiasService { get; set; }

        PrestamoClienteUI _Prestamo { get; set; } = new PrestamoClienteUI();
        IEnumerable<GarantiaConMarcaYModelo> _Garantias { get; set; } = new List<GarantiaConMarcaYModelo>();


        private async Task DialogSearchPrestamo()
        {
            string[] cols = { "Nombres", "Apellidos" };
            var parameters = new DialogParameters { ["Columns"] = cols };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchPrestamoByProperty>("Seleccionar Prestamo", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _Prestamo = (PrestamoClienteUI)result.Data;
                await GetGarantias(_Prestamo.IdPrestamo);
            }
        }
        private async Task GetGarantias(int IdPrestamo)
        {
            var gar = await _GarantiasService.GetGarantiasByPrestamo(IdPrestamo);
             _Garantias = gar;
        }
    }
}
