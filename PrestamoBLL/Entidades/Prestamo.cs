using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PrestamoBLL.Entidades
{
    public enum TiposAmortizacion { No_Amortizable_cuotas_fijas = 1, Amortizable_por_dia_abierto, Amortizable_por_periodo_abierto, Amortizable_cuotas_fijas, No_Amortizable_abierto }
    public class InfoDeudaPrestamoDrCr 
        //: IInfoDeudaPrestamoDrCr
    {
        public int CantidadDeCuotas { get; internal set; }

        public float CuotasAtrasadas { get; internal set; }

        public float CuotasLiquidadas {  get; internal set; }
    
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
        

        readonly IEnumerable<CuotaAmpliada> cuotas;
        readonly DateTime Fecha;
        public InfoDeudaPrestamoDrCr(IEnumerable<CuotaAmpliada> cuotas, DateTime fecha)
        {
            this.cuotas = cuotas;
            this.Fecha = fecha;
            this.CalcularDeuda();
        }

        private void CalcularDeuda()
        {
            foreach (var cuota in cuotas)
            {
                this.CantidadDeCuotas++;
                this.CuotasLiquidadas += (cuota.BalanceTotal == 0) ? 1 : 0;
                if (cuota.BalanceTotal > 0)
                {
                    this.CuotasVigentes++;
                    this.TotalCapital += cuota.BceCapital;
                    this.TotalInteres += cuota.BceInteres;
                    this.TotalMora += cuota.BceMora;
                    this.TotalInteresDespuesDeVencido += cuota.BceInteresDespuesDeVencido;
                    this.TotalOtrosCargos += cuota.BceOtrosCargos;
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
        public Prestamo infoPrestamo { get; internal set; } = new Prestamo();

        public InfoClienteDrCr infoCliente { get; internal set; } = new InfoClienteDrCr();

        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; internal set; } = new List<InfoGarantiaDrCr>();

        public IEnumerable<InfoCodeudorDrCr> infoCodeudores { get; internal set; } = new List<InfoCodeudorDrCr>();

    }

    public class PrestamoConDetallesParaCreditosYDebitos 
        //: IPrestamoConDetallesParaCreditosyDebitos
    {
        public InfoPrestamoDrCr infoPrestamo { get; internal set; }

        public InfoClienteDrCr infoCliente { get; internal set; }

        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; internal set; }

        public IEnumerable<InfoCodeudorDrCr> infoCodeudores { get; internal set; }

        public IEnumerable<CuotaAmpliada> Cuotas { get; internal set; }

        public InfoDeudaPrestamoDrCr InfoDeuda { get; internal set; }
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
    public class Prestamo : BaseInsUpd, IPrestamoForGeneradorCuotas
    {
        public int IdPrestamo { get; set; }
        [IgnorarEnParam]
        [Display(Name ="Prestamo Numero")]
        public string PrestamoNumero { get; internal set; } = string.Empty;
        /// <summary>
        /// el valor menos 1 indica que no se establecio ningun prestamo a renovar
        /// </summary>
        public int IdPrestamoARenovar { get; set; } = -1;
        [IgnorarEnParam]
        /// attention analizar poner un objeto InfoPrestamoForView que permita poner todos los campos que uno pudiera necesitar como este NumeroPrestamoARenovar, etc
        public string NumeroPrestamoARenovar { get; internal set; } = string.Empty;

        [Display(Name = "Indique la clasificacion")]
        public int IdClasificacion { get; set; }
        [Display(Name = "Indique el tipo de amortizacion")]

        public int IdTipoAmortizacion { get; set; }

        [IgnorarEnParam]
        public TiposAmortizacion TipoAmortizacion {
            get { return (TiposAmortizacion)IdTipoAmortizacion; }
            set { IdTipoAmortizacion = (int)value; } } 

        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        [IgnorarEnParam]
        public bool TieneGarantias { get { return IdGarantias==null ? false : IdGarantias.Count() > 0; } }
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        [Display(Name = "Indique el cliente")]
        public int IdCliente { get; set; } = 0;

        [IgnorarEnParam]
        public List<Garantia> _Garantias { get; set; } = new List<Garantia>();
        [IgnorarEnParam]
        public List<int> IdGarantias { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Codeudor> _Codeudores { get; set; }
        
        [IgnorarEnParam]
        public List<int> IdCodeudores { get; set; }
        [Display(Name = "Fecha de emision")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaEmisionReal { get; set; } = DateTime.Now;
        [HiddenInput]
        [ReadOnly(true)]
        public DateTime FechaEmisionParaCalculos { get; internal set; } = DateTime.Now;
        [Display(Name = "fecha de vencimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; internal set; }
        [Display(Name = "Indique el codigo de la tasa de interes")]
        public int IdTasaInteres { get; set; }
        [Display(Name = "La tasa de interes por periodo")]
        [IgnorarEnParam]
        [ReadOnly(true)]
        public decimal TasaDeInteresPorPeriodo { get; set; }
        [Display(Name = "Indique la mora")]
        public int IdTipoMora { get; set; }
        [Display(Name = "Indique la forma (periodo) de las pago?")]
        public int IdPeriodo { get; set; }
        [IgnorarEnParam]
        public Periodo Periodo { get; internal set; }
        [Display(Name = "Cantidad de Cuotas")]
        //[Range(1, 1000000, ErrorMessage = "Debe indicar un periodo mayor  a cero")]
        //[RegularExpression(("([1-9][0-9]*)"), ErrorMessage ="la cantidad de periodo digitada no es valida debe ser un valor mayor a cero y no puede tener decimales")]
        [Min(1,ErrorMessage ="el valor minimo aceptado es 1")]
        public int CantidadDePeriodos { get; set; } = 1;
        [Display(Name = "Monto prestado al cliente?")]
        [Required(ErrorMessage="debe digitar un valor 0 o un valor")]
        //[RegularExpression(("([0-9][0-9]*)"), ErrorMessage = "no se aceptan valores negativos")]
        //[Range(0, 999999999, ErrorMessage = "No se aceptan valores negativos")]
        public decimal MontoPrestado { get; set; }
        [Display(Name = "Deuda del prestamo a renovar ?")]
        [ReadOnly(true)]
        [Range(0, 999999999, ErrorMessage = "No se aceptan valores negativos")]
        public decimal DeudaRenovacion { get; set; }
        /// <summary>
        /// tiene sumado el dinero emitido al cliente (monto prestado) + le deuda de la r
        /// </summary>
        [IgnorarEnParam]
        public decimal TotalPrestado => MontoPrestado + DeudaRenovacion;
        // { get { return MontoPrestado + DeudaRenovacion } internal set { var valor = value;} }
        [Display(Name = "Indique  la Divisa")]
        public int IdDivisa { get; set; } = 1;
        [IgnorarEnParam]
        public bool LlevaGastoDeCierre => InteresGastoDeCierre > 0;
        [Display(Name = "Interes para el gasto de cierre ?")]
        public decimal InteresGastoDeCierre { get; set; }
        [ReadOnly(true)]
        public decimal MontoGastoDeCierre { get; internal set; }
        [Display(Name ="Es deducible el gasto de cierre")]
        public bool GastoDeCierreEsDeducible { get; set; }
        [Display(Name = "Sumo el gasto de cierre a las cuotas ?")]

        public bool SumarGastoDeCierreALasCuotas { get; set; } = true;
        [Display(Name = "Cargo interes al gasto de cierre ?")]
        public bool CargarInteresAlGastoDeCierre { get; set; } = true;
        [Display(Name = "Desea acomodar las fechas de las cuotas?")]
        public bool AcomodarFechaALasCuotas { get { return FechaInicioPrimeraCuota != InitValues._19000101; } }
        /// <summary>
        ///  si se acomoda el prestamo se debe indicar cual es la fecha en que desea que la primera cuota sea generada
        /// </summary>
        [ReadOnly(true)]
        [HiddenInput]
        public DateTime FechaInicioPrimeraCuota { get; internal set; } = InitValues._19000101;

        /// <summary>
        /// este campo es el que tendra la fecha real de donde partira a generar las cuotas y sus fechas de vencimientos, es necesario para cuando al prestamo se le acomode las cuotas
        /// </summary>

        public Prestamo()
        {

        }

        public Prestamo(Periodo periodo)
        {
            var periodoDest = new Periodo();
            _type.CopyPropertiesTo(periodo, periodoDest);
            this.Periodo = periodoDest;
        }
        //internal IEnumerable<Cuota> _Cuotas { get; set; } = new List<Cuota>();
        //public DataTable Cuotas => this._Cuotas.ToDataTable();
        //public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        ////this._Garantias.ToDataTable();
        //public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
        public InfoClienteDrCr infoCliente { get; internal set; }

        public IEnumerable<InfoGarantiaDrCr> infoGarantias { get; internal set; }
    }

    public class PrestamoInsUpdParam : Prestamo
    {
        private IEnumerable<Cuota> _CuotasList = new List<Cuota>();
        private IEnumerable<Codeudor> __Codeudores = new List<Codeudor>();
        private IEnumerable<Garantia> __Garantias = new List<Garantia>();

        public PrestamoInsUpdParam(Prestamo prestamo, IEnumerable<Cuota> cuotas, IEnumerable<Codeudor> codeudores, IEnumerable<Garantia> garantias)
        {
            this._CuotasList = cuotas;
            //var data = codeudores.Select(cod => new { idCodeudor = cod.IdCodeudor });
            //this.__Codeudores = codeudores != null ? codeudores : new List<Codeudor>(); ;
            //this.__Garantias = garantias != null ? garantias : new List<Garantia>(); ;
        }
        public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        //this._Garantias.ToDataTable();
        public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
        public DataTable Cuotas => this._CuotasList.ToDataTable();

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
        [IgnorarEnParam]
        public int SearchType { set; get; } = 0;
        //public int SearchType { set; get; } = 0; // valor 0 para prestamos y 1 para clientes / garantias
    }

    public class PrestamosGetParams : BaseIdNegocio 
    {
        public int idPrestamo { get; set; } = -1;
        public int idCliente { get; set; } = -1;
        public int idGarantia { get; set; } = -1;
        public DateTime fechaEmisionRealDesde { get; set; } = InitValues._19000101;
        public DateTime fechaEmisionRealHasta { get; set; } = InitValues._19000101;
    }
}



