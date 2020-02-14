using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class TerritorioVM
    {
        public Territorio Territorio { get; set; }
        public int territorioSeleccionado { get; set; }
        public IEnumerable<Territorio> ListaTerritorios { get; set; }
    }
}