using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.EntidadEstatus
{
    public partial class ViewEstatusOfObject
    {
        [Parameter]
        public string Label { get; set; } = "Estatus";
        [Parameter]
        public int Id { get; set; }
        [Parameter]
        public PrestamoEntidades.eTipoStatus TipoBusqueda { get; set; }
        //private ClientesService ClientesService { get; set; }
        //private PrestamosService PrestamosService { get; set; }
        [Inject]
        private ClientesEstatusService ClientesEstatusService { get; set; }
        [Inject]
        private PrestamosEstatusService PrestamosEstatusService { get; set; }
        [Inject]
        private EstatusService EntidadEstatusService { get; set; }
        private IEnumerable<PrestamoEntidades.ClienteEstatusGet> estatusesCliente { get; set; } = new List<PrestamoEntidades.ClienteEstatusGet>();
        private IEnumerable<PrestamoEntidades.PrestamoEstatusGet> estatusesPrestamo { get; set; } = new List<PrestamoEntidades.PrestamoEstatusGet>();

        [Parameter]
        public bool AllowActions { get; set; } = false;
        //private PrestamoEntidades.Prestamo prestamo = new PrestamoEntidades.Prestamo();

        //private PrestamoEntidades.Cliente cliente = new PrestamoEntidades.Cliente();

        protected override async Task OnInitializedAsync()
        {
            //EntidadEstatus = new PrestamoEntidades.EntidadEstatus();
            estatusesCliente = new List<PrestamoEntidades.ClienteEstatusGet>();
            //private PrestamoEntidades.Prestamo prestamo = new PrestamoEntidades.Prestamo();
            await GetEstatusById();
            //await base.OnInitializedAsync();
        }
        public async Task GetEstatusById()
        {
            //IdEstatus = 1;
            if (TipoBusqueda == PrestamoEntidades.eTipoStatus.Cliente)
            {
                var estatuss = await ClientesEstatusService.Get(new PrestamoEntidades.ClienteEstatusGetParams { IdCliente = Id });
                if (estatuss.Count() > 0)
                {
                    estatusesCliente = estatuss;
                }
            }
            else if (TipoBusqueda == PrestamoEntidades.eTipoStatus.Prestamos)
            {
                var estatuss = await PrestamosEstatusService.Get(new PrestamoEntidades.PrestamoEstatusGetParams { IdPrestamo = Id });
                if (estatuss.Count() > 0)
                {
                    estatusesPrestamo = estatuss;
                }   
            }

            //if (TipoBusqueda == 1)
            //{
            //    var clientes = await ClientesService.GetClientesAsync(new PrestamoEntidades.ClienteGetParams { IdCliente = Id });
            //    if (clientes.Count()>0)
            //    {
            //        cliente = clientes.FirstOrDefault();
            //    }
            //}
            //else
            //{
            //    prestamo = await PrestamosService.GetByIdAsync(Id);
            //}
        }
    }
}
