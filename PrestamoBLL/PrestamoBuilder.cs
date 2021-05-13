using PrestamoBLL.Entidades;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoBLL
{
    public class PrestamoBuilder
    {
        #region property members
        List<Cliente> clientes = new List<Cliente>();
        List<Codeudor> codeudores = new List<Codeudor>();
        List<Garantia> garantias = new List<Garantia>();
        List<Cuota> cuotas = new List<Cuota>();
        Prestamo prestamoInProgress = new Prestamo();
        Periodo periodo = new Periodo();
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
        #endregion property members

        public PrestamoBuilder(Prestamo prestamo)
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
            validarIdNoMenorNiIgualACero(prestamo.IdNegocio,"IdNegocio");
            prestamoInProgress.IdNegocio = prestamo.IdNegocio;
            prestamoInProgress.OtrosCargosSinInteres = prestamo.OtrosCargosSinInteres;
            SetFechaDeEmision(prestamo.FechaEmisionReal);
            SetClasificacion(prestamo.IdClasificacion);
            //SetAmortizacion(prestamo.TipoAmortizacion);
            SetAmortizacion(prestamo.IdTipoAmortizacion);
            SetRenovacion(prestamo.NumeroPrestamoARenovar, prestamo.IdPrestamoARenovar);
            SetClientes(prestamo.IdCliente);
            SetGarantias(prestamo.IdGarantias);
            SetCodeuDores(prestamo._Codeudores);
            SetMontoAPrestar(prestamo.MontoPrestado, prestamo.IdDivisa);
            SetGastDeCierre(prestamo.InteresGastoDeCierre, prestamo.GastoDeCierreEsDeducible, prestamo.FinanciarGastoDeCierre, prestamo.CargarInteresAlGastoDeCierre);
            SetTasaInteres(prestamo.IdTasaInteres);
            SetAcomodarFecha(prestamo.FechaInicioPrimeraCuota);
            SetPeriodoYDuracion(prestamo.IdPeriodo, prestamo.CantidadDePeriodos); ;
            SetMoras(prestamo.IdTipoMora);
        }
        public void AddCodeudor(Prestamo prestamo, Codeudor codeudor)
        {
            validarIdNoMenorNiIgualACero(codeudor.IdCodeudor, "IdCodeudor");
            if (codeudor.IdCodeudor > 0)
            {
                prestamo._Codeudores.Add(codeudor);
            }
            else
            {
                throw new NullReferenceException("el id del cliente a agregar no es valido debe ser mayor que cero");
            }
        }
        private void AddGarantia(Prestamo prestamo, Garantia garantia)
        {
            if (garantia.IdGarantia > 0)
            {
                prestamo._Garantias.Add(garantia);
            }
            else
            {
                throw new NullReferenceException("la garantia a agregar no es valida");
            }
            // verificar que la garantia no este en otro prestamo, siempre y cuando no tenga la opcion
            // indicada de que puede ser una garantia para varios prestamos.
            // pero debe es bueno que lo indique
            garantias.Add(garantia);
        }
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
            if (prestamoInProgress.IdPrestamoARenovar < 0  )
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
            var mora = BLLPrestamo.Instance.TiposMorasGet(new TipoMoraGetParams { IdNegocio = prestamoInProgress.IdNegocio, IdTipoMora = idTipoMora }).FirstOrDefault();
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
            var duracion = this.prestamoInProgress.CantidadDePeriodos * periodo.MultiploPeriodoBase;
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
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { IdNegocio = prestamoInProgress.IdNegocio, idPeriodo = idPeriodo }).FirstOrDefault();
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
            var tasaDeInteres = BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = prestamoInProgress.IdNegocio, idTasaInteres = idTasaDeInteres }).FirstOrDefault();

            this.prestamoInProgress.IdTasaInteres = idTasaDeInteres;
            this.prestamoInProgress.TasaDeInteresPorPeriodo = tasaDeInteres.InteresMensual;

        }

        private void SetGastDeCierre(decimal tasaGastoDeCierre, bool EsDeducible, bool sumargastoDeCierreALasCuotas, bool cargarInteresAlGastoDeCierre)
        {
            this.prestamoInProgress.InteresGastoDeCierre = tasaGastoDeCierre;
            prestamoInProgress.GastoDeCierreEsDeducible = EsDeducible;
            prestamoInProgress.CargarInteresAlGastoDeCierre = cargarInteresAlGastoDeCierre;
            prestamoInProgress.FinanciarGastoDeCierre = sumargastoDeCierreALasCuotas;
        }

        private void SetCodeuDores(List<Codeudor> codeudores)
        {
            prestamoInProgress.IdCodeudores = new List<int>();
            if (codeudores != null)
            {
                foreach (var item in codeudores)
                {
                    AddCodeudor(prestamoInProgress, item);
                    validarIdNoMenorNiIgualACero(item.IdCodeudor, "IdCodeudor");
                    prestamoInProgress.IdCodeudores.Add(item.IdCodeudor);
                }
            }
        }

        private void SetGarantias(List<Garantia> garantias)
        {
            prestamoInProgress.IdGarantias = new List<int>();
            if (garantias != null)
            {
                foreach (var item in garantias)
                {
                    AddGarantia(prestamoInProgress, item);
                    validarIdNoMenorNiIgualACero(item.IdGarantia, "IdGarantia");
                    prestamoInProgress.IdGarantias.Add(item.IdGarantia);
                }
            }
        }
        private void SetGarantias(IEnumerable<int> idGarantias)
        {
            // que las garantias en cuestion no tengan otros prestamos vigente
            var tienenPrestamosVigentes = BLLPrestamo.Instance.GarantiasTienenPrestamosVigentes(idGarantias);

            prestamoInProgress.IdGarantias = new List<int>();
            if (garantias != null)
            {
                foreach (var item in idGarantias)
                {
                    validarIdNoMenorNiIgualACero(item,"IdGarantia");
                    prestamoInProgress.IdGarantias.Add(item);
                }
            }
        }
        private void SetCodeudores(List<Codeudor> codeudores)
        {
            foreach (var item in codeudores)
            {
                AddCodeudor(prestamoInProgress, item);
                prestamoInProgress.IdCodeudores.Add(item.IdCodeudor);
            }
        }
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
                    generadorCuotas= new GeneradorCuotasFijasNoAmortizable(info);
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
        private Prestamo Build2()
        {
            IGeneradorCuotas genCuotas = GetGeneradorCuotas();
            // prestamoInProgress._Cuotas = genCuotas.GenerarCuotas();
            return prestamoInProgress;
            //var prestamoConDependencias = new PrestamoInsUpdParam(prestamoInProgress,cuotas: cuotas, prestamoInProgress.Codeudores, prestamoInProgress.Garantias);
            //return prestamoConDependencias;
        }
        public PrestamoInsUpdParam Build()
        {
            IGeneradorCuotas genCuotas = GetGeneradorCuotas();
            var cuotas = genCuotas.GenerarCuotas();
            // var cuotasVacias = new List<Cuota>();
            var prestamoConDependencias = new PrestamoInsUpdParam(prestamoInProgress,  cuotas,  prestamoInProgress._Codeudores, prestamoInProgress._Garantias);
            _type.CopyPropertiesTo(prestamoInProgress, prestamoConDependencias);
            return prestamoConDependencias;
        }
    }
}
