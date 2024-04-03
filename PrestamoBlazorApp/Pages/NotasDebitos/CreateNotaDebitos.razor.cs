using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace PrestamoBlazorApp.Pages.NotasDebitos
{
    public partial class CreateNotaDebitos : BaseForCreateOrEdit
    {
        [Inject]
        ClientesService ClientesService { get; set; }
        [Inject]
        PrestamosService PrestamosService { get; set; }
        NotaDeDebito NotaDe { get; set; } = new NotaDeDebito();
        private PrestamoEntidades.Cliente ClienteSelected { get; set; } = new PrestamoEntidades.Cliente();
        private PrestamoEntidades.Prestamo PrestamoSelected { get; set; } = new PrestamoEntidades.Prestamo();
        public int IdPrestamo { get; set; } = -1;
        private string SelectedCodigo { get; set; }
        private IEnumerable<string> CargosSelected { get; set; } = new List<string>();
        private IEnumerable<CodigoCargos> CodigosCargos { get; set; } = new List<CodigoCargos>();
        private List<DetalleCargo> DataSelect { get; set; } = new List<DetalleCargo>();
        
        public bool estaEditando=true;
        public decimal MontoCargado { get; set; }
        public decimal MontoRestante { get; set; } = 0;
        protected override Task OnInitializedAsync()
        {
            CodigosCargos = ListadoCodigosCargos.Get();
            return base.OnInitializedAsync();
        }
        private async Task GetDataPrestamo()
        {
            PrestamoSelected = new PrestamoEntidades.Prestamo();
            var prestamos = await PrestamosService.GetAsync(new PrestamoEntidades.PrestamosGetParams { idPrestamo = IdPrestamo });
            PrestamoSelected = prestamos.FirstOrDefault();
            IdPrestamo = PrestamoSelected.IdPrestamo;

            StateHasChanged();
        }
        
        private async Task AgregarDetalle(MouseEventArgs arg)
        {
            StateHasChanged();
        }

        //private void AsignarCargosDebito()
        //{
        //    var cargos = CodigosCargos.FirstOrDefault(c => c.Codigo == SelectedCodigo);
        //    var detalleCargo = new DetalleCargo
        //    {
        //        CodigoCargo = SelectedCodigo,
        //        Monto = MontoCargado,
        //        //NombreCargo = cargos.Nombre
        //        NombreCargo = cargos != null ? cargos.Nombre : null
        //    };
        //    if (MontoCargado + detalleCargo.Monto <= NotaDe.Monto)
        //    {
        //        DataSelect.Add(detalleCargo);
        //        MontoCargado += detalleCargo.Monto;
        //        MontoRestante = NotaDe.Monto - detalleCargo.Monto;

        //    }
        //    else
        //    {
        //        _ = NotifyMessageBySnackBar("Monto Excedido, Verificar Monto Asignado", MudBlazor.Severity.Error);
        //        MontoCargado = 0;
        //    }
        //}


    }
}
 







