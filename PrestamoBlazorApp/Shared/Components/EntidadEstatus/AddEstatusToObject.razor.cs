using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
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
        private int IdEstatus { get; set; }
        private string TipoBusquedaStr { get; set; }
        [Inject]
        ClientesService ClientesService { get; set; }
        [Inject]
        PrestamosService PrestamosService { get; set; }
        private PrestamoEntidades.Cliente ClienteSelected { get; set; } = new PrestamoEntidades.Cliente();
        private PrestamoEntidades.Prestamo PrestamoSelected { get; set; } = new PrestamoEntidades.Prestamo();

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
        private async Task GetData()
        {
            IdEstatus = 0;
            ClienteSelected = new PrestamoEntidades.Cliente();
            PrestamoSelected = new PrestamoEntidades.Prestamo();
            if (TipoBusqueda == 1)
            {
                var clientes = await ClientesService.GetClientesAsync(new PrestamoEntidades.ClienteGetParams { IdCliente = Id });
                if (clientes.Count()>0)
                {
                    ClienteSelected = clientes.FirstOrDefault();
                    IdEstatus = ClienteSelected.IdEstatus;
                }
            }
            else
            {
                if (TipoBusqueda == 2)
                {
                    var prestamos = await PrestamosService.GetAsync(new PrestamoEntidades.PrestamosGetParams { idPrestamo = Id });
                    if (prestamos.Count() > 0)
                    {
                        PrestamoSelected = prestamos.FirstOrDefault();
                        IdEstatus = PrestamoSelected.IdEstatus;
                    }
                }
            }
            StateHasChanged();
        }
    }
}
