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
        GarantiaConMarcaYModelo _Garantia { get; set; } = new GarantiaConMarcaYModelo();


        private async Task DialogSearchPrestamo()
        {
            string[] cols = { "Nombres", "Apellidos" };
            var parameters = new DialogParameters { ["Columns"] = cols };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchPrestamoByProperty>("Seleccionar Prestamo", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _Prestamo = (PrestamoClienteUI)result.Data;
                await GetGarantia(_Prestamo.IdPrestamo);
            }
        }
        private async Task GetGarantia(int IdPrestamo)
        {
            var gar = await _GarantiasService.GetGarantias(new GarantiaGetParams { IdGarantia = _Prestamo.IdGarantia});
            if (gar.Count() > 0)
            {
                _Garantia = gar.FirstOrDefault();
            }
        }
    }
}
