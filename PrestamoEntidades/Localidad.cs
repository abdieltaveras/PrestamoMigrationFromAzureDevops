using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Localidad : BaseInsUpdCatalogo
    {
        public int IdLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdTipoLocalidad { get; set; } = -1;
        //public int IdNegocio { get; set; }
        //public string Nombre { get; set; } = string.Empty;
        [IgnoreOnParams]
        public string Descripcion { get; set; } = string.Empty;

        public override int GetId() => this.IdLocalidad;

        public override string ToString()
        {
            return $"{IdLocalidad} {Nombre} {Descripcion}";
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
        public int IdLocalidad { get; set; } = -1;
        public int IdLocalidadPadre { get; set; } = -1;
        public int IdTipoLocalidad { get; set; } = -1;
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

        public bool SoloLosQuePermitenCalle { get; set; } = false;
        [IgnoreOnParams]
        public int MinLength { get; set; } = minLengthDefault;

        public const int minLengthDefault = 2;
    }

    public class BuscarNombreLocalidadParams : BaseGetParams
    {
        public int IdLocalidad { get; set; }
    }

    public class LocalidadesHijas : BaseInsUpdCatalogo
    {
        public int IdLocalidad { get; set; }
        public string TipoLocalidad { get; set; }
        public string DivisionTerritorial { get; set; }
        public override int GetId() => this.IdLocalidadNegocio;
        
    }

    public class LocalidadPaisesGetParams : BaseGetParams{}
}
