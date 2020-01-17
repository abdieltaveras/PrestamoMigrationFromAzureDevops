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
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class LocalidadInsUptParams
    {
        public int IdLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdTipoLocalidad { get; set; }
        public int IdNegocio { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }



    public class LocalidadGetParams
    {
        public int IdLocalidad { get; set; } = 0;
    }

    public class BuscarLocalidad : Localidad
    {
        public string NombrePadre { get; set; } = string.Empty;
        public string TipoNombrePadre { get; set; } = string.Empty;
        public bool PermiteCalle { get; set; }

    }
    public class BuscarLocalidadParams
    {
        public int IdNegocio { get; set; }

        public string Search { get; set; } = string.Empty;
    }

    public class BuscarNombreLocalidadParams
    {
        public int IdNegocio { get; set; }
        public int IdLocalidad { get; set; }
    }
}
