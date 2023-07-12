using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;


namespace PrestamoEntidades
{
    public enum TiposAmortizacion { No_Amortizable_cuotas_fijas = 1, Amortizable_por_dia_abierto, Amortizable_por_periodo_abierto, Amortizable_cuotas_fijas, No_Amortizable_abierto }
    public class InfoDeudaPrestamoDrCr
    //: IInfoDeudaPrestamoDrCr
    {
        public int CantidadDeCuotas { get; internal set; }

        public float CuotasAtrasadas { get; internal set; }

        public float CuotasLiquidadas { get; internal set; }

        public float CuotasFuturasSinVencer => this.CuotasVigentes - this.CuotasAtrasadas;

        public float CuotasVigentes { get; private set; }
        public decimal TotalCapital { get; internal set; }

        public decimal TotalInteres { get; internal set; }

        public decimal TotalInteresDespuesDeVencido { get; internal set; }

        public decimal TotalMora { get; internal set; }

        public decimal TotalOtrosCargos { get; internal set; }

        public decimal DeudaTotal => this.TotalCapital + this.TotalInteres + this.TotalMora + this.TotalOtrosCargos + this.TotalInteresDespuesDeVencido;
        public decimal DeudaAtrasada { get; internal set; }

        public decimal DeudaALaFecha { get; internal set; }
        public decimal DeudaNoVencida => DeudaTotal - DeudaAtrasada;

        public string OtrosDetalles { get; internal set; } = string.Empty;


        //readonly IEnumerable<CuotaAmpliada> cuotas;
        readonly DateTime Fecha;
        public InfoDeudaPrestamoDrCr(IEnumerable<CxCCuota> cuotas, DateTime fecha)
        {
            this.cuotas = cuotas;
            this.Fecha = fecha;
            this.CalcularDeuda();
        }

        public IEnumerable<CxCCuota> cuotas { get; set; }
        private void CalcularDeuda()
        {
            foreach (var cuota in cuotas)
            {
                this.CantidadDeCuotas++;
                this.CuotasLiquidadas += (cuota.BceGeneral == 0) ? 1 : 0;
                if (cuota.BceGeneral > 0)
                {
                    this.CuotasVigentes++;
                    this.TotalCapital += cuota.BceCapital;
                    this.TotalInteres += cuota.BceInteres;
                    //this.TotalMora += cuota.BceMora;
                    //this.TotalInteresDespuesDeVencido += cuota.BceInteresDespuesDeVencido;
                    this.TotalOtrosCargos += (decimal)cuota.BceOtrosCargos;
                    if (cuota.Atrasada(this.Fecha))
                    {
                        this.CuotasAtrasadas++;
                        this.DeudaAtrasada += this.DeudaTotal;
                    }
                    if (cuota.MenorOIgualALaFecha(this.Fecha))
                    {
                        this.DeudaALaFecha += this.DeudaTotal;
                    }
                }
            }
        }
    }

    public class InfoPrestamoDrCr : IInfoPrestamoDrCr
    {
        public int IdTipoAmortizacion { get; internal set; }
        public string NombreClasificacion { get; internal set; } = string.Empty;

        public string NombreTipoAmortizacion => Enum.GetName(typeof(TiposAmortizacion), IdTipoAmortizacion);

        public string NombreTipoMora { get; internal set; } = string.Empty;

        public string IdTipoMora { get; internal set; } = string.Empty;

        public string OtrosDetalles { get; internal set; } = string.Empty;

        public string NombrePeriodo { get; internal set; } = string.Empty;

        public int IdPrestamo { get; internal set; }

        public string PrestamoNumero { get; internal set; } = string.Empty;

        public decimal TotalPrestado { get; internal set; }

        public DateTime FechaEmisionReal { get; internal set; } = InitValues._19000101;

        public DateTime FechaEmisionParaCalculos { get; internal set; } = InitValues._19000101;

        public DateTime FechaVencimiento { get; internal set; } = InitValues._19000101;


    }
    public class PrestamoConDetallesParaUIPrestamo
    {
        public Prestamo infoPrestamo { get; set; } = new Prestamo();

        public InfoClienteDrCr infoCliente { get; set; } = new InfoClienteDrCr();

        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; set; } = new List<InfoGarantiaDrCr>();

        public IEnumerable<InfoCodeudorDrCr> infoCodeudores { get; set; } = new List<InfoCodeudorDrCr>();

    }
    public class PrestamoClienteUI
    {
        public string NombreDocumentoIdentidad => Enum.GetName(typeof(TiposIdentificacionCliente), IdTipoIdentificacion);

        public string NumeracionDocumentoIdentidad { get; set; } = string.Empty;
        public string DocumentoIdentidadMasked { get { return NumeracionDocumentoIdentidad.Length > 4 ? NumeracionDocumentoIdentidad.Substring(NumeracionDocumentoIdentidad.Length - 4) : NumeracionDocumentoIdentidad; } }

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

        public string Imagen1FileName { get; set; } = string.Empty;

        public string Imagen2FileName { get; set; } = string.Empty;

        public bool Activo { get; set; } = false;
        public int IdTipoIdentificacion { get; set; }
        public override string ToString() => $"{this.Nombres} {this.Apellidos} {this.TelefonoCasa}";

        //

        public int IdTipoAmortizacion { get; set; }
        public string NombreClasificacion { get; internal set; } = string.Empty;

        public string NombreTipoAmortizacion => Enum.GetName(typeof(TiposAmortizacion), IdTipoAmortizacion);

        public string NombreTipoMora { get; set; } = string.Empty;

        public string IdTipoMora { get; set; } = string.Empty;

        //public string OtrosDetalles { get; internal set; } = string.Empty;

        public string NombrePeriodo { get; set; } = string.Empty;

        public int IdPrestamo { get; set; }

        public string PrestamoNumero { get; set; } = string.Empty;

        public decimal TotalPrestado { get; set; }

        public DateTime FechaEmisionReal { get; set; } = InitValues._19000101;

        public DateTime FechaEmisionParaCalculos { get; set; } = InitValues._19000101;

        public DateTime FechaVencimiento { get; set; } = InitValues._19000101;
    }
    public class PrestamoClienteUIGetParam
    {
        public int IdPrestamo { get; set; } = -1;
        public int IdGarantia { get; set; } = -1;
        public int IdCliente { get; set; } = -1;
        public string NoIdentificacion { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public string Matricula { get; set; } = "";
        public string Placa { get; set; } = "";
    }
    public class PrestamoClienteUIGetParamWtSearchText : PrestamoClienteUIGetParam
    {
        public string SearchText { get; set; }
    }
    public class PrestamoConDetallesParaCreditosYDebitos
    //: IPrestamoConDetallesParaCreditosyDebitos
    {
        public InfoPrestamoDrCr infoPrestamo { get; set; }

        public InfoClienteDrCr infoCliente { get; set; }

        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; set; }

        public IEnumerable<InfoCodeudorDrCr> infoCodeudores { get; set; }

        public IEnumerable<CxCCuota> Cuotas { get; set; }

        public InfoDeudaPrestamoDrCr InfoDeuda { get; set; }
    }
    public class PrestamoSearch
    {
        public int IdPrestamo { get; set; }
        public decimal MontoPrestado { get; set; }
        public string PrestamoNumero { get; internal set; } = string.Empty;
        public string Clasificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string FotoCliente { get; set; } = string.Empty;
        public string NoIdentificacion { get; set; } = string.Empty;
    }
    public class Prestamo : BaseInsUpd, IInfoGeneradorCuotas
    {
        public int IdPrestamo { get; set; }

        [IgnoreOnParams]
        [Display(Name = "Prestamo Numero")]
        public string PrestamoNumero { get; set; } = string.Empty;
        /// <summary>
        /// el valor menos 1 indica que no se establecio ningun prestamo a renovar
        /// </summary>
        public int IdPrestamoARenovar { get; set; } = -1;
        [IgnoreOnParams]
        /// attention analizar poner un objeto InfoPrestamoForView que permita poner todos los campos que uno pudiera necesitar como este NumeroPrestamoARenovar, etc
        public string NumeroPrestamoARenovar { get; set; } = string.Empty;

        [Display(Name = "Indique la clasificacion")]
        public int IdClasificacion { get; set; } = -1;
        [Display(Name = "Indique el tipo de amortizacion")]

        public virtual int IdTipoAmortizacion { get; set; } = 1;

        [IgnoreOnParams]
        public TiposAmortizacion TipoAmortizacion
        {
            get { return (TiposAmortizacion)IdTipoAmortizacion; }
            set { IdTipoAmortizacion = (int)value; }
        }
        public int IdEstatus { get; set; }
        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        [IgnoreOnParams]
        public bool TieneGarantias { get { return IdGarantias == null ? false : IdGarantias.Count() > 0; } }
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        [Display(Name = "Indique el cliente")]
        public int IdCliente { get; set; } = 0;

        //[IgnoreOnParams]
        //public List<Garantia> _Garantias { get; set; } = new List<Garantia>();
        [IgnoreOnParams]
        public List<int> IdGarantias { get; set; } = new List<int>();

        //[IgnoreOnParams]
        //public List<Codeudor> _Codeudores { get; set; }
        [IgnoreOnParams]
        public string Cliente { get; set; }
        [IgnoreOnParams]
        public List<int> IdCodeudores { get; set; } = new List<int>();
        [Display(Name = "Fecha de emision")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime FechaEmisionReal { get; set; } = DateTime.Now;

        [ReadOnly(true)]
        public DateTime FechaEmisionParaCalculos { get; internal set; } = DateTime.Now;
        [Display(Name = "fecha de vencimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        
        [IgnoreOnParams(true)]
        public DateTime FechaVencimiento { get; set; }
        [Display(Name = "Indique el codigo de la tasa de interes")]
        public virtual int IdTasaInteres { get; set; } = -1;
        [Display(Name = "La tasa de interes por periodo")]
        [IgnoreOnParams]
        [ReadOnly(true)]
        public decimal TasaDeInteresDelPeriodo { get; set; }
        [Display(Name = "Indique la mora")]
        public int IdTipoMora { get; set; } = -1;
        [Display(Name = "Indique la forma (periodo) de las pago?")]


        [IgnoreOnParams]
        public bool Saldado { get; internal set; } = false;
        [Display(Name = "Seleccione el Periodo")]
        public virtual int IdPeriodo { get; set; } = -1;
        [IgnoreOnParams]
        public Periodo Periodo { get; set; }

        [Display(Name = "Cantidad de Cuotas")]
        //[Range(1, 1000000, ErrorMessage = "Debe indicar un periodo mayor  a cero")]
        //[RegularExpression(("([1-9][0-9]*)"), ErrorMessage ="la cantidad de periodo digitada no es valida debe ser un valor mayor a cero y no puede tener decimales")]
        [Range(typeof(int), "1", "99999999", ErrorMessage = "solo se acepta valores mayor a 0")]
        //[Min(1,ErrorMessage ="el valor minimo aceptado es 1")]
        public virtual int CantidadDePeriodos { get; set; } = 1;
        [Display(Name = "Monto prestado al cliente?")]
        [Required(ErrorMessage = "debe digitar un valor 0 o un valor")]
        //[RegularExpression(("([0-9][0-9]*)"), ErrorMessage = "no se aceptan valores negativos")]
        //[Range(0, 999999999, ErrorMessage = "No se aceptan valores negativos")]
        public virtual decimal MontoPrestado { get; set; }
        [Display(Name = "Deuda del prestamo a renovar ?")]
        [ReadOnly(true)]
        [Range(0, 999999999, ErrorMessage = "No se aceptan valores negativos")]
        public decimal DeudaRenovacion { get; set; }
        /// <summary>
        /// esta propiedad es solo visual no tiene efectos en la tabla su proposito
        /// es poder indicar si el prestamo que se esta trabajando llevara o no renovacion
        /// para que las vistas habiliten entonces la interaccion con el usuario
        /// </summary>
        [IgnoreOnParams]
        [NotMapped]
        [Display(Name = "Habilitar Renovacion")]
        public bool HabilitarRenovacion { get; set; } = false;
        /// <summary>
        /// tiene sumado el dinero emitido al cliente (monto prestado) + le deuda de la r
        /// </summary>
        [IgnoreOnParams]
        public decimal MontoCapital => MontoPrestado + DeudaRenovacion;
        // { get { return MontoPrestado + DeudaRenovacion } internal set { var valor = value;} }
        [Display(Name = "Indique  la Divisa")]
        public int IdDivisa { get; set; } = 1;
        /// <summary>
        /// esta propiedad es solo visual no tiene efectos en la tabla su proposito
        /// es poder indicar si el prestamo que se esta trabajando llevara o no gasto de cierre
        /// inducido si el campo interes gasto de cierre es mayor a cero
        /// </summary>
        [IgnoreOnParams]
        [NotMapped]
        public bool LlevaGastoDeCierre => InteresGastoDeCierre > 0;
        //{ get=> ; internal set; }
        [Range(0.0, 30.00, ErrorMessage = "rango permitido entre 1 y 30%")]
        [Display(Name = "Interes al G/C/?")]
        public virtual decimal InteresGastoDeCierre { get; set; } = 0.00m;
        public decimal MontoGastoDeCierre { get; set; }
        [Display(Name = "Es deducible el G/C?")]
        public virtual bool GastoDeCierreEsDeducible { get; set; } = false;
        [Display(Name = "Financiar el G/C?")]
        public virtual bool FinanciarGastoDeCierre { get; set; } = true;
        [Display(Name = "Cargo interes al G/C ?")]
        public virtual bool CargarInteresAlGastoDeCierre { get; set; } = true;

        /// <summary>
        /// determina si se acomodaran las cuotas o no, lo hace determinando el valor de FechaInicioPrimeraCuota
        /// que el mismo le hayan establecido alguno para hacer el calculo
        /// </summary>
        public virtual bool AcomodarFechaALasCuotas => FechaInicioPrimeraCuota != InitValues._19000101;

        /// <summary>
        ///  si se acomoda el prestamo se debe indicar cual es la fecha en que desea que la primera cuota sea generada
        /// </summary>


        public DateTime FechaInicioPrimeraCuota { get; set; } = InitValues._19000101;

        /// <summary>
        /// este campo es el que tendra la fecha real de donde partira a generar las cuotas y sus fechas de vencimientos, es necesario para cuando al prestamo se le acomode las cuotas
        /// </summary>

        public Prestamo()
        {

        }

        public Prestamo(Periodo periodo)
        {

            this.Periodo = periodo.ToJson().ToType<Periodo>(); ;
        }
        //internal IEnumerable<Cuota> _Cuotas { get; set; } = new List<Cuota>();
        //public DataTable Cuotas => this._Cuotas.ToDataTable();
        //public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        ////this._Garantias.ToDataTable();
        //public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
        public InfoClienteDrCr infoCliente { get; internal set; }
        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; internal set; }
        [Range(0.00, 999999999999.99, ErrorMessage = "no se aceptan valores negativos")]
        [Display(Name = "Otros Cargos")]
        public decimal OtrosCargos { get; set; }

        public bool CargarInteresAOtrosGastos { get; set; }
        /// <summary>
        /// true si es para estimarla generando solamente las primera y ultima cuota o false
        /// si es para generarlas todas se usa normalmente para insertar o actualizar un prestamo
        /// que no tiene operacionescc
        /// </summary>
        [IgnoreOnParams]
        public bool ProyectarPrimeraYUltima { get; set; } = false;

        public override string ToString()
        {
            return $"Monto Prestado {MontoPrestado} a una tasa de {TasaDeInteresDelPeriodo}  por periodo";
        }
        [IgnoreOnParams]
        public bool LlevaGarantia { get; set; }


    }

    public static class ExtMeth
    {
        public static DataTable ToDataTablePcp<T>(this IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }

    public class PrestamoInsUpdParam : Prestamo
    {
        public readonly IEnumerable<CxCCuotaForSqlType> _CuotasList = new List<CxCCuotaForSqlType>();

        public PrestamoInsUpdParam(IEnumerable<CxCCuotaForSqlType> cuotas)
        {
            this._CuotasList = cuotas;
        }
        public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        //this._Garantias.ToDataTable();
        public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();

        //public DataTable Cuotas => this._CuotasList.ToDataTable();
        public DataTable Cuotas => this._CuotasList.ToDataTablePcp();
    }

    internal class PrestamoGarantias
    {
        public int IdGarantia { get; set; }
    }

    internal class PrestamoCodeudores
    {
        public int IdCodeudor { get; set; }
    }

    public class PrestamosSearchParams : BaseGetParams
    {
        public string TextToSearch { set; get; } = string.Empty;
        [IgnoreOnParams]
        public int SearchType { set; get; } = 0;
        //public int SearchType { set; get; } = 0; // valor 0 para prestamos y 1 para clientes / garantias
    }

    public class PrestamosGetParams : BaseIdNegocio
    {
        public int idPrestamo { get; set; } = -1;

        public int idLocalidadNegocio { get; set; } = -1;
        public int idCliente { get; set; } = -1;
        public int idGarantia { get; set; } = -1;
        public DateTime? fechaEmisionRealDesde { get; set; }
        public DateTime? fechaEmisionRealHasta { get; set; }
    }



}



