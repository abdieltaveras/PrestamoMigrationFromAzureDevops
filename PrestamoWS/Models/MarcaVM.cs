using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Models
{
    public class MarcaVM
    {
        public Marca Marca { get; set; }
        public IEnumerable<Marca> ListaMarcas { get; set; }

    }
}
