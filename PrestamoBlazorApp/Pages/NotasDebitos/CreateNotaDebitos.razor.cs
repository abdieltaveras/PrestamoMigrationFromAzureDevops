using Microsoft.AspNetCore.Components;
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
        private string selectedCodigo { get; set; }
        private IEnumerable<string> CargosSelected { get; set; } = new List<string>();
        private IEnumerable<CodigoCargos> CodigosCargos { get; set; }
        private List<CodigoCargos> DataSelect { get; set; } = new List<CodigoCargos>();
        public bool estaEditando=true;

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
        protected async Task NombreYCodigo( string args)
        {
            await ActualizarDatosSeleccionados(args);
        }
        private Task ActualizarDatosSeleccionados (string args)
        {
          
            var cargos= CodigosCargos.FirstOrDefault(c => c.Codigo == args);
            DataSelect.Clear();
            DataSelect.Add(cargos);
            return Task.CompletedTask;

            
        }
        private void AgregarNuevoValor(double valor)
        {
            //var cargo= CodigosCargos.FirstOrDefault();
            if (valor > 0)
            {
                 NotaDe.Monto = (decimal?)valor;
                //DataSelect.Add(new CodigoCargos
                //{
                //    Codigo = "rr",
                //    Nombre = "rrr",
                //   NotaDebito =  { Monto = (decimal?)valor }
                //});
                //NotaDe.Monto = 0;
                estaEditando = true;
            }
            else
            {
                estaEditando = false;
            }
        }


        //protected void HandleValueChanged( ChangeEventArgs args)
        //{
        //    IEnumerable<string> selectedValues= new List<string>();
        //    CargosSelected = selectedValues.ToList();
        //    CargosSelected = new List<string>();
        //}




    }
}
 







