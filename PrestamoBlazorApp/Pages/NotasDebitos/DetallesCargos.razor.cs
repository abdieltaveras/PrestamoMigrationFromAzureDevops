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
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace PrestamoBlazorApp.Pages.NotasDebitos
{
    public partial class DetallesCargos : BaseForList
    {
        [Parameter]
        public EventCallback<decimal>  OnAplicarCargo { get; set; }
        NotaDeDebito NotaDe { get; set; } = new NotaDeDebito();
        public int IdPrestamo { get; set; } = -1;
        private string SelectedCodigo { get; set; }
        private IEnumerable<string> CargosSelected { get; set; } = new List<string>();
        private IEnumerable<CodigoCargos> CodigosCargos { get; set; } = new List<CodigoCargos>();

        
        private List<DetalleCargo> DataSelect { get; set; } = new List<DetalleCargo>();
        public decimal MontoCargado { get; set; } = 0;
        public decimal MontoRestante { get; set; } = 0;
        protected override Task OnInitializedAsync()
        {
            CodigosCargos = ListadoCodigosCargos.Get();
            return base.OnInitializedAsync();
            
        }
       
        private async Task AgregarDetalle(MouseEventArgs arg)
        {
            AsignarCargosDebito();
            await OnAplicarCargo.InvokeAsync(this.MontoCargado);
            StateHasChanged();
        }

        private void AsignarCargosDebito()
        {
            var cargos = CodigosCargos.FirstOrDefault(c => c.Codigo == SelectedCodigo);
            var detalleCargo = new DetalleCargo
            {
                CodigoCargo = SelectedCodigo,
                Monto = MontoCargado,
                NombreCargo = cargos != null ? cargos.Nombre : null
            };
            ValorRestante();
            if (MontoCargado + detalleCargo.Monto <= NotaDe.Monto)
            {
                if (MontoCargado<=0)
                {
                    _ = NotifyMessageBySnackBar("No se permiten valores egativos , Verificar Monto Asignado", MudBlazor.Severity.Error);
                }


                DataSelect.Add(detalleCargo);
            }

            else
            {
                _ = NotifyMessageBySnackBar("Monto Excedido, Verificar Monto Asignado", MudBlazor.Severity.Error);

            }
        }

        private void ValorRestante()
        {
            MontoRestante = NotaDe.Monto - MontoCargado;
            var Ver = NotaDe.Monto = MontoRestante;
            _ = NotifyMessageBySnackBar($"acumulado {Ver}", Severity.Warning);
        }

        private void Cal( decimal valor )
        {
            valor= NotaDe.Monto-MontoCargado;
            NotaDe.Monto= valor;
          
        
        }


    }
}
 







