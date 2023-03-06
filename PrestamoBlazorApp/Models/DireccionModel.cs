using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Models
{
    public class DireccionModel : Direccion
    {
        public string SelectedLocalidad { get; set; }
    }
}
