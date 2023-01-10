using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Prestamos
{
    public partial class SearchPrestamoById:BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string[] Columns { get; set; } = { };
        private int SearchText { get; set; }
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }
        private int SelectedPropertySearch { get; set; } = 1;

        [Inject]
        PrestamosService PrestamosService { get; set; }
        List<PrestamoConDetallesParaUIPrestamo> prestamos { get; set; } = new List<PrestamoConDetallesParaUIPrestamo>();
        [Parameter]
        public PrestamoConDetallesParaUIPrestamo Value
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
        public EventCallback<PrestamoConDetallesParaUIPrestamo> ValueChanged { get; set; }
        MudMessageBox MudMessageBox { get; set; }

        private PrestamoConDetallesParaUIPrestamo _value;
        private async Task Get()
        {
            if (SearchText > 0)
            {
                PrestamoConDetallesParaUIPrestamo prestamo = await PrestamosService.GetConDetallesForUiAsync(SearchText);
                prestamos.Add(prestamo);
            }
            else
            {
                await Alert("El Id del prestamo debe ser mayor a 0.");
                //await SweetMessageBox("El Id del prestamo debe ser mayor a 0.", "error");
            }
            
        }
        private async Task onSearchClick()
        {
            await Get();
        }
        private async Task SelectedValue(PrestamoConDetallesParaUIPrestamo select)
        {
            Value = select;
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(select));
            }
        }
    }
}
