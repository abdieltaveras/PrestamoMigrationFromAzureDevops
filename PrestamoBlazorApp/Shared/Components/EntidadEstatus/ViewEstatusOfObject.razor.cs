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
        public int IdEstatus { get; set; }
        [Parameter]
        public int TipoBusqueda { get; set; }
        //private ClientesService ClientesService { get; set; }
        //private PrestamosService PrestamosService { get; set; }
        [Inject]
        private EstatusService EntidadEstatusService { get; set; }
        private PrestamoEntidades.Estatus estatus { get; set; } = new PrestamoEntidades.Estatus();
        //private PrestamoEntidades.Prestamo prestamo = new PrestamoEntidades.Prestamo();

        //private PrestamoEntidades.Cliente cliente = new PrestamoEntidades.Cliente();

        protected override async Task OnInitializedAsync()
        {
            //EntidadEstatus = new PrestamoEntidades.EntidadEstatus();
            await GetEstatusById();
            //await base.OnInitializedAsync();
        }
        public async Task GetEstatusById()
        {
            //IdEstatus = 1;
            var estatuses = await EntidadEstatusService.Get(new PrestamoEntidades.EstatusGetParams { IdEstatus = IdEstatus });
            if (estatuses.Count()>0)
            {
                estatus = estatuses.FirstOrDefault();
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
