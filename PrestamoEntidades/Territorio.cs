using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Territorio : BaseInsUpd
    {
        public int IdDivisionTerritorial { get; set; }
        [Required]
        public int IdLocalidadPadre { get; set; }
        [Required]
        public int IdDivisionTerritorialPadre { get; set; }
        //public int IdNegocio { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Estatus")]
        public bool Activo { get; set; } = true;
        [Required]
        public bool PermiteCalle { get; set; } = false;

        [IgnoreOnParams]
        public string NombreTipoHijoDe { get; set; }

        [IgnoreOnParams]
        public string DescripcionPadre { get; set; }

        public override string ToString()
        {
            return  $"DivisionTerritorial {IdDivisionTerritorial} Padre {IdLocalidadPadre} Nombre{Nombre} PermiteCalle{PermiteCalle} ";
        }
        //public override int GetId()
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class DivisionSearchParams : BaseUsuarioEIdNegocio
    {
        public int IdDivisionTerritorialPadre { get; set; }
    }

    public class TerritorioSearchParams : BaseGetParams
    {
        public int IdLocalidadPadre { get; set; }
    }    

    public class TerritorioGetParams : BaseUsuarioEIdNegocio
    {
    }
}
