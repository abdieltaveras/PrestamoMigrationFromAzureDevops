﻿using emtSoft.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace PrestamoBLL.Entidades
{

    public class InfoCodeudorDrCr
    {
        public int IdCodeudor { get; internal set; }

        public string Nombres { get; internal set; } = string.Empty;

        public string Apellidos { get; internal set; } = string.Empty;

    }

    public class InfoClienteDrCr 
        //: IInfoClienteDrCr
    {
        
        public string NombreDocumentoIdentidad => Enum.GetName(typeof(TiposIdentificacionCliente ), IdTipoIdentificacion);
        
        public string NumeracionDocumentoIdentidad { get; internal set; } = string.Empty;

        public string InfoLaboral {  get; internal set; } = string.Empty;

        public string TelefonoTrabajo1 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono1;

        public string TelefonoTrabajo2 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono2;

        public string OtrosDetalles { get; internal set; } = string.Empty;

        public string CodigoCliente { get; internal set; } = string.Empty;

        public int IdCliente { get; internal set; } 

        public string Nombres { get; internal set; } = string.Empty;

        public string Apellidos  { get; internal set; } = string.Empty;

        public string TelefonoMovil  { get; internal set; } = string.Empty;

        public string TelefonoCasa  { get; internal set; } = string.Empty;

        public string Imagen1FileName { get; internal set; } = string.Empty;

        public string Imagen2FileName { get; internal set; } = string.Empty;

        public bool Activo { get; internal set; } = false;
        public int IdTipoIdentificacion { get; internal set; }
        public string InfoDelCliente => $"{this.Nombres} {this.Apellidos} {this.TelefonoCasa}";
    }

    public class Cliente : BasePersonaInsUpd
    {
        //public Cliente()
        //{
        //    this.InfoConyugeObj = new Conyuge();
        //    this.InfoDireccionObj = new Direccion();
        //    this.InfoLaboralObj = new InfoLaboral();
        //    this.InfoReferenciasObj = new List<Referencia>();
        //}
        [KeyAttribute]
        public int IdCliente { get; set; } = 0;
        [IgnorarEnParam]
        [StringLength(40)]
        [Required]
        public string Codigo { get; set; } = string.Empty;

        public int IdStatus { get; set; } = -1;
        public bool GenerarSecuencia { get; set; } = true;

        [Display(Name = "Tipo Identificacion")]
        public int IdTipoIdentificacion { get; set; } = (int)TiposIdentificacionPersona.Cedula;
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
        public int IdEstadoCivil { get; set; } = (int)EstadosCiviles.Soltero;
        //<summary>
        //son los datos en formato string que son traidos de las tablas
        //</summary>
        internal string InfoConyuge { get; set; } = string.Empty;
        [IgnorarEnParam]
        public Conyuge InfoConyugeObj { get { return string.IsNullOrEmpty(InfoConyuge) ? new Conyuge() : InfoConyuge.ToType<Conyuge>(); } set { InfoConyuge = value.ToJson(); } }
        //<summary>
        //la direccion en formato json
        //</summary>
        internal string InfoDireccion { get; set; } = string.Empty;
        [IgnorarEnParam]
        public Direccion InfoDireccionObj { get { return string.IsNullOrEmpty(InfoDireccion) ? new Direccion() : InfoDireccion.ToType<Direccion>(); } set { InfoDireccion = value.ToJson(); } }
        /// <summary>
        /// la informacion laboral en formato json
        /// </summary>
        internal string InfoLaboral { get; set; } = string.Empty;
        [IgnorarEnParam]
        public InfoLaboral InfoLaboralObj { get { return string.IsNullOrEmpty(InfoLaboral) ? new InfoLaboral() : InfoLaboral.ToType<InfoLaboral>(); } set { InfoLaboral = value.ToJson(); } }
        /// <summary>
        /// la informacion de referencias en formato json
        /// </summary>
        internal string InfoReferencias { get; set; } = string.Empty;
        [IgnorarEnParam]
        public IEnumerable<Referencia> InfoReferenciasObj { 
            get { return string.IsNullOrEmpty(InfoReferencias) ? new List<Referencia>() : InfoReferencias.ToType<IEnumerable<Referencia>>(); }
        } 

        private List<Referencia> _infoReferencias = new List<Referencia>();
        /// <summary>
        /// para asignar valor de todas las referencias, no olvide enviarlas
        /// todas incluyendo las que ya se hayan registrado
        /// </summary>
        /// <param name="referencias"></param>
        public void SetReferencias(IEnumerable<Referencia> referencias)
        {
            this._infoReferencias = InfoReferenciasObj.ToList();
            this.InfoReferencias  = JsonConvert.SerializeObject(_infoReferencias);
        }
        /// <summary>
        /// guarda el nombre de la imagen
        /// </summary>
        public string Imagen1FileName { get; set; } = string.Empty;
        public string Imagen2FileName { get; set; } = string.Empty;
        

        [Display(Name = "Tiene Pareja o Conyuge")]
        public bool TieneConyuge { get; set; }

        [IgnorarEnParam]
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        [IgnorarEnParam]
        public IEnumerable<string> ImagesForCliente { get; set; }

        public override string ToString()
        {
            //return $"{Codigo}: {Nombres } {Apellidos} {Codigo} ";
            return $" {Nombres } {Apellidos} ";
        }
    }
    public class ClienteGetParams : BaseGetParams
    //: BaseGetParams
    {
        public string Codigo { get; set; } = string.Empty;
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
        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public DateTime? InsertadoDesde { get; set; } = null;

        public DateTime? InsertadoHasta { get; set; } = null;

        /// <summary>
        /// cantidad de registros a seleccionar enviar null
        /// hara que por defecto el procedimiento seleccione 100
        /// -1 hara que seleccione todo
        /// </summary>
        public int CantidadRegistrosASeleccionar { get; set; } = -1;

        /// <summary>
        /// selecciona esta cantidad de registros cada vez que hace el get
        /// </summary>
        public int SeleccionarLuegoDelIdCliente { get; set; } = -1;
        
        public int IdLocalidad { get; set; } = -1;
    }

    public class ClienteDelParams : BaseAnularOrDeleteParams
    {

    }

    public class BuscarClienteParams : BaseGetParams
    {
        public string TextToSearch { get; set; } = string.Empty;
    }

}


