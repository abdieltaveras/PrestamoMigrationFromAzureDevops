using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Pages.Prestamos;
using PrestamoBlazorApp.Pages.Prestamos.Components;
using PrestamoBlazorApp.Pages.Prestamos.Components.Estatus;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Prestamos;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes.Components.ClienteInfoPrestamo
{
    //  esto es un componente, pero lo puse en una pagina para desarrollarlo sin tener que perder tiempo invocando
    public partial class InfoPrestamoNormal : BaseForList
    {
        string SearchInputValue { get; set; }
        [Inject]
        private PrestamosService PrestamosService { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        private string SearchText { get; set; }
        [Inject]
        GarantiasService _GarantiasService { get; set; }

        PrestamoClienteUI _Prestamo { get; set; } = new PrestamoClienteUI();
        IEnumerable<GarantiaConMarcaYModelo> _Garantias { get; set; } = new List<GarantiaConMarcaYModelo>();
        private IEnumerable<PrestamoEntidades.PrestamoEstatusGet> estatusesPrestamo { get; set; } = new List<PrestamoEntidades.PrestamoEstatusGet>();
        [Inject]
        private PrestamosEstatusService PrestamosEstatusService { get; set; }
        protected override async Task OnInitializedAsync()
        {
           
        }
        private async Task DialogPrestamosList(int id)
        {
            var prestamosCliente = await PrestamosService.GetPrestamoClienteUI(new PrestamoClienteUIGetParam { IdCliente = _Prestamo.IdCliente });
            if (prestamosCliente.Count() > 0)
            {
                await NotifyMessageBySnackBar($"Este Cliente Tiene Mas Prestamos", Severity.Error);
                var parameters = new DialogParameters { ["IdCliente"] = id };
                DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
                var dialog = DialogService.Show<ClientePrestamosList>($"Otros Prestamos De {_Prestamo.NombreCompleto}", parameters, dialogOptions);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    _Prestamo = (PrestamoClienteUI)result.Data;
                    await GetGarantias(_Prestamo.IdPrestamo);
                    await DialogPrestamoStatus(_Prestamo.IdPrestamo);
                }
            }
       
        }
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
                await DialogPrestamosList(_Prestamo.IdCliente);
            }
        }
        public async Task DialogPrestamoStatus(int id)
        {
   
            var estatuss = await PrestamosEstatusService.Get(new PrestamoEntidades.PrestamoEstatusGetParams { IdPrestamo = id });
            if (estatuss.Count() > 0)
            {
                //estatusesPrestamo = estatuss;
                await NotifyMessageBySnackBar($"Este Prestamo tiene varios status", Severity.Error);
                var parameters = new DialogParameters { ["estatusesPrestamo"] = estatuss };
                DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
                var dialog = DialogService.Show<PrestamoDialogEstatus>("Estatus de este prestamo", parameters, dialogOptions);
            }
        }
        private async Task GetGarantias(int IdPrestamo)
        {
            var gar = await _GarantiasService.GetGarantiasByPrestamo(IdPrestamo);
             _Garantias = gar;
        }
    }
}
