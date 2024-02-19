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
        private IEnumerable<string> CargosSelected { get; set; } = new HashSet<string>();
        private IEnumerable<CodigoCargos> CodigosCargos { get; set; }

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


        protected void HandleValueChanged(IEnumerable<string> selectedValues)
        {
           
            CargosSelected = selectedValues.ToList();
            CargosSelected = new List<string>();
        }




    }
}
 







