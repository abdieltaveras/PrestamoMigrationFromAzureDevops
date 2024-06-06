using Newtonsoft.Json;
using PcpUtilidades;
using PrestamoBLL.Models;
using PrestamoEntidades;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    internal class PrestamoBuilder
    {
        
        #region property members
        //IEnumerable<Cliente> clientes = new List<Cliente>();
        //IEnumerable<Codeudor> codeudores = new List<Codeudor>();
        //IEnumerable<Garantia> garantias = new List<Garantia>();
        //IEnumerable<CxCCuota> cuotas = new List<CxCCuota>();
        //Periodo Periodo = new Periodo();
        private int IdLocalidadnegocio { get; set; }
        private string Usuario { get; set; }
        
        private Prestamo prestamoInProgress = new PrestamoConCalculos();
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
        #endregion property members

        internal static async Task<PrestamoBuilder> Create(Prestamo prestamo)
        {
            var myclass = new PrestamoBuilder();
            await myclass.SetPrestamo(prestamo);
            return myclass;
        }

        private PrestamoBuilder() { }

        #region operaciones
        /// <summary>
        /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
        /// a la base de datos.
        /// </summary>
        /// <param name="prestamo"></param>
        private async Task SetPrestamo(Prestamo prestamo)
        {

            ValidarPrestamo(prestamo);
            this.IdLocalidadnegocio = prestamo.IdLocalidadNegocio;
            this.Usuario = prestamo.Usuario;
            //_type.CopyPropertiesTo(prestamo, prestamoInProgress);
            prestamoInProgress = prestamo.ToJson().ToType<PrestamoConCalculos>();
            //prestamoInProgress.IdNegocio = prestamo.IdNegocio;
            //prestamoInProgress.IdLocalidadNegocio = prestamo.IdLocalidadNegocio;
            //prestamoInProgress.OtrosCargos = prestamo.OtrosCargos;
            SetFechaDeEmision(prestamo.FechaEmisionReal);
            SetClasificacion(prestamo.IdClasificacion);
            //SetAmortizacion(prestamo.TipoAmortizacion);
            SetAmortizacion(prestamo.IdTipoAmortizacion);
            SetRenovacion(prestamo.NumeroPrestamoARenovar, prestamo.IdPrestamoARenovar);
            SetClientes(prestamo.IdCliente);
            SetGarantias(prestamo.IdGarantias);
            SetCodeuDores(prestamo.IdCodeudores);
            SetMontoAPrestar(prestamo.MontoPrestado, prestamo.IdDivisa);
            SetGastDeCierre(prestamo);
            SetPeriodoYDuracion(prestamo.IdPeriodo, prestamo.CantidadDeCuotas); ;
            SetTasaInteres(prestamo.IdTasaInteres);
            SetAcomodarFecha(prestamo.FechaInicioPrimeraCuota);

            SetMoras(prestamo.IdTipoMora);
        }

        private void ValidarPrestamo(Prestamo prestamo)
        {
            BLLValidations.ValueGreaterThanZero(prestamo.IdLocalidadNegocio, "Id Localidad negocio");
            BLLValidations.StringNotEmptyOrNull(prestamo.Usuario, "Users");
            BLLValidations.ValueGreaterThanZero(prestamo.IdNegocio, "IdNegocio");
        }

        public void AddCodeudor(Prestamo prestamo, int idCodeudor)
        {
            BLLValidations.ValueGreaterThanZero(idCodeudor, "IdCodeudor");
            prestamo.IdCodeudores.Add(idCodeudor);

        }
        //private void AddGarantia(Prestamo prestamo, Garantia garantia)
        //{
        //    if (garantia.IdGarantia > 0)
        //    {
        //        prestamo._Garantias.Add(garantia);
        //    }
        //    else
        //    {
        //        throw new NullReferenceException("la garantia a agregar no es valida");
        //    }
        //    // verificar que la garantia no este en otro prestamo, siempre y cuando no tenga la opcion
        //    // indicada de que puede ser una garantia para varios prestamos.
        //    // pero debe es bueno que lo indique
        //    garantias.Add(garantia);
        //}
        public void SetMontoAPrestar(decimal monto, int idDivisa)
        {
            BLLValidations.ValidateRange(monto, getValorMinimo(), getValorMaximo(), "dinero prestado");
            //ValidateRange(idDivisa, 1, 0, "La divisa");
            prestamoInProgress.MontoPrestado = monto;
            prestamoInProgress.IdDivisa = idDivisa;
        }

        private decimal getValorMaximo()
        {
            return -1; // significa infinito
        }

        private decimal getValorMinimo()
        {
            // si hay un valor por renovacion esto pueder ser 0, pero si no es renovacion el valor no puede ser 0
            if (prestamoInProgress.IdPrestamoARenovar < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void SetFechaEmision(DateTime fechaEmision)
        {
            /// validar que la fecha de emision este dentro del rango permitido
            prestamoInProgress.FechaEmisionReal = fechaEmision;
        }

        

        

        private void CargarGastoDeCierre(decimal tasaGastoDeCiere = 0, bool esDeducible = false)
        {
            prestamoInProgress.InteresGastoDeCierre = tasaGastoDeCiere;
            prestamoInProgress.GastoDeCierreEsDeducible = esDeducible;
        }

        private void SetAcomodarFecha(DateTime fechaInicioPrimeraCuota)
        {
            // aqui debe tomar la fecha y debe actualizar entonces la propiedad  [FechaInicioCalculoPrestamo]
            prestamoInProgress.FechaInicioPrimeraCuota = fechaInicioPrimeraCuota;
        }

        
        private void SetMoras(int idTipoMora)
        {
            BLLValidations.ValueGreaterThanZero(idTipoMora, "IdTipoMora");
            var mora =  new TipoMoraBLL(IdLocalidadnegocio, Usuario).GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamoInProgress.IdNegocio, IdTipoMora = idTipoMora }).FirstOrDefault();

            //var mora = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamoInProgress.IdNegocio, IdTipoMora = idTipoMora }).FirstOrDefault();
            prestamoInProgress.IdTipoMora = mora.IdTipoMora;

        }

        private void SetPeriodoYDuracion(int idPeriodo, int cantidadDePeriodo)
        {
            SetPeriodo(idPeriodo, cantidadDePeriodo);
            prestamoInProgress.FechaVencimiento = CalcularFechaVencimiento();
            prestamoInProgress.CantidadDeCuotas = cantidadDePeriodo;
        }

        public DateTime CalcularFechaVencimiento()
        {
            // primero buscar el Periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            
            var fechaVencimiento = new DateTime();
            var periodo = new PeriodoBLL(this.IdLocalidadnegocio, Usuario).GetPeriodos(new PeriodoGetParams { idPeriodo= prestamoInProgress.IdPeriodo }).FirstOrDefault();
            BLLValidations.ObjectNotNull(periodo,"Periodo");
            var duracion = this.prestamoInProgress.CantidadDeCuotas * (int)periodo.MultiploPeriodoBase;
            if (prestamoInProgress.AcomodarFechaALasCuotas)
            {
                throw new Exception("aun no estamos trabajando con acomodar cuotas");
            }
            var fechaInicial = prestamoInProgress.FechaEmisionReal;
            switch (periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fechaVencimiento = fechaInicial.AddDays(duracion);
                    break;
                case PeriodoBase.Semana:
                    fechaVencimiento = fechaInicial.AddDays(duracion * 7);
                    break;
                case PeriodoBase.Quincena:
                    var meses = duracion / 2;
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
            return fechaVencimiento;
        }

        private void SetPeriodo(int idPeriodo, int cantidadPeriodo)
        {
            BLLValidations.ValueGreaterThanZero(idPeriodo, "IdPeriodo");
            BLLValidations.ValueGreaterThanZero(cantidadPeriodo, "cantidadPeriodo");
            var periodo = new PeriodoBLL(IdLocalidadnegocio,Usuario).GetPeriodos(new PeriodoGetParams { IdNegocio = prestamoInProgress.IdNegocio, idPeriodo = idPeriodo }).FirstOrDefault();
            if (!periodo.Activo)
            {
                throw new Exception("El Periodo indicado no es valido porque no esta activo");
            }
            if (periodo.Anulado())
            {
                throw new Exception("El Periodo indicado no es valido porque ha sido anulado");
            }
            this.prestamoInProgress.Periodo = periodo;
            this.prestamoInProgress.IdPeriodo = idPeriodo;
            this.prestamoInProgress.CantidadDeCuotas = cantidadPeriodo;
        }

        private void SetTasaInteres(int idTasaDeInteres)
        {
            BLLValidations.ValueGreaterThanZero(idTasaDeInteres, "IdTasaInteres");
            var tasaDeInteres = new TasaInteresBLL(IdLocalidadnegocio, Usuario).GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = prestamoInProgress.IdNegocio, idTasaInteres = idTasaDeInteres }).FirstOrDefault();
            this.prestamoInProgress.IdTasaInteres = idTasaDeInteres;
            var tasaDeInteresPorPeriodo = new TasaInteresBLL(IdLocalidadnegocio, Usuario).CalcularTasaInteresPorPeriodos(tasaDeInteres.InteresMensual, prestamoInProgress.Periodo);
            this.prestamoInProgress.TasaDeInteresDelPeriodo = tasaDeInteresPorPeriodo.InteresDelPeriodo;

        }

        //private void SetGastDeCierre(decimal tasaGastoDeCierre, bool EsDeducible, bool sumargastoDeCierreALasCuotas, bool cargarInteresAlGastoDeCierre)
        private void SetGastDeCierre(Prestamo prestamo)
        {
            prestamoInProgress.InteresGastoDeCierre = prestamo.InteresGastoDeCierre;
            prestamoInProgress.GastoDeCierreEsDeducible = prestamo.GastoDeCierreEsDeducible;
            prestamoInProgress.CargarInteresAlGastoDeCierre = prestamo.CargarInteresAlGastoDeCierre;
            prestamoInProgress.FinanciarGastoDeCierre = prestamo.FinanciarGastoDeCierre;
            prestamoInProgress.MontoGastoDeCierre = prestamo.MontoGastoDeCierre;
        }

        private void SetCodeuDores(List<int> idCodeudores)
        {
            prestamoInProgress.IdCodeudores = new List<int>();
            if (idCodeudores.Any())
            {
                foreach (var item in idCodeudores)
                {
                    AddCodeudor(prestamoInProgress, item);
                    BLLValidations.ValueGreaterThanZero(item, "IdCodeudor");
                    prestamoInProgress.IdCodeudores.Add(item);
                }
            }
        }

        // se dicidio unicamente usar el metodo por idGarantias el que sigue
        //private void SetGarantias(List<Garantia> garantias)
        //{
        //    prestamoInProgress.IdGarantias = new List<int>();
        //    if (garantias != null)
        //    {
        //        foreach (var item in garantias)
        //        {
        //            AddGarantia(prestamoInProgress, item);
        //            ValidarIdNoMenorNiIgualACero(item.IdGarantia, "IdGarantia");
        //            prestamoInProgress.IdGarantias.Add(item.IdGarantia);
        //        }
        //    }
        //}
        private void SetGarantias(IEnumerable<int> idGarantias)
        {
            // que las garantias en cuestion no tengan otros prestamos vigente
            var tienenPrestamosVigentes = new GarantiaBLL(this.IdLocalidadnegocio, this.Usuario).GarantiasTienenPrestamosVigentes(idGarantias);

            prestamoInProgress.IdGarantias = new List<int>();
            if (idGarantias.Any())
            {
                foreach (var item in idGarantias)
                {
                    BLLValidations.ValueGreaterThanZero(item, "IdGarantia");
                    prestamoInProgress.IdGarantias.Add(item);
                }
            }
        }
        private void SetCodeudores(List<int> idCodeudores)
        {
            if (idCodeudores.Any())
            {
                foreach (var item in idCodeudores)
                {
                    prestamoInProgress.IdCodeudores.Add(item);
                }
            }
        }
        private void SetClientes(int idCliente)
        {
            BLLValidations.ValueGreaterThanZero(idCliente, "IdCliente");
            prestamoInProgress.IdCliente = idCliente;
        }

        private void SetRenovacion(string noPrestamoARenovar, int idPrestamoARenovar)
        {
            //if (idPrestamoARenovar > 0) { throw new NullReferenceException("el valor de idPrestamoARenovar no puede ser 0"); }
            if (!string.IsNullOrEmpty(noPrestamoARenovar) && idPrestamoARenovar > 0)
            {
                this.prestamoInProgress.NumeroPrestamoARenovar = noPrestamoARenovar;
                this.prestamoInProgress.IdPrestamoARenovar = idPrestamoARenovar;
            }

        }

        private void SetAmortizacion(TiposAmortizacion tipoAmortizacion)
        {
            //prestamoInProgress.TipoAmortizacion = tipoAmortizacion;
        }

        private void SetAmortizacion(int idTipoAmortizacion)
        {
            BLLValidations.ValueGreaterThanZero(idTipoAmortizacion, "Tipo Amortizacion");
            var amorti = (TiposAmortizacion)idTipoAmortizacion;
            prestamoInProgress.IdTipoAmortizacion = idTipoAmortizacion;
        }

        private void SetClasificacion(int idClasificacion)
        {
            BLLValidations.ValueGreaterThanZero(idClasificacion, "IdClasificacion");
            // seria bueno analizar que impida que en un prestamo inmobiliario no ponga un vehiculo o incluso que la clasificacion la tome por las garantias
            this.prestamoInProgress.IdClasificacion = idClasificacion;

        }

        private void SetFechaDeEmision(DateTime fechaEmision)
        {
            // la fecha debe estar dentro del rango permitido
            if (fechaEstaEnRangoPermitido(fechaEmision))
            {
                this.prestamoInProgress.FechaEmisionReal = fechaEmision;
            }
        }

        private bool fechaEstaEnRangoPermitido(DateTime fechaEmision)
        {
            return true;
        }

        //public static IEnumerable<CxCCuota> CuotasGenerator(IInfoGeneradorCuotas infGenCuotas)
        //{
        //    var cuotas = CuotasGenerator.CreateCuotas(infGenCuotas);
            
        //}

        
        #endregion operaciones

        public PrestamoInsUpdParam Build()
        {
            //IGeneradorCuotas genCuotas = GetGeneradorCuotas();
            //var cuotas = genCuotas.GenerarCuotas();
            prestamoInProgress.ProyectarPrimeraYUltima = false;
            var cuotas = GeneradorDeCuotas.CreateCuotasMaestroDetalle(this.prestamoInProgress.IdPrestamo, this.prestamoInProgress);
            
            // var cuotasVacias = new List<Cuota>();
            var prestamoConDependencias = new PrestamoInsUpdParam();
            // establecer los valores del maestro y los detalles
            var detalles = MaestroDetalleDebitosBLL.CreateDetallesDr(cuotas);
            var result = MaestroDetalleDebitosBLL.CreateDrMaestroYDetalles(cuotas);
            prestamoConDependencias.CargosMaestro = result.MaestrosDr;

            prestamoConDependencias.CargosDetalles = result.DetallesDr;
            //todo fix
            //var prestamoConDependencias = new PrestamoInsUpdParam(cuotas);
            _type.CopyPropertiesTo(prestamoInProgress, prestamoConDependencias);
            return prestamoConDependencias;
        }
    }

    public class PrestamoConCalculos : Prestamo
    {
        private Func<Task> execCalcs { get; set; }
        public PrestamoConCalculos()
        {
            execCalcs = NoCalcular;
        }

        public new bool LlevaGarantia() => Clasificaciones.Where(cl => cl.IdClasificacion == base.IdClasificacion).FirstOrDefault().RequiereGarantia;

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
            this.OnNotificarMensaje = notificarMensaje;
            this.Clasificaciones = clasificaciones;
            this.TiposMora = tiposMora;
            this.TasasDeInteres = tasasDeInteres;
            this.Periodos = periodos;
        }

        EventHandler<string> OnNotificarMensaje;
        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        public List<IMaestroDebitoConDetallesCxC> CxCMaestroDetalles { get; set; } = new List<IMaestroDebitoConDetallesCxC>();

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


        //public static DateTime CalcularFechaVencimiento(DateTime fechaPrestamo, Periodo periodo, int cantidadDePeriodos,
        //    DateTime FechaInicioPrimeraCuota, bool acomodarFechaALasCuotas )
        //{
        //    var prestamo = new Prestamo();
        //    prestamo.FechaEmisionReal = fechaPrestamo;
        //    prestamo.Periodo = periodo;
        //    prestamo.CantidadDeCuotas = cantidadDePeriodos;
        //    prestamo.FechaInicioPrimeraCuota = FechaInicioPrimeraCuota;
        //    var fechaVencimiento = PrestamoConCalculos.CalcularFechaVencimiento(acomodarFechaALasCuotas,periodo,);
        //    return fechaVencimiento;
        //}
        internal static DateTime CalcularFechaVencimiento(bool acomodarFechaALasCuotas, Periodo periodo, DateTime fechaInicial, int cantidadDeCuotas )
        {
            // primero buscar el Periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            var duracion = cantidadDeCuotas * periodo.MultiploPeriodoBase;
            var fechaVencimiento = new DateTime();
            if (acomodarFechaALasCuotas)
            {
                throw new Exception("aun no estamos trabajando con acomodar cuotas");
            }
            switch (periodo.PeriodoBase)
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
            

            return fechaVencimiento;
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


        public override int CantidadDeCuotas
        {
            get => base.CantidadDeCuotas;
            set
            {
                RejectNegativeValue(value);
                base.CantidadDeCuotas = value;
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
            this.FechaVencimiento = this.CxCMaestroDetalles.Last().Fecha;
        }


        public async Task<PrestamoConCalculos> CalcularPrestamo()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.FechaVencimiento = this.CxCMaestroDetalles.Last().Fecha;
            return this;
        }

        //public IEnumerable<CxCCuota> GenerarCuotas(IInfoGeneradorCuotas info)
        //{
        //    var generadorCuotas = CuotasConCalculo.GetGeneradorDeCuotas(info);
        //    var cuotas = generadorCuotas.GenerarCuotas();
        //    return cuotas;
        //}

        public TasasInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            var result = new TasaInteresBLL(this.IdLocalidadNegocio,this.Usuario).CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);
            return result;
        }

        private async Task CalcularCuotas()
        {
            if (IdPeriodo < 0 || IdTasaInteres <= 0) return;
            base.Periodo = Periodos.Where(per => per.idPeriodo == IdPeriodo).FirstOrDefault();
            var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == IdTasaInteres).FirstOrDefault();
            var tasaDeInteresPorPeriodos = CalculateTasaInteresPorPeriodo(tasaDeInteres.InteresMensual, Periodo);
            base.TasaDeInteresDelPeriodo = tasaDeInteresPorPeriodos.InteresDelPeriodo;
            
            var infoCuotas = new InfoGeneradorDeCuotas(this);
            

            // todo poner el calculo de tasa de interes por Periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            // todo fix
            //var cuotas = GenerarCuotas(infoCuotas);
            
            //this.CxCMaestroDetalles = cuotas.ToList();
            
        }
    }
    //public class PrestamoManager
    //{
    //    //private TipoMoraBLL _TipoMoraBLL { get; set; } 
    //    //private PeriodoBLL _PeriodoBLL { get; set; }

    //    #region property members

    //    private PrestamoConCalculos prestamoInProgress = new PrestamoConCalculos();

    //    public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    //    #endregion property members

    //    internal static async Task<PrestamoManager> Create(Prestamo prestamo)
    //    {

    //        var myclass = new PrestamoManager();
    //        await myclass.SetPrestamo(prestamo);
    //        return myclass;
    //    }
    //    private  PrestamoManager()
    //    {

    //    }

    //    private void NotificadorDeMensaje(object sender, string e)
    //    {

    //    }

    //    //Todo: debo arreglar esto con luis

    //    /// <summary>
    //    /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
    //    /// a la base de datos.
    //    /// </summary>
    //    /// <param name="prestamo"></param>
    //    private async Task SetPrestamo(Prestamo prestamo)
    //    {
    //        BLLValidations.ValueGreaterThanZero(prestamo.IdLocalidadNegocio, "Id Localidad negocio");
    //        BLLValidations.StringNotEmptyOrNull(prestamo.Users, "Users");
    //        // hay que enviar usuario e idlocalidad
    //        //_TipoMoraBLL = new TipoMoraBLL(prestamo.IdLocalidadNegocio,prestamo.Users);
    //        //_PeriodoBLL = new PeriodoBLL(prestamo.IdLocalidadNegocio, prestamo.Users);
    //        var clasificaciones = BLLPrestamo.Instance.GetClasificaciones(new ClasificacionesGetParams { IdNegocio = prestamo.IdNegocio });
    //        var tiposMora = new TipoMoraBLL(prestamo.IdLocalidadNegocio, prestamo.Users).GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamo.IdNegocio });
    //        var tasasDeInteres = new TasaInteresBLL(prestamo.IdLocalidadNegocio,prestamo.Users).GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = prestamo.IdNegocio});
    //        var periodos = new PeriodoBLL(prestamo.IdLocalidadNegocio, prestamo.Users).GetPeriodos(new PeriodoGetParams { IdNegocio = prestamo.IdNegocio });

    //        prestamoInProgress = new PrestamoConCalculos();
    //        prestamoInProgress = prestamo.ToJson().ToType<PrestamoConCalculos>();
    //        prestamoInProgress.SetServices(this.NotificadorDeMensaje, clasificaciones, tiposMora, tasasDeInteres, periodos);

    //        await prestamoInProgress.ExecCalcs();
    //    }

    //    private IGeneradorCuotas GetGeneradorCuotas()
    //    {
    //        prestamoInProgress.ProyectarPrimeraYUltima = false;
    //        //var tipoAmortizacion = (TiposAmortizacion)prestamoInProgress.IdTipoAmortizacion;
    //        return CuotasConCalculo.GetGeneradorDeCuotas(prestamoInProgress);
    //    }


    //    public PrestamoInsUpdParam Build()
    //    {
    //        IGeneradorCuotas genCuotas = GetGeneradorCuotas();
    //        var cuotas = genCuotas.GenerarCuotas();
    //        // var cuotasVacias = new List<Cuota>();
    //        var prestamoConDependencias = new PrestamoInsUpdParam(cuotas);
    //        _type.CopyPropertiesTo(prestamoInProgress, prestamoConDependencias);
    //        return prestamoConDependencias;
    //    }
    //}


}
