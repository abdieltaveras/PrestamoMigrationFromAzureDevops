using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Localidad : BaseCatalogo
    {
        public int IdLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdTipoLocalidad { get; set; }
        //public int IdNegocio { get; set; }
        //public string Nombre { get; set; } = string.Empty;
        [IgnorarEnParam]
        public string Descripcion { get; set; } = string.Empty;

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }

    public class LocalidadInsUptParams
    {
        public int IdLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdTipoLocalidad { get; set; }
        public int IdNegocio { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class LocalidadGetParams : BaseGetParams
    {
        public int IdLocalidad { get; set; } = 0;
    }

    public class BuscarLocalidad : Localidad
    {
        public string NombrePadre { get; set; } = string.Empty;
        public string TipoNombrePadre { get; set; } = string.Empty;
        public bool PermiteCalle { get; set; }

    }
    public class BuscarLocalidadParams : BaseGetParams
    {
        public string Search { get; set; } = string.Empty;
    }

    public class BuscarNombreLocalidadParams : BaseGetParams
    {
        public int IdLocalidad { get; set; }
    }
}
