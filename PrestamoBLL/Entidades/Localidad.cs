using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
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
        public int IdLocalidadPadre { get; set; }
    }

    public class BuscarLocalidad : Localidad
    {
        public string NombrePadre { get; set; } = string.Empty;
        public string TipoNombrePadre { get; set; } = string.Empty;
        public bool PermiteCalle { get; set; }

        public string LocalidadSeleccionada { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"{Nombre} [{Descripcion}] ({TipoNombrePadre} {NombrePadre})";
        }

    }
    public class BuscarLocalidadParams 
    {
        public string Search { get; set; } = string.Empty;

        //public int IdLocalidadNegocio { get; set; } = -1;

        public bool SoloLosQuePermitenCalle { get; set; } = false;

        [IgnorarEnParam]
        public int MinLength { get; set; } = minLengthDefault;
        
        public const int minLengthDefault = 2;
    }

    public class BuscarNombreLocalidadParams : BaseGetParams
    {
        public int IdLocalidad { get; set; }
    }

    public class LocalidadesHijas : BaseCatalogo
    {
        public int IdLocalidad { get; set; }
        public string TipoLocalidad { get; set; }
        public string DivisionTerritorial { get; set; }
        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }

    public class LocalidadPaisesGetParams : BaseGetParams{}
}
