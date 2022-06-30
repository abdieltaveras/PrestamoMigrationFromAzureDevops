using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class DivisionTerritorial : BaseInsUpd
    {
        public int IdDivisionTerritorial { get; set; }
        
        [Required]
        public int? IdDivisionTerritorialPadre { get; set; }

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
            return  $"DivisionTerritorial {IdDivisionTerritorial} IdPadre {IdDivisionTerritorialPadre} Nombre {Nombre} PermiteCalle{PermiteCalle} ";
        }
    }

    public class DivisionTerritorialGetParams : BaseUsuarioEIdNegocio
    {
        public int idDivisionTerritorial { get; set; } = -1;
        public int IdDivisionTerritorialPadre { get; set; } = -1;
    }

    public class DivisionTerritorialComponentsGetParams 
    {
        public string Usuario { get; set; } = string.Empty;
        public int idDivisionTerritorial { get; set; } = -1;
    }
}
