﻿using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Entidades
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
        public InfoDeudaPrestamoDrCr(IEnumerable<Cuota> cuotas, DateTime fecha)
        {
            this.cuotas = cuotas;
            this.Fecha = fecha;
            this.CalcularDeuda();
        }

        public IEnumerable<Cuota> cuotas { get; set; }
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

        public IEnumerable<Cuota> Cuotas { get; internal set; }

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
    public class Prestamo : BaseInsUpd, IInfoGeneradorCuotas
    {
        public int IdPrestamo { get; set; }
        

        [IgnorarEnParam]
        [Display(Name = "Prestamo Numero")]
        public string PrestamoNumero { get; set; } = string.Empty;
        /// <summary>
        /// el valor menos 1 indica que no se establecio ningun prestamo a renovar
        /// </summary>
        public int IdPrestamoARenovar { get; set; } = -1;
        [IgnorarEnParam]
        /// attention analizar poner un objeto InfoPrestamoForView que permita poner todos los campos que uno pudiera necesitar como este NumeroPrestamoARenovar, etc
        public string NumeroPrestamoARenovar { get; internal set; } = string.Empty;

        [Display(Name = "Indique la clasificacion")]
        public int IdClasificacion { get; set; } = -1;
        [Display(Name = "Indique el tipo de amortizacion")]

        public virtual int IdTipoAmortizacion { get; set; } = 1;

        [IgnorarEnParam]
        public TiposAmortizacion TipoAmortizacion
        {
            get { return (TiposAmortizacion)IdTipoAmortizacion; }
            set { IdTipoAmortizacion = (int)value; }
        }

        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        [IgnorarEnParam]
        public bool TieneGarantias { get { return IdGarantias == null ? false : IdGarantias.Count() > 0; } }
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        [Display(Name = "Indique el cliente")]
        public int IdCliente { get; set; } = 0;

        //[IgnorarEnParam]
        //public List<Garantia> _Garantias { get; set; } = new List<Garantia>();
        [IgnorarEnParam]
        public List<int> IdGarantias { get; set; } = new List<int>();

        //[IgnorarEnParam]
        //public List<Codeudor> _Codeudores { get; set; }

        [IgnorarEnParam]
        public List<int> IdCodeudores { get; set; } = new List<int>();
        [Display(Name = "Fecha de emision")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime FechaEmisionReal { get; set; } = DateTime.Now;
        [HiddenInput]
        [ReadOnly(true)]
        public DateTime FechaEmisionParaCalculos { get; internal set; } = DateTime.Now;
        [Display(Name = "fecha de vencimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; set; }
        [Display(Name = "Indique el codigo de la tasa de interes")]
        public virtual int IdTasaInteres { get; set; } = -1;
        [Display(Name = "La tasa de interes por periodo")]
        [IgnorarEnParam]
        [ReadOnly(true)]
        public decimal TasaDeInteresPorPeriodo { get; set; }
        [Display(Name = "Indique la mora")]
        public int IdTipoMora { get; set; } = -1;
        [Display(Name = "Indique la forma (periodo) de las pago?")]


        [IgnorarEnParam]
        public bool Saldado { get; internal set; } = false;
        [Display(Name = "Seleccione el Periodo")]
        public virtual int IdPeriodo { get; set; } = -1;
        [IgnorarEnParam]
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
        [IgnorarEnParam]
        [NotMapped]
        [Display(Name = "Habilitar Renovacion")]
        public bool HabilitarRenovacion { get; set; } = false;
        /// <summary>
        /// tiene sumado el dinero emitido al cliente (monto prestado) + le deuda de la r
        /// </summary>
        [IgnorarEnParam]
        public decimal MontoCapital => MontoPrestado + DeudaRenovacion;
        // { get { return MontoPrestado + DeudaRenovacion } internal set { var valor = value;} }
        [Display(Name = "Indique  la Divisa")]
        public int IdDivisa { get; set; } = 1;
        /// <summary>
        /// esta propiedad es solo visual no tiene efectos en la tabla su proposito
        /// es poder indicar si el prestamo que se esta trabajando llevara o no gasto de cierre
        /// inducido si el campo interes gasto de cierre es mayor a cero
        /// </summary>
        [IgnorarEnParam]
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
        [Display(Name = "Desea acomodar las fechas de las cuotas?")]
        public virtual bool AcomodarFechaALasCuotas { get { return FechaInicioPrimeraCuota != InitValues._19000101; } }
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
        [Range(0.00, 999999999999.99, ErrorMessage = "no se aceptan valores negativos")]
        [Display(Name = "Otros Cargos Sin Interes")]
        public decimal OtrosCargosSinInteres { get; set; }

        public override string ToString()
        {
            return $"Monto Prestado {MontoPrestado} a una tasa de {TasaDeInteresPorPeriodo}  por periodo";
        }

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
        private IEnumerable<CuotaForSqlType> _CuotasList = new List<CuotaForSqlType>();

        public PrestamoInsUpdParam(IEnumerable<CuotaForSqlType> cuotas)
        {
            this._CuotasList = cuotas;
        }
        public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        //this._Garantias.ToDataTable();
        public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
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
        [IgnorarEnParam]
        public int SearchType { set; get; } = 0;
        //public int SearchType { set; get; } = 0; // valor 0 para prestamos y 1 para clientes / garantias
    }

    public class PrestamosGetParams : BaseIdNegocio
    {
        public int idPrestamo { get; set; } = -1;

        public int idLocalidadNegocio { get; set; } = -1;
        public int idCliente { get; set; } = -1;
        public int idGarantia { get; set; } = -1;
        public DateTime fechaEmisionRealDesde { get; set; } = InitValues._19000101;
        public DateTime fechaEmisionRealHasta { get; set; } = InitValues._19000101;
    }

    public class PrestamoConCalculos : Prestamo
    {
        private Func<Task> execCalcs { get; set; } 
        public PrestamoConCalculos()
        {
            execCalcs = NoCalcular;
        }

        private async Task NoCalcular() => await Task.Run(() => { });

        public async Task ExecCalcs() => await execCalcs();


        public void ActivateCalculos() => execCalcs = Calcular;
        public void SetServices(EventHandler<string> notificarMensaje,
            IEnumerable<Clasificacion> clasificaciones,
            IEnumerable<TipoMora> tiposMora,
            IEnumerable<TasaInteres> tasasDeInteres,
            IEnumerable<Periodo> periodos
            )
        {
            execCalcs = Calcular;
            this.OnNotificarMensaje = notificarMensaje;
            this.Clasificaciones = clasificaciones;
            this.TiposMora = tiposMora;
            this.TasasDeInteres = tasasDeInteres;
            this.Periodos = periodos;

        }

        EventHandler<string> OnNotificarMensaje;
        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();

        public override decimal MontoPrestado
        {
            get => base.MontoPrestado;
            set
            {
                RejectNegativeValue(value);
                base.MontoPrestado = value;
                execCalcs();
            }
        }


        public DateTime CalcularFechaVencimiento()
        {
            // primero buscar el periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            var duracion = this.CantidadDePeriodos * this.Periodo.MultiploPeriodoBase;
            var fechaVencimiento = new DateTime();
            if (this.AcomodarFechaALasCuotas)
            {
                throw new Exception("aun no estamos trabajando con acomodar cuotas");
            }
            var fechaInicial = this.FechaEmisionReal;
            switch (this.Periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fechaVencimiento = fechaInicial.AddDays(duracion);
                    break;
                case PeriodoBase.Semana:
                    fechaVencimiento = fechaInicial.AddDays(duracion * 7);
                    break;
                case PeriodoBase.Quincena:
                    var meses = (duracion / 2);
                    var quincenasImpares = (duracion % 2) == 1;
                    fechaVencimiento = fechaInicial;
                    if (meses >= 1)
                    {
                        fechaVencimiento = fechaInicial.AddMonths(meses);
                    }
                    if (quincenasImpares)
                    {
                        fechaVencimiento = fechaVencimiento.AddDays(15);
                    }
                    break;
                case PeriodoBase.Mes:
                    fechaVencimiento = fechaInicial.AddMonths(duracion);
                    break;
                case PeriodoBase.Ano:
                    fechaVencimiento = fechaInicial.AddYears(duracion);
                    break;
                default:
                    break;
            }
            this.FechaVencimiento = fechaVencimiento;
            
            return this.FechaVencimiento;
        }

        public override decimal InteresGastoDeCierre
        {
            get => base.InteresGastoDeCierre;
            set
            {
                RejectNegativeValue(value);
                base.InteresGastoDeCierre = value;
                execCalcs();
            }
        }


        public override int IdTasaInteres
        {
            get => base.IdTasaInteres;
            set
            {
                base.IdTasaInteres = value;
                execCalcs();
            }
        }

        public override int IdPeriodo
        {
            get => base.IdPeriodo;
            set
            {
                base.IdPeriodo = value;
                execCalcs();
            }
        }


        public override int CantidadDePeriodos
        {
            get => base.CantidadDePeriodos;
            set
            {
                RejectNegativeValue(value);
                base.CantidadDePeriodos = value;
                execCalcs();
            }
        }

        public override bool GastoDeCierreEsDeducible
        {
            get => base.GastoDeCierreEsDeducible;
            set
            {
                base.GastoDeCierreEsDeducible = value;
                execCalcs();
            }
        }

        public override bool FinanciarGastoDeCierre
        {
            get => base.FinanciarGastoDeCierre;
            set
            {
                base.FinanciarGastoDeCierre = value;
                execCalcs();
            }
        }

        public override bool CargarInteresAlGastoDeCierre
        {
            get => base.CargarInteresAlGastoDeCierre;
            set
            {
                base.CargarInteresAlGastoDeCierre = value;
                execCalcs();
            }
        }

        public override DateTime FechaEmisionReal
        {
            get => base.FechaEmisionReal;
            set
            {
                base.FechaEmisionReal = value;
                execCalcs();
            }
        }

        private void RejectNegativeValue(decimal value)
        {
            if (value < 0)
            {
                this.OnNotificarMensaje.Invoke(this, "el valor del monto prestamo no puede ser menor o igual a 0");
                return;
            }
        }
        private async Task CalcularGastoDeCierre()
        {
            MontoGastoDeCierre = LlevaGastoDeCierre ? MontoPrestado * (InteresGastoDeCierre / 100) : 0;
        }

        public async Task Calcular()
        {
            
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.FechaVencimiento = this.Cuotas.Last().Fecha;
        }


        public IEnumerable<Cuota> GenerarCuotas(IInfoGeneradorCuotas info)
        {
            var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            return cuotas;
        }

        public TasaInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            var result = BLLPrestamo.Instance.CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);
            return result;
        }

        private async Task CalcularCuotas()
        {
            if (IdPeriodo < 0 || IdTasaInteres <= 0) return;
            this.Periodo = Periodos.Where(per => per.idPeriodo == IdPeriodo).FirstOrDefault();
            var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == IdTasaInteres).FirstOrDefault();
            var tasaDeInteresPorPeriodos = CalculateTasaInteresPorPeriodo(tasaDeInteres.InteresMensual, Periodo);
            this.TasaDeInteresPorPeriodo = tasaDeInteresPorPeriodos.InteresDelPeriodo;
            var infoCuotas = new infoGeneradorDeCuotas(this);

            // todo poner el calculo de tasa de interes por periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            var cuotas = GenerarCuotas(infoCuotas);
            this.Cuotas.Clear();
            this.Cuotas.AddRange(cuotas);
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());
        }
    }

}



