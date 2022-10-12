using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.EntidadEstatus
{
    public partial class AddEstatusToObject
    {
        MudBlazor.MudForm form;
        bool success;
        string[] errors = { };
        string LabelBuscar { get; set; } = "Cliente";
        public int _TipoBusqueda { get; set; } = -1;
        [Parameter]
        public int TipoBusqueda { get { return _TipoBusqueda; } set { _TipoBusqueda = value; OnTipoBusquedaChange(); } }
        [Parameter]
        public int Id { get; set; }
        private string TipoBusquedaStr { get; set; }
        private void EstatusSelected(SelectClass selected)
        {

        }
        private void OnTipoBusquedaChange()
        {
            if (TipoBusqueda == 1)
            {
                TipoBusquedaStr = "Cliente";
            }
            else
            {
                if (TipoBusqueda == 2)
                {
                    TipoBusquedaStr = "Prestamo";
                }
                else
                {
                    TipoBusquedaStr = "";
                }
            }
            StateHasChanged();
        }
    }
}
