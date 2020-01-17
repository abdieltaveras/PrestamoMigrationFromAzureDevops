using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class TipoVM
    {
        public Tipo Tipo { get; set; }
        public IEnumerable<Tipo> ListaTipos { get; set; }
    }
}