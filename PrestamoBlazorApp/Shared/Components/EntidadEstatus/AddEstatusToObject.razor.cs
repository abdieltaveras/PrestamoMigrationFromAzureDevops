using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.EntidadEstatus
{
    public partial class AddEstatusToObject : BaseForCreateOrEdit
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
        [Inject]
        ClientesEstatusService ClientesEstatusService { get; set; }
        [Inject]
        PrestamosEstatusService PrestamosEstatusService { get; set; }
        [Inject]
        ClientesService ClientesService { get; set; }
        [Inject]
        PrestamosService PrestamosService { get; set; }
        private PrestamoEntidades.Cliente ClienteSelected { get; set; } = new PrestamoEntidades.Cliente();
        private PrestamoEntidades.Prestamo PrestamoSelected { get; set; } = new PrestamoEntidades.Prestamo();
        public int SelectedEstatus { get; set; }
        private ClienteEstatus ClienteEstatus { get; set; } = new ClienteEstatus();
        private PrestamoEstatus PrestamoEstatus { get; set; } = new PrestamoEstatus();
        protected override async Task OnInitializedAsync()
        {
            ClienteEstatus = new ClienteEstatus();
            PrestamoEstatus = new PrestamoEstatus();
        }
        private void EstatusSelected(SelectClass selected)
        {
            SelectedEstatus = Convert.ToInt32( selected.Value);
        }
        private void OnTipoBusquedaChange()
        {
            ClienteSelected = new Cliente();
            PrestamoSelected = new Prestamo();
            ClienteEstatus = new ClienteEstatus();
            PrestamoEstatus = new PrestamoEstatus();
            SelectedEstatus = -1;
            Id = -1;
            if (TipoBusqueda == (int)eAddEstatusTo.Clientes)
            {
                TipoBusquedaStr = "Cliente";
            }
            else
            {
                if (TipoBusqueda == (int)eAddEstatusTo.Prestamos)
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
            ClienteSelected = new PrestamoEntidades.Cliente();
            PrestamoSelected = new PrestamoEntidades.Prestamo();
            if (TipoBusqueda == (int)eAddEstatusTo.Clientes)
            {
                var clientes = await ClientesService.GetClientesAsync(new PrestamoEntidades.ClienteGetParams { IdCliente = Id });
                if (clientes.Count()>0)
                {
                    ClienteSelected = clientes.FirstOrDefault();
                    Id = ClienteSelected.IdCliente;
                }
            }
            else
            {
                if (TipoBusqueda == (int)eAddEstatusTo.Prestamos)
                {
                    var prestamos = await PrestamosService.GetAsync(new PrestamoEntidades.PrestamosGetParams { idPrestamo = Id });
                    if (prestamos.Count() > 0)
                    {
                        PrestamoSelected = prestamos.FirstOrDefault();
                        Id = PrestamoSelected.IdPrestamo;
                    }
                }
            }
            StateHasChanged();
        }
        private async Task OnAsignarClick()
        {
            if (TipoBusqueda == (int)eAddEstatusTo.Clientes)
            {
                ClienteEstatus.IdEstatus = SelectedEstatus;
                ClienteEstatus.IdCliente = ClienteSelected.IdCliente;
                await Handle_SaveData(()=> ClientesEstatusService.Save(ClienteEstatus));
            }else if (TipoBusqueda == (int)eAddEstatusTo.Prestamos)
            {
                PrestamoEstatus.IdEstatus = SelectedEstatus;
                PrestamoEstatus.IdPrestamo = PrestamoSelected.IdPrestamo;
                await Handle_SaveData(()=> PrestamosEstatusService.Save(PrestamoEstatus));
            }
            await SweetMessageBox("Asignado correctamente");
            await GetData();
        }
    }
}
