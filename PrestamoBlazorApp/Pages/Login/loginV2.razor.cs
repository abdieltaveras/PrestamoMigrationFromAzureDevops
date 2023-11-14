using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public partial class LoginV2 : BaseForCreateOrEdit
    {
        private Compañias CompañiaSeleccionada { get; set; } = new Compañias();
        private int LocalidadSeleccionada { get; set; }
        public IEnumerable<Compañias> EmpresasList { get; set; } = new List<Compañias>();


        protected override Task OnInitializedAsync()
        {
            EmpresasList = new List<Compañias>
            {
                new Compañias { IdEmpresa = 1, Empresa = "Intagsa", Localidades = new List <Localidad> { new Localidad {IdLocalidad=1, Nombre="Seibo" } } },
                new Compañias { IdEmpresa = 2, Empresa = "Papito Prestamo", Localidades = new List<Localidad> { new Localidad { IdLocalidad = 2, Nombre = "Seibo" } } }
            };
            return base.OnInitializedAsync();

        }
    }
}


public class Compañias
{
    public int IdEmpresa { get; set; }
    public string Empresa { get; set; }
    public List<Localidad> Localidades { get; set; } = new List<Localidad>();


    }



