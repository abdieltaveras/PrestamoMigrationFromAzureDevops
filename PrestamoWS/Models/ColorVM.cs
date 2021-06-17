using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Models
{
    public class ColorVM
    {
        public Color Color { get; set; }
        public IEnumerable<Color> ListaColores { get; set; }
    }
}
