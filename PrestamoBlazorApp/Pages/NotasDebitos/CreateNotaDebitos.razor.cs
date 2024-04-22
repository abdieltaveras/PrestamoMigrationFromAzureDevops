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
        public decimal MontoAAplicar { get; set; }
        public decimal MontoAplicado { get; set; }
        public decimal MontoPorAplicar { get; set; }
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
        private async Task Calculo1(decimal valor) 

        {
            MontoAplicado += valor;
            MontoPorAplicar = MontoAAplicar - MontoAplicado;
            await NotifyMessageBySnackBar($"valor recibido {valor} acumulado {MontoAplicado}", Severity.Warning);
        }
        

    }
}
 







