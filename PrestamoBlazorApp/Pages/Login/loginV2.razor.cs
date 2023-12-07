using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public partial class LoginV2 : BaseForCreateOrEdit
    {
        private Compañias CompañiaSeleccionada { get; set; } = new Compañias();

        private Localidad LocalidadSeleccionada { get; set; }
        private int IdCompañiaSeleccionada { get; set; }
        private int IdLocalidadSeleccionada { get; set; }
        private IEnumerable<Compañias> EmpresasList { get; set; } = new List<Compañias>();

        private IEnumerable<Localidad> LocalidadesDeLaEmpresa { get; set; } = new List<Localidad>();
        protected override Task OnInitializedAsync()
        {
            EmpresasList = new List<Compañias>
            {
                new Compañias { IdEmpresa = 1,Codigo=1, Empresa = "Intagsa (varias localidades)", Localidades = new List <Localidad> {
                    new Localidad {IdLocalidad=1, Nombre="La Romana"},
                    new Localidad {IdLocalidad=2, Nombre="Rsj"},
                    new Localidad {IdLocalidad=3, Nombre="Sosua"},
                      new Localidad {IdLocalidad=3, Nombre="Higuey"},
                } },
                new Compañias { IdEmpresa = 2, Empresa = "Motoprestamo Richiez (una sola localidad)", Localidades = new List<Localidad> { new Localidad { IdLocalidad = 4, Nombre = "Higuey" } } }
            };
            return base.OnInitializedAsync();

        }

        private async Task  OnCompaniaChanged(int idCompañia)
        {
            IdCompañiaSeleccionada = idCompañia;
            var value = EmpresasList.FirstOrDefault(item => item.IdEmpresa == idCompañia);
            
            CompañiaSeleccionada = (value == null) ? null : value;
            IdLocalidadSeleccionada = 0;
            
        }
        protected async Task OnLocalidadChanged(int idLocalidad)
        {
            IdLocalidadSeleccionada = idLocalidad;
            LocalidadSeleccionada = CompañiaSeleccionada.Localidades.FirstOrDefault(item => item.IdLocalidad == idLocalidad); 
        }
    }
}


public class Compañias
{
    public int IdEmpresa { get; set; }
    public string Empresa { get; set; }

    public int Codigo { get; set; }
    public List<Localidad> Localidades { get; set; } = new List<Localidad>();

}



