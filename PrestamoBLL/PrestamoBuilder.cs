using Newtonsoft.Json;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class PrestamoBuilder
    {
        TipoMoraBLL tipoMoraBLL = new TipoMoraBLL(-1, "");
        PeriodoBLL periodoBLL = new PeriodoBLL(-1, "");

        #region property members

        PrestamoConCalculos prestamoInProgress = new PrestamoConCalculos();
        Periodo periodo = new Periodo();
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
        #endregion property members

        public PrestamoBuilder(Prestamo prestamo)
        {
            SetPrestamo(prestamo);
        }

        
        
        private void NotificadorDeMensaje(object sender, string e)
        {

        }

        //Todo: debo arreglar esto con luis

        /// <summary>
        /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
        /// a la base de datos.
        /// </summary>
        /// <param name="prestamo"></param>
        private async Task SetPrestamo(Prestamo prestamo)
        {
             // hay que enviar usuario e idlocalidad
            var clasificaciones = BLLPrestamo.Instance.GetClasificaciones(new ClasificacionesGetParams { IdNegocio = 1 });
            var tiposMora = tipoMoraBLL.GetTiposMoras(new TipoMoraGetParams { IdNegocio = 1 });
            var tasasDeInteres = new TasaInteresBLL(-1,"Luis Prueba").GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = 1 });
            var periodos = periodoBLL.GetPeriodos(new PeriodoGetParams { IdNegocio = 1 });
            prestamoInProgress = new PrestamoConCalculos();
            prestamoInProgress = prestamo.ToJson().ToType<PrestamoConCalculos>();

            prestamoInProgress.SetServices(this.NotificadorDeMensaje, clasificaciones, tiposMora, tasasDeInteres, periodos);
            await prestamoInProgress.ExecCalcs();
        }

        private IGeneradorCuotas GetGeneradorCuotas()
        {
            prestamoInProgress.ProyectarPrimeraYUltima = false;
            //var tipoAmortizacion = (TiposAmortizacion)prestamoInProgress.IdTipoAmortizacion;
            return CuotasConCalculo.GetGeneradorDeCuotas(prestamoInProgress);
        }

        
        public PrestamoInsUpdParam Build()
        {
            IGeneradorCuotas genCuotas = GetGeneradorCuotas();
            var cuotas = genCuotas.GenerarCuotas();
            // var cuotasVacias = new List<Cuota>();
            var prestamoConDependencias = new PrestamoInsUpdParam(cuotas);
            _type.CopyPropertiesTo(prestamoInProgress, prestamoConDependencias);
            return prestamoConDependencias;
        }
    }

    public class PrestamoBuilder3
    {
        TipoMoraBLL tipoMoraBLL = new TipoMoraBLL(-1, "");
        PeriodoBLL periodoBLL = new PeriodoBLL(-1, "");
        #region property members
        List<Cliente> clientes = new List<Cliente>();
        List<Codeudor> codeudores = new List<Codeudor>();
        List<Garantia> garantias = new List<Garantia>();
        List<CxCCuota> cuotas = new List<CxCCuota>();
        Prestamo prestamoInProgress = new Prestamo();
        Periodo periodo = new Periodo();
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
        #endregion property members

        public PrestamoBuilder3(Prestamo prestamo)
        {
            SetPrestamo(prestamo);
        }

        #region operaciones
        /// <summary>
        /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
        /// a la base de datos.
        /// </summary>
        /// <param name="prestamo"></param>
        private void SetPrestamo(Prestamo prestamo)
        {
            //_type.CopyPropertiesTo(prestamo, prestamoInProgress);
            validarIdNoMenorNiIgualACero(prestamo.IdNegocio, "IdNegocio");
            prestamoInProgress.IdNegocio = prestamo.IdNegocio;
            prestamoInProgress.IdLocalidadNegocio = prestamo.IdLocalidadNegocio;
            prestamoInProgress.OtrosCargos = prestamo.OtrosCargos;
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
            SetPeriodoYDuracion(prestamo.IdPeriodo, prestamo.CantidadDePeriodos); ;
            SetTasaInteres(prestamo.IdTasaInteres);
            SetAcomodarFecha(prestamo.FechaInicioPrimeraCuota);

            SetMoras(prestamo.IdTipoMora);
        }
        public void AddCodeudor(Prestamo prestamo, int idCodeudor)
        {
            validarIdNoMenorNiIgualACero(idCodeudor, "IdCodeudor");
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
            validateRange(monto, getValorMinimo(), getValorMaximo(), "dinero prestado");
            //validateRange(idDivisa, 1, 0, "La divisa");
            prestamoInProgress.MontoPrestado = monto;
            prestamoInProgress.IdDivisa = idDivisa;
        }

        private decimal getValorMaximo()
        {
            return 100000000;
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

        private void validarIdNoMenorNiIgualACero(int id, string nombrePropiedad)
        {
            if (id <= 0) { throw new NullReferenceException($"el valor de la propiedad {nombrePropiedad} no puede ser menor o igual a cero"); }
        }

        private void validateRange(decimal valor, decimal minimo, decimal maximo, string propiedad)
        {
            if (valor < minimo)
            {
                throw new ArgumentOutOfRangeException($"El valor de {propiedad} es menor al valor minimo aceptado el cual es {minimo}");
            }
            if (valor > maximo)
            {
                throw new ArgumentOutOfRangeException($"El valor de {propiedad} es mayor que el valor maximo aceptado el cual es {minimo}");
            }
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
            validarIdNoMenorNiIgualACero(idTipoMora, "IdTipoMora");
            var mora = tipoMoraBLL.GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamoInProgress.IdNegocio, IdTipoMora = idTipoMora }).FirstOrDefault();

            //var mora = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamoInProgress.IdNegocio, IdTipoMora = idTipoMora }).FirstOrDefault();
            prestamoInProgress.IdTipoMora = mora.IdTipoMora;

        }

        private void SetPeriodoYDuracion(int idPeriodo, int cantidadDePeriodo)
        {
            SetPeriodo(idPeriodo, cantidadDePeriodo);
            prestamoInProgress.FechaVencimiento = CalcularFechaVencimiento();
            prestamoInProgress.CantidadDePeriodos = cantidadDePeriodo;
        }

        public DateTime CalcularFechaVencimiento()
        {
            // primero buscar el periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            var duracion = this.prestamoInProgress.CantidadDePeriodos * (int)periodo.MultiploPeriodoBase;
            var fechaVencimiento = new DateTime();
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
            validarIdNoMenorNiIgualACero(idPeriodo, "IdPeriodo");
            validarIdNoMenorNiIgualACero(cantidadPeriodo, "cantidadPeriodo");
            var periodo = periodoBLL.GetPeriodos(new PeriodoGetParams { IdNegocio = prestamoInProgress.IdNegocio, idPeriodo = idPeriodo }).FirstOrDefault();
            if (!periodo.Activo)
            {
                throw new Exception("El periodo indicado no es valido porque no esta activo");
            }
            if (periodo.Anulado())
            {
                throw new Exception("El periodo indicado no es valido porque ha sido anulado");
            }
            this.prestamoInProgress.Periodo = periodo;
            this.prestamoInProgress.IdPeriodo = idPeriodo;
            this.prestamoInProgress.CantidadDePeriodos = cantidadPeriodo;
        }

        private void SetTasaInteres(int idTasaDeInteres)
        {
            validarIdNoMenorNiIgualACero(idTasaDeInteres, "IdTasaDeInteres");
            var tasaDeInteres = new TasaInteresBLL(-1,"Luis Prueba").GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = prestamoInProgress.IdNegocio, idTasaInteres = idTasaDeInteres }).FirstOrDefault();
            this.prestamoInProgress.IdTasaInteres = idTasaDeInteres;
            var tasaDeInteresPorPeriodo = new TasaInteresBLL(-1, "Luis Prueba").CalcularTasaInteresPorPeriodos(tasaDeInteres.InteresMensual, prestamoInProgress.Periodo);
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
            if (idCodeudores != null)
            {
                foreach (var item in idCodeudores)
                {
                    AddCodeudor(prestamoInProgress, item);
                    validarIdNoMenorNiIgualACero(item, "IdCodeudor");
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
        //            validarIdNoMenorNiIgualACero(item.IdGarantia, "IdGarantia");
        //            prestamoInProgress.IdGarantias.Add(item.IdGarantia);
        //        }
        //    }
        //}
        private void SetGarantias(IEnumerable<int> idGarantias)
        {
            // que las garantias en cuestion no tengan otros prestamos vigente
            var tienenPrestamosVigentes = new GarantiaBLL(-1, "Luis Prueba").GarantiasTienenPrestamosVigentes(idGarantias);

            prestamoInProgress.IdGarantias = new List<int>();
            if (garantias != null)
            {
                foreach (var item in idGarantias)
                {
                    validarIdNoMenorNiIgualACero(item, "IdGarantia");
                    prestamoInProgress.IdGarantias.Add(item);
                }
            }
        }
        //private void SetCodeudores(List<Codeudor> codeudores)
        //{
        //    foreach (var item in codeudores)
        //    {
        //        AddCodeudor(prestamoInProgress, item);
        //        prestamoInProgress.IdCodeudores.Add(item.IdCodeudor);
        //    }
        //}
        private void SetClientes(int idCliente)
        {
            validarIdNoMenorNiIgualACero(idCliente, "IdCliente");
            prestamoInProgress.IdCliente = idCliente;
        }

        private void SetRenovacion(string noPrestamoARenovar, int idPrestamoARenovar)
        {
            if (idPrestamoARenovar == 0) { throw new NullReferenceException("el valor de idPrestamoARenovar no puede ser 0"); }
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
            validarIdNoMenorNiIgualACero(idTipoAmortizacion, "Tipo Amortizacion");
            var amorti = (TiposAmortizacion)idTipoAmortizacion;
            prestamoInProgress.IdTipoAmortizacion = idTipoAmortizacion;
        }

        private void SetClasificacion(int idClasificacion)
        {
            validarIdNoMenorNiIgualACero(idClasificacion, "IdClasifiacion");
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
        private IGeneradorCuotas GetGeneradorCuotas()
        {
            //var tipoAmortizacion = (TiposAmortizacion)prestamoInProgress.IdTipoAmortizacion;
            return GetGeneradorDeCuotas(prestamoInProgress);
        }
        


        public static IGeneradorCuotas GetGeneradorDeCuotas(IInfoGeneradorCuotas info)
        {
            IGeneradorCuotas generadorCuotas = null;
            var tipoAmortizacion = info.TipoAmortizacion;
            switch (tipoAmortizacion)
            {
                case TiposAmortizacion.No_Amortizable_cuotas_fijas:
                    generadorCuotas = new GeneradorCuotasFijasNoAmortizable(info);
                    break;
                case TiposAmortizacion.Amortizable_por_dia_abierto:
                    break;
                case TiposAmortizacion.Amortizable_por_periodo_abierto:

                    break;
                case TiposAmortizacion.Amortizable_cuotas_fijas:
                    break;
                case TiposAmortizacion.No_Amortizable_abierto:
                    break;
                default:
                    break;
            }

            if (generadorCuotas == null)
            {
                throw new NotImplementedException("no se ha implementado la generacion de cuotas aun para " + tipoAmortizacion.ToString());
            }
            return generadorCuotas;
        }
        #endregion operaciones

        public PrestamoInsUpdParam Build()
        {
            IGeneradorCuotas genCuotas = GetGeneradorCuotas();
            var cuotas = genCuotas.GenerarCuotas();
            // var cuotasVacias = new List<Cuota>();
            var prestamoConDependencias = new PrestamoInsUpdParam(cuotas);
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

        public List<CxCCuota> Cuotas { get; set; } = new List<CxCCuota>();

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


        public static DateTime CalcularFechaVencimiento(DateTime fechaPrestamo, Periodo periodo, int cantidadDePeriodos,
            DateTime FechaInicioPrimeraCuota)
        {
            var prestamo = new Prestamo();
            prestamo.FechaEmisionReal = fechaPrestamo;
            prestamo.Periodo = periodo;
            prestamo.CantidadDePeriodos = cantidadDePeriodos;
            prestamo.FechaInicioPrimeraCuota = FechaInicioPrimeraCuota;
            var fechaVencimiento = PrestamoConCalculos.CalcularFechaVencimiento(prestamo);
            return fechaVencimiento;
        }
        private static DateTime CalcularFechaVencimiento(Prestamo prestamo)
        {
            // primero buscar el periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            var duracion = prestamo.CantidadDePeriodos * prestamo.Periodo.MultiploPeriodoBase;
            var fechaVencimiento = new DateTime();
            if (prestamo.AcomodarFechaALasCuotas)
            {
                throw new Exception("aun no estamos trabajando con acomodar cuotas");
            }
            var fechaInicial = prestamo.FechaEmisionReal;
            switch (prestamo.Periodo.PeriodoBase)
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
            prestamo.FechaVencimiento = fechaVencimiento;

            return prestamo.FechaVencimiento;
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


        public async Task<PrestamoConCalculos> CalcularPrestamo()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.FechaVencimiento = this.Cuotas.Last().Fecha;
            return this;
        }

        public IEnumerable<CxCCuota> GenerarCuotas(IInfoGeneradorCuotas info)
        {
            var generadorCuotas = CuotasConCalculo.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            return cuotas;
        }

        public TasaInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            var result = new TasaInteresBLL(-1, "Luis Prueba").CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);
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

            // todo poner el calculo de tasa de interes por periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            var cuotas = GenerarCuotas(infoCuotas);
            //this.Cuotas.Clear();
            this.Cuotas = cuotas.ToList();
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());
        }
    }

}
