using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevBox.Core.DAL.SQLServer;
namespace PrestamoEntidades
{
    public class Negocio : BaseInsUpd
    {
        
        public string Codigo { get; set; } = string.Empty;
        public string NombreJuridico { get; set; } = string.Empty;

        public string NombreComercial { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public bool Bloqueado { get; set; } = false;

        public bool PermitirOperaciones { get; set; } = true;

        public string CorreoElectronico { get; set; } = string.Empty;
        public string TaxIdNo { get; set; } = string.Empty;
        /// <summary>
        /// Detalles adicionales  Direccion, telefono, etc datos que se puedan enviar en un json o xml
        /// </summary>
        public string OtrosDetalles { get; private set; } = string.Empty;

        [IgnoreOnParams]
        public NegocioInfo NegocioInfoObj { get ; set ; }
        public string Logo { get; set; }

        public string Slogan { get; set; }
    }


    
    public class NegociosGetParams : BaseGetParams
    {
        [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;
        public string NombreJuridico { get; set; } = string.Empty;

        public string NombreComercial { get; set; } = string.Empty;

        public string TaxIdNo { get; set; } = string.Empty;

        public int PermitirOperaciones { get; set; } = -1;

    }

    /// <summary>
    /// otras informaciones relacionadas al negocio
    /// </summary>
    public class NegocioInfo
    {

        [MaxLength(100)]
        public string Direccion1 { get; set; }
        [MaxLength(100)]
        public string Direccion2 { get; set; }
        [MaxLength(20)]
        [Phone]
        public string Telefono1 { get; set; }
        [MaxLength(20)]
        [Phone]
        public string Telefono2 { get; set; }


    }
}
