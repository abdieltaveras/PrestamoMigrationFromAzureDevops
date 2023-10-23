﻿using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{

    public class ClienteTipoBusqueda : Enumeration
    {
        public static ClienteTipoBusqueda Nombre = new(1, nameof(Nombre));
        public static ClienteTipoBusqueda Apellido = new(2, nameof(Apellido));
        public ClienteTipoBusqueda(int id, string name) : base(id, name)
        {
        }
    }

    public enum TiposFotosPersonas { Rostro, DocIdentificacion }

    public class InfoCodeudorDrCr
    {
        public int IdCodeudor { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

    }

    public class InfoClienteDrCr
    //: IInfoClienteDrCr
    {

        public string NombreDocumentoIdentidad => Enum.GetName(typeof(TiposIdentificacionCliente), IdTipoIdentificacion);

        public string NumeracionDocumentoIdentidad { get; set; } = string.Empty;

        public string InfoLaboral { get; set; } = string.Empty;

        public string TelefonoTrabajo1 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono1;

        public string TelefonoTrabajo2 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono2;

        public string OtrosDetalles { get; set; } = string.Empty;

        public string CodigoCliente { get; set; } = string.Empty;

        public int IdCliente { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string TelefonoMovil { get; set; } = string.Empty;

        public string TelefonoCasa { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;


        public bool Activo { get; set; } = false;
        public int IdTipoIdentificacion { get; set; }
        public override string ToString() => $"{this.Nombres} {this.Apellidos} {this.TelefonoCasa}";
    }

    public class Cliente : BasePersonaInsUpd, IUsuarioAndIdLocalidadNegocio
    {

        [KeyAttribute]
        public int IdCliente { get; set; } = 0;
        [IgnoreOnParams]
        [StringLength(40)]
        [Required]
        public string Codigo { get; set; } = string.Empty;
        public bool GenerarSecuencia { get; set; } = true;

        [Display(Name = "Tipo Identificacion")]
        public int IdTipoIdentificacion { get; set; } = (int)TiposIdentificacionPersona.Cedula;
        [Display(Name = "Profesion u Ocupacion")]
        public int IdTipoProfesionUOcupacion { get; set; } = -1;
        [Required(ErrorMessage = "digite el numero de identificacion")]
        [Display(Name = "No Identificacion")]
        public string NoIdentificacion { get; set; } = string.Empty;
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaNacimiento { get; set; } = InitValues._19000101;
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
        public string InfoConyuge { get; set; } = string.Empty;
        [IgnoreOnParams]
        public Conyuge InfoConyugeObj { get; set; } = new Conyuge();
        //{return string.IsNullOrEmpty(InfoConyuge) ? new Conyuge() : InfoConyuge.ToType<Conyuge>(); } set { InfoConyuge = value.ToJson(); } }
        //<summary>
        //la direccion en formato json
        //</summary>
        public string InfoDireccion { get; set; } = string.Empty;
        [IgnoreOnParams]
        public Direccion InfoDireccionObj { get; set; } = new Direccion();

        //{ get { return string.IsNullOrEmpty(InfoDireccion) ? new Direccion() : InfoDireccion.ToType<Direccion>(); } set { InfoDireccion = value.ToJson(); } }
        /// <summary>
        /// la informacion laboral en formato json
        /// </summary>

        public void RemoveAllButNumber()
        {
            TelefonoCasa = TelefonoCasa.RemoveAllButNumber();
            TelefonoMovil = TelefonoMovil.RemoveAllButNumber();
            NoIdentificacion = NoIdentificacion.RemoveAllButNumber();
            if (InfoConyugeObj != null)
            {
                InfoConyugeObj.TelefonoTrabajo = InfoConyugeObj.TelefonoTrabajo.RemoveAllButNumber();
                InfoConyugeObj.TelefonoPersonal = InfoConyugeObj.TelefonoPersonal.RemoveAllButNumber();
            }
            InfoReferenciasObj.ForEach(refe => refe.Telefono.RemoveAllButNumber());
        }
        public string InfoLaboral { get; set; } = string.Empty;
        [IgnoreOnParams]
        public InfoLaboral InfoLaboralObj { get; set; } = new InfoLaboral();
        //get { return string.IsNullOrEmpty(InfoLaboral) ? new InfoLaboral() : InfoLaboral.ToType<InfoLaboral>(); } set { InfoLaboral = value.ToJson(); } }
        /// <summary>
        /// la informacion de referencias en formato json, use el objeto para trabajar
        /// </summary>
        public string InfoReferencias { get; set; } = string.Empty;
        [IgnoreOnParams]
        public List<Referencia> InfoReferenciasObj { get; set; } = new List<Referencia>();

        private List<Referencia> _infoReferencias = new List<Referencia>();
        /// <summary>
        /// Convierte los objetos a representacion interna en Json
        /// </summary>
        public void ConvertObjToJson()
        {
            this.InfoLaboral = InfoLaboralObj.ToJson();
            this.InfoDireccion = InfoDireccionObj.ToJson();
            this.InfoConyuge = InfoConyugeObj.ToJson();
            this.InfoReferencias = InfoReferenciasObj.ToJson();
            for (int i = 0; i < ImagenesObj.Count; i++)
            {
                if (ImagenesObj.Count > 1 && i > 0)
                {
                    this.Imagenes += ",";
                }
                this.Imagenes += ImagenesObj[i].ConvertToJson();

            }

            this.Imagenes = "[" + this.Imagenes + "]";
        }

        /// <summary>
        /// extrae de la representacion interna del json al objeto en si.
        /// </summary>
        public void ConvertJsonToObj(string directorioDeImagenes)
        {
            var infoLaboralObj = InfoLaboral.ToType<InfoLaboral>();
            var infoDireccionObj = InfoDireccion.ToType<Direccion>();
            var infoConyugeObj = InfoConyuge.ToType<Conyuge>();
            var infoReferenciasObj = InfoReferencias.ToType<List<Referencia>>();

            if (!string.IsNullOrEmpty(directorioDeImagenes))
            {
                foreach (var item in ImagenesObj)
                {
                    item.ConvertNombreArchivoToBase64String(directorioDeImagenes);
                }
            }
            if (this.InfoReferenciasObj == null)
            {
                this.InfoReferenciasObj = new List<Referencia>();
            }

            this.InfoLaboralObj = infoLaboralObj == null ? new InfoLaboral() : infoLaboralObj;
            this.InfoDireccionObj = infoDireccionObj == null ? new Direccion() : infoDireccionObj;
            this.InfoConyugeObj = infoConyugeObj == null ? new Conyuge() : infoConyugeObj;
            this.InfoReferenciasObj = infoReferenciasObj == null ? new List<Referencia>() : infoReferenciasObj;
            //this.ImagenesObj = imagenesObj == null ? new List<Imagen>() : imagenesObj;

        }

        public void ConvertImagenJsonToObj(string directorioDeImagenes)
        {
            var imagenesObj = Imagenes.ToType<List<Imagen>>();
            if (!string.IsNullOrEmpty(directorioDeImagenes))
            {
                foreach (var item in imagenesObj)
                {
                    item.ConvertNombreArchivoToBase64String(directorioDeImagenes);
                }
            }
            this.ImagenesObj = imagenesObj == null ? new List<Imagen>() : imagenesObj;

        }

        [Display(Name = "Tiene Pareja o Conyuge")]
        public bool TieneConyuge { get; set; } = false;

        [IgnoreOnParams]
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string Imagenes { get; internal set; }

        [IgnoreOnParams]
        public List<Imagen> ImagenesObj { get; set; } = new List<Imagen>();
        [IgnoreOnParams]
        public List<Imagen> ImagenesRemover { get; set; } = new List<Imagen>();

        public override string ToString()
        {
            //return $"{Codigo}: {Nombres } {Apellidos} {Codigo} ";
            return $" {Nombres} {Apellidos} ";
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

        public string NombreCompleto { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Apodo { get; set; } = string.Empty;

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
        public string NoIdentificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;
    }

}


