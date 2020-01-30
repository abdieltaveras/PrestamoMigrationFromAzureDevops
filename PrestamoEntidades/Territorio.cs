using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Territorio : BaseInsUpd
    {
        public int IdTipoLocalidad { get; set; }
        public int HijoDe { get; set; }
        public int IdDivisionTerritorial { get; set; }
        //public int IdNegocio { get; set; }
        public string Descripcion { get; set; }
        public bool PermiteCalle { get; set; } = false;

        [IgnorarEnParam]
        public string NombreTipoHijoDe { get; set; }

        [IgnorarEnParam]
        public string DescripcionPadre { get; set; }


    }

    //public class TerritoriosConHijo : Territorio
    //{
    //    public string NombreTipoHijoDe { get; set; }

    //}

    //public class TerritoriosConPadre : Territorio
    //{
    //    public string DescripcionPadre { get; set; }

    //}

    public class DivisionSearchParams
    {
        public int IdNegocio { get; set; }
        public int IdDivisionTerritorial { get; set; }
    }

    public class TerritorioSearchParams
    {
        public int IdNegocio { get; set; }
        public int HijoDe { get; set; }
    }    

    public class TerritorioGetParams
    {
        public int IdNegocio { get; set; }
    }
}
