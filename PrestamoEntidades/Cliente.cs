using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrestamoEntidades
{

    public enum TiposFotosPersonas {Rostro, DocIdentificacion }

    public class InfoCodeudorDrCr
    {
        public int IdCodeudor { get;  set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get;  set; } = string.Empty;

    }

    public class InfoClienteDrCr 
        //: IInfoClienteDrCr
    {
        
        public string NombreDocumentoIdentidad => Enum.GetName(typeof(TiposIdentificacionCliente ), IdTipoIdentificacion);
        
        public string NumeracionDocumentoIdentidad { get;  set; } = string.Empty;

        public string InfoLaboral {  get;  set; } = string.Empty;

        public string TelefonoTrabajo1 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono1;

        public string TelefonoTrabajo2 => this.InfoLaboral.ToType<InfoLaboral>().NoTelefono2;

        public string OtrosDetalles { get;  set; } = string.Empty;

        public string CodigoCliente { get;  set; } = string.Empty;

        public int IdCliente { get;  set; } 

        public string Nombres { get;  set; } = string.Empty;

        public string Apellidos  { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string TelefonoMovil  { get;  set; } = string.Empty;

        public string TelefonoCasa  { get;  set; } = string.Empty;

        public string Imagen1FileName { get;  set; } = string.Empty;

        public string Imagen2FileName { get;  set; } = string.Empty;

        public bool Activo { get;  set; } = false;
        public int IdTipoIdentificacion { get; set; }
        public override string ToString() => $"{this.Nombres} {this.Apellidos} {this.TelefonoCasa}";
    }

    public class Cliente : BasePersonaInsUpd, IUsuarioAndIdLocalidadNegocio
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
        [IgnoreOnParams]
        [StringLength(40)]
        [Required]
        public string Codigo { get; set; } = string.Empty;
        
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
        public DateTime? FechaNacimiento { get; set; } = DateTime.Now;
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
        public Conyuge InfoConyugeObj { get; set; } 
            //{return string.IsNullOrEmpty(InfoConyuge) ? new Conyuge() : InfoConyuge.ToType<Conyuge>(); } set { InfoConyuge = value.ToJson(); } }
        //<summary>
        //la direccion en formato json
        //</summary>
        public string InfoDireccion { get; set; } = string.Empty;
        [IgnoreOnParams]
        public Direccion InfoDireccionObj { get; set; }

        //{ get { return string.IsNullOrEmpty(InfoDireccion) ? new Direccion() : InfoDireccion.ToType<Direccion>(); } set { InfoDireccion = value.ToJson(); } }
        /// <summary>
        /// la informacion laboral en formato json
        /// </summary>

        public void RemoveAllButNumber() {
            TelefonoCasa = TelefonoCasa.RemoveAllButNumber();
            TelefonoMovil = TelefonoMovil.RemoveAllButNumber();
            NoIdentificacion =NoIdentificacion.RemoveAllButNumber();
            InfoConyugeObj.TelefonoTrabajo = InfoConyugeObj.TelefonoTrabajo.RemoveAllButNumber();
            InfoConyugeObj.NoTelefono1 = InfoConyugeObj.NoTelefono1.RemoveAllButNumber();
            InfoReferenciasObj.ForEach(refe => refe.Telefono.RemoveAllButNumber());
        }
        public string InfoLaboral { get; set; } = string.Empty;
        [IgnoreOnParams]
        public InfoLaboral InfoLaboralObj { get; set; }
            //get { return string.IsNullOrEmpty(InfoLaboral) ? new InfoLaboral() : InfoLaboral.ToType<InfoLaboral>(); } set { InfoLaboral = value.ToJson(); } }
        /// <summary>
        /// la informacion de referencias en formato json, use el objeto para trabajar
        /// </summary>
        public string InfoReferencias { get;  set; } = string.Empty;
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
            this.InfoLaboralObj = InfoLaboral.ToType<InfoLaboral>();
            this.InfoDireccionObj = InfoDireccion.ToType<Direccion>();
            this.InfoConyugeObj = InfoConyuge.ToType<Conyuge>();
            this.InfoReferenciasObj = InfoReferencias.ToType<List<Referencia>>();
            this.ImagenesObj = Imagenes.ToType<List<Imagen>>();
            foreach (var item in ImagenesObj)
            {
                item.ConvertNombreArchivoToBase64String(directorioDeImagenes);
            }
            if (this.InfoReferenciasObj == null)
            {
                this.InfoReferenciasObj = new List<Referencia>();
            }
        }

        
        /// <summary>
        /// guarda el nombre de la imagen
        /// </summary>
        public string Imagen1FileName { get; set; } = string.Empty;
        public string Imagen2FileName { get; set; } = string.Empty;


        [Display(Name = "Tiene Pareja o Conyuge")]
        public bool TieneConyuge { get; set; } = false;

        [IgnoreOnParams]
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string Imagenes { get; internal set; }

        [IgnoreOnParams]
        public List<Imagen> ImagenesObj { get; set; } = new List<Imagen>();


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

        [IgnoreOnParams]
        // para indicar que desea convertir a objeto los datos guardados en formato Json como conyuge, informacion laboral, etc
        public bool ConvertToObj { get; set; } = false;
    }

    public class ClienteDelParams : BaseAnularOrDeleteParams
    {

    }

    public class BuscarClienteParams : BaseGetParams
    {
        public string TextToSearch { get; set; } = string.Empty;
        public string Option { get; set; } = string.Empty;
    }

}


