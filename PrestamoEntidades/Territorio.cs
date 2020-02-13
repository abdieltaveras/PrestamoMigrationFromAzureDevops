using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Territorio : BaseCatalogo
    {
        public int IdTipoLocalidad { get; set; }
        public int IdLocalidadPadre { get; set; }
        public int IdDivisionTerritorial { get; set; }
        //public int IdNegocio { get; set; }
        //public string Nombre { get; set; }
        public bool PermiteCalle { get; set; } = false;

        [IgnorarEnParam]
        public string NombreTipoHijoDe { get; set; }

        [IgnorarEnParam]
        public string DescripcionPadre { get; set; }


    }

    public class DivisionSearchParams : BaseGetParams
    {
        public int IdDivisionTerritorial { get; set; }
    }

    public class TerritorioSearchParams : BaseGetParams
    {
        public int IdLocalidadPadre { get; set; }
    }    

    public class TerritorioGetParams : BaseGetParams
    {
    }
}
