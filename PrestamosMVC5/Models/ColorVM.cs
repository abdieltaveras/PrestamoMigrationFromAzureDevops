using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class ColorVM 
    {
        public Color Color { get; set; }
        public IEnumerable<Color> ListaColores { get; set; }
    }
}