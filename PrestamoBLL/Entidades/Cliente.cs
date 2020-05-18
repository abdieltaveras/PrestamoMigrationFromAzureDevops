using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
{
    public class InfoClienteDrCr : Cliente, IInfoClienteDrCr
    {
        public string NombreDocumentoIdentidad { get; internal set; }

        public string NumeracionDocumentoIdentidad => this.NoIdentificacion;

        public string TelefonoTrabajo1 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono1;

        public string TelefonoTrabajo2 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono2;

        public string OtrosDetalles { get; internal set; }
    }

    public class Cliente : BasePersonaInsUpd
    {
        [KeyAttribute]
        public int IdCliente { get; set; } = 0;
        [IgnorarEnParam]
        [StringLength(40)]
        [Required]
        public string Codigo { get; set; } = string.Empty;

        public bool GenerarSecuencia { get; set; } = true;

        [Display(Name = "Tipo Identificacion")]
        public int IdTipoIdentificacion { get; set; } = 0;
        [Display(Name = "Profesion u Ocupacion")]
        public int IdTipoProfesionUOcupacion { get; set; } = 0;
        [Required(ErrorMessage = "digite el numero de identificacion")]
        [Display(Name = "No Identificacion")]
        public string NoIdentificacion { get; set; } = string.Empty;
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        [Display(Name = "Telefono movil")]
        public string TelefonoMovil { get; set; } = string.Empty;
        [Display(Name = "Telefono Casa")]
        public string TelefonoCasa { get; set; } = string.Empty;

        [Display(Name = "Correo Electronico")]
        //[EmailAddress(ErrorMessage ="correo electronico invalido")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Display(Name = "Estado Civil Legal")]
        public int EstadoCivil { get; set; } = 0;
        //<summary>
        //son los datos en formato string que son traidos de las tablas
        //</summary>
        public string InfoConyuge { get; set; } = string.Empty;
        //<summary>
        //la direccion en formato json
        //</summary>
        public string InfoDireccion { get; set; } = string.Empty;
        /// <summary>
        /// la informacion laboral en formato json
        /// </summary>
        public string InfoLaboral { get; set; } = string.Empty;
        /// <summary>
        /// la informacion de referencias en formato json
        /// </summary>
        public string InfoReferencia { get; set; } = string.Empty;

        /// <summary>
        /// guarda el nombre de la imagen
        /// </summary>
        public string Imagen1FileName { get; set; } = string.Empty;
        public string Imagen2FileName { get; set; } = string.Empty;

        [Display(Name = "Tiene Pareja o Conyuge")]
        public bool TieneConyuge { get; set; }

        public override string ToString()
        {
            //return $"{Codigo}: {Nombres } {Apellidos} {Codigo} ";
            return $" {Nombres } {Apellidos} ";
        }
    }
    public class ClientesGetParams : BaseGetParams
    //: BaseGetParams
    {
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
        public int IdTipoIdentificacion { get; set; } = -1;
        public string NoIdentificacion { get; set; } = string.Empty;
        //public DateTime FechaNacimiento { get; set; } = InitValues._19000101;
        //public int Sexo { get; set; } = 0;
        //[Display(Name = "Estado Civil")]
        //public int EstadoCivil { get; set; } = -1;
    }

    public class ClienteDelParams : BaseAnularParams
    {

    }

}


