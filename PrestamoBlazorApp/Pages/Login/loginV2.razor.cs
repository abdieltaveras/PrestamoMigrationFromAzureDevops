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
       private Compañias Compañia { get; set; }= new Compañias();

       public IEnumerable <Compañias> EmpresasList { get; set; } = new List<Compañias>();


        public LoginV2()
        {
            EmpresasList = new List<Compañias>
            {

            new Compañias { IdEmpresas = 1, Empresa = "Intagsa", Localidad = "La Romana" },
            new Compañias { IdEmpresas = 2, Empresa = "Papito Prestamo", Localidad = "seibo" },
            new Compañias { IdEmpresas = 4, Empresa = "FyG", Localidad = "San Pedro de Macoris" },
            new Compañias { IdEmpresas = 3, Empresa = "castillo Prestamo", Localidad = "Higuey" }

            };
        }
    }  
}

    public class Compañias
    {
        public int IdEmpresas { get; set; }
        public string Empresa { get; set; }
        public string Localidad { get; set; }
    }



