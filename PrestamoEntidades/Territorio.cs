using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Territorio
    {
        public int IdTipoLocalidad { get; set; }
        public int PadreDe { get; set; }
        public int IdNegocio { get; set; }
        public string Descripcion { get; set; }
        public bool PermiteCalle { get; set; } = false;

    }

    public class TerritoriosConHijo : Territorio
    {
        public string HijoDe { get; set; }

    }
    public class TerritorioSearchParams
    {
        public int IdNegocio { get; set; }
        public int PadreDe { get; set; }
    }    

    public class TerritorioGetParams
    {
        public int IdNegocio { get; set; }
    }
}
