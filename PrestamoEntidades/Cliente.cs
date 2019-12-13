﻿using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PcProg.DAL;


namespace PrestamoEntidades
{
    [Table("tblClientes", Schema = "sis")]
    public class Cliente : BasePersonaInsUpd, ISecuenciable
    {
        [KeyAttribute]
        public int IdCliente { get; set;} = 0;
        
        [Secuenciable]
        [StringLength(100)]
        [Required]
        public string Codigo { get; set; } = string.Empty;
        [Display(Name = "Tipo Identificacion")]
        public int IdTipoIdentificacion { get; set; } = 0;
        [Display(Name = "Profesion u Ocupacion")]
        public int IdTipoProfesionUOcupacion { get; set; } = 0;
        [Required]
        [Display(Name = "No Identificacion")]
        public string NoIdentificacion { get; set; } = string.Empty;
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        [Display(Name = "Telefono movil")]
        public string TelefonoMovil { get; set; } = string.Empty;
        [Display(Name = "Telefono Casa")]
        public string TelefonoCasa { get; set; } = string.Empty;
        [Display(Name = "Estado Civil")]
        [EmailAddress(ErrorMessage ="correo electronico invalido")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Display(Name = "Estado Civil")]
        public EstadoCivil EstadoCivil { get; set; } = EstadoCivil.Soltero;
        //<summary>
        //son los datos en formato string que son traidos de las tablas
        //</summary>
        public string InfoConyuge { get; set;} = string.Empty;
        //<summary>
        //la direccion en formato json
        //</summary>
        public string  InfoDireccion { get; set; } = string.Empty;
        /// <summary>
        /// la informacion laboral en formato json
        /// </summary>
        public string InfoLaboral { get; set; } = string.Empty;
        [Required]
        [NotMapped]
        public bool GenerarSecuencia { get; set; } = true;
        public override int GetId() => this.IdCliente;
        public override  string  ToString()
        {
            //return $"{Codigo}: {Nombres } {Apellidos} {Codigo} ";
            return $" {Nombres } {Apellidos} ";
        }
    }
    [SpGetProcedure("spGetClientes")]
    [Schema("sis")]
    public class ClientesGetParams 
        //: BaseGetParams
    {
        public int IdNegocio { get; set; } = -1;
        public int IdCliente { get; set; } = -1;
        /// <summary>
        /// indica si esta o no activo, por defecto pone que sea true
        /// </summary>
        public int Activo { get; set; } = -1;
        // -CHEQUEAR antes estaba asi Int16  en vez de bool asi Int16 Activo { get; set; } = 1;
        //public string Nombres { get; set; } = string.Empty;
        //public string Apellidos { get; set; } = string.Empty;
        //// PersonaInfoBasicaSinCodigo
        //public string Codigo { get; set; } = string.Empty;
        //// PersonaInfoAmpliadaConCodigoGetParams
        //public int IdTipoIdentificacion { get; set; } = -1;
        //public string NoIdentificacion { get; set; } = string.Empty;
        //public DateTime FechaNacimiento { get; set; } = new DateTime(1900, 1, 1);
        //public int Sexo { get; set; } = 0;
        //[Display(Name = "Estado Civil")]
        //public int EstadoCivil { get; set; } = -1;
    }

    [SpDelProcedure("spDelCliente")]
    [Schema("sis")]
    public class ClienteDelParams : BaseDelParams
    {

    }

}


