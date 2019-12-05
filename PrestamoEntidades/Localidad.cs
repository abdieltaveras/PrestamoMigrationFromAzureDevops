using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdTipoLocalidad { get; set; }
        public int IdNegocio { get; set; }
        // public int IdTipoLocalidadHijo { get; set; }
        // public string IdTipoLocalidadHijoNombre { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool PermiteCalle { get; set; } = false;
    }

    public class LocalidadGetParams
    {
        public int IdLocalidad { get; set; } = 0;
    }

    public class BuscarLocalidad : Localidad
    {
        public string Descripcion { get; set; } = string.Empty;

    }
    public class BuscarLocalidadParams
    {
        public string Search { get; set; } = string.Empty;
    }
}
