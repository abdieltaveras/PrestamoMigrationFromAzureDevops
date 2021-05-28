using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class GeneradorCuotasFijasNoAmortizable : IGeneradorCuotas
    {
        internal readonly IInfoGeneradorCuotas fuente;
        internal readonly Periodo periodo;
        DateTime fechaCuotaAnterior = InitValues._19000101;
        List<Cuota> cuotas = new List<Cuota>();
        public GeneradorCuotasFijasNoAmortizable(IInfoGeneradorCuotas info)
        {
            if (info.TipoAmortizacion != TiposAmortizacion.No_Amortizable_cuotas_fijas)
            { throw new InvalidOperationException("para generar este tipo de cuotas, solo se admite el tipo de amortizacion No Amortizable Cuota Fija"); }
            fuente = info;
            periodo = info.Periodo;
            
        }

        public List<Cuota> GenerarCuotas()
        {
            this.cuotas.Clear();
            GastoDeCierreSinFinanciamiento();
            decimal capitalPorCuota = getCapitalPorCuota();
            decimal interesPorCuota = getInteresPorCuota();
            decimal otrosCargosSinInteres = getOtrosCargosSinInteresPorCuota();
            this.fechaCuotaAnterior = fuente.FechaEmisionReal;
            // Como se manejara el gasto de cierre aqui
            // que seran las cuotas realmente en este sistema
            // sera algo mas abierto como cuentas por cobrar de monica que manejara lo que es un doc_cc
            // que permite cualquier cosa y que establezca un tipo que sea cuotaPrestamo
            // y para los demas casos otros tipos segun la necesidad. analizarlo, pero iniciar como ahora
            // el sistema con cuotas o si a lass cuotas se le pondra alguna informacion que detalle otros casos
            for (int i = 1; i <= fuente.CantidadDePeriodos; i++)
            {
                var cuota = new Cuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = i, OtrosCargos = otrosCargosSinInteres };
                setGastoDeCierreFinaciadoEnCuotas(i, cuota);
                cuota.Fecha = getFecha(i);
                this.fechaCuotaAnterior = cuota.Fecha;
                cuotas.Add(cuota);
            }
            ajustarValores();
            return cuotas;
        }

        /// <summary>
        /// revisa si la sumatoria de los valores de las cuotas tienen alguna diferencia y lo ajustan
        /// ejemplo si de capital debe ser 1000 y la sumatoria de las cuotas da 998.75
        /// el sistema a la ultima cuota le hace un ajuste
        /// </summary>
        private void ajustarValores()
        {
            decimal totalCapitalCuotas = 0;
            decimal totalOtrosCargosSinInteresCuotas = 0;
            decimal totalGastoDeCierreCuotas = 0;

            foreach (var cuota in cuotas)
            {
                totalCapitalCuotas += cuota.Capital ?? 0;
                totalOtrosCargosSinInteresCuotas += (decimal)cuota.OtrosCargos;
                totalGastoDeCierreCuotas += (decimal)cuota.GastoDeCierre;
            }


            decimal ajusteCapital = fuente.MontoCapital - totalCapitalCuotas;
            decimal ajusteOtrosCargosSinInteres = fuente.OtrosCargosSinInteres - totalOtrosCargosSinInteresCuotas;
            decimal ajusteGastoDeCierre = 0;
            if (!fuente.GastoDeCierreEsDeducible)
            {
                ajusteGastoDeCierre = fuente.MontoGastoDeCierre - totalGastoDeCierreCuotas;
            }
            var ultimaCuotaAjustada = cuotas[cuotas.Count() - 1];

            if (ajusteCapital != 0)
            {
                ultimaCuotaAjustada.Capital += ajusteCapital;
            }

            if (ajusteOtrosCargosSinInteres != 0)
            {
                ultimaCuotaAjustada.OtrosCargos += ajusteOtrosCargosSinInteres;
            }

            if (ajusteGastoDeCierre != 0)
            {
                ultimaCuotaAjustada.GastoDeCierre += ajusteGastoDeCierre;
            }

            cuotas[cuotas.Count() - 1] = ultimaCuotaAjustada;
        }

        private void GastoDeCierreSinFinanciamiento()
        {
            if ((this.fuente.MontoGastoDeCierre > 0) && (!this.fuente.FinanciarGastoDeCierre) && (!fuente.GastoDeCierreEsDeducible)) 
            {
                var cuota = new Cuota { GastoDeCierre = this.fuente.MontoGastoDeCierre, Fecha = this.fuente.FechaEmisionReal, Numero = 0 };
                this.cuotas.Add(cuota);
            }
        }

        private DateTime getFecha(int i)
        {
            if (i == 1 && fuente.AcomodarFechaALasCuotas)
            {
                this.fechaCuotaAnterior = fuente.FechaInicioPrimeraCuota;
                return this.fuente.FechaInicioPrimeraCuota;
            }
            DateTime fechaCuota = calcFecha();
            return fechaCuota;
        }

        private DateTime calcFecha()
        {
            var fecha = new DateTime();
            switch (periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fecha = this.fechaCuotaAnterior.AddDays(1 * periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Semana:
                    fecha = this.fechaCuotaAnterior.AddDays(7 * periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Quincena:
                    fecha = this.fechaCuotaAnterior;
                    for (int periodoCuota = 1; periodoCuota <= periodo.MultiploPeriodoBase; periodoCuota++)
                    {
                        if (periodoCuota % 2 == 0)
                        {
                            fecha = fecha.AddMonths(periodoCuota / 2);
                        }
                        else
                        {
                            fecha = fecha.AddDays(15);
                        }
                    }
                    break;
                case PeriodoBase.Mes:
                    fecha = this.fechaCuotaAnterior.AddMonths(1 * periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Ano:
                    fecha = this.fechaCuotaAnterior.AddYears(1 * periodo.MultiploPeriodoBase);
                    break;
                default:
                    break;
            }

            return fecha;
        }

        private void setGastoDeCierreFinaciadoEnCuotas(int index, Cuota cuota)
        {
            if (this.fuente.GastoDeCierreEsDeducible)
            {
                return;
            }

            if ((this.fuente.MontoGastoDeCierre > 0) &&
               (this.fuente.FinanciarGastoDeCierre))
            {
                cuota.GastoDeCierre = Math.Round(this.fuente.MontoGastoDeCierre / this.fuente.CantidadDePeriodos);
                // ahora calculamos el interes del gasto de cierre si debe cargarlo
                if (this.fuente.CargarInteresAlGastoDeCierre)
                {
                    cuota.InteresDelGastoDeCierre = Math.Round(this.fuente.MontoGastoDeCierre * (this.fuente.TasaDeInteresPorPeriodo / 100), 2);
                }
            }
        }

        private decimal getInteresPorCuota()
        {
            var tasaInteresPorPeriodo = fuente.TasaDeInteresPorPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal interesPorCuota = Math.Round(fuente.MontoCapital * (tasaInteresPorPeriodo / 100), 2);
            return interesPorCuota;
        }
        private decimal getOtrosCargosSinInteresPorCuota()
        {
            var tasaInteresPorPeriodo = fuente.TasaDeInteresPorPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal otrosCargosSininteresPorCuota = Math.Round(fuente.OtrosCargosSinInteres / fuente.CantidadDePeriodos, 2);
            return otrosCargosSininteresPorCuota;
        }
        private decimal getCapitalPorCuota()
        {
            var capitalPorCuota = Math.Round(fuente.MontoCapital / fuente.CantidadDePeriodos, 2);
            return capitalPorCuota;
        }

    }

    public class infoGeneradorDeCuotas : IInfoGeneradorCuotas
    {
        public TiposAmortizacion TipoAmortizacion { get; set; }

        public DateTime FechaEmisionReal { get; set; } = InitValues._19000101;


        public decimal MontoCapital { get; set; } = 0;

        public decimal MontoGastoDeCierre { get; set; } = 0;

        public bool CargarInteresAlGastoDeCierre { get; set; } = false;

        public int CantidadDePeriodos { get; set; } = 0;

        public bool AcomodarFechaALasCuotas { get; set; } = false;

        public DateTime FechaInicioPrimeraCuota { get; set; } = InitValues._19000101;

        public decimal TasaDeInteresPorPeriodo { get; set; } = 0;

        public Periodo Periodo { get; set; }

        public bool FinanciarGastoDeCierre { get; set; }
        public decimal OtrosCargosSinInteres { get; set; }
        public bool GastoDeCierreEsDeducible { get; set; }

        public infoGeneradorDeCuotas(TiposAmortizacion tipoAMortizacion)
        {
            this.TipoAmortizacion = TipoAmortizacion;
        }
        public infoGeneradorDeCuotas(Prestamo prestamo) {

            {

                AcomodarFechaALasCuotas = prestamo.AcomodarFechaALasCuotas;
                CantidadDePeriodos = prestamo.CantidadDePeriodos;
                MontoCapital = prestamo.MontoCapital;
                FechaEmisionReal = prestamo.FechaEmisionReal;
                FechaInicioPrimeraCuota = prestamo.FechaInicioPrimeraCuota;
                CargarInteresAlGastoDeCierre = prestamo.CargarInteresAlGastoDeCierre;
                FinanciarGastoDeCierre = prestamo.FinanciarGastoDeCierre;
                MontoGastoDeCierre = prestamo.MontoGastoDeCierre;
                OtrosCargosSinInteres = prestamo.OtrosCargosSinInteres;
                GastoDeCierreEsDeducible = prestamo.GastoDeCierreEsDeducible;
                TipoAmortizacion = prestamo.TipoAmortizacion;
                TasaDeInteresPorPeriodo = prestamo.TasaDeInteresPorPeriodo;
                Periodo = prestamo.Periodo;
            };
        }
    }
    public interface IInfoGeneradorCuotas
    {
        TiposAmortizacion TipoAmortizacion { get; }
        DateTime FechaEmisionReal { get; }
        decimal MontoCapital { get; }

        decimal MontoGastoDeCierre { get; }
        decimal OtrosCargosSinInteres { get; }
        bool CargarInteresAlGastoDeCierre { get; }

        bool GastoDeCierreEsDeducible { get; }

        bool FinanciarGastoDeCierre { get; }
        int CantidadDePeriodos { get; }
        bool AcomodarFechaALasCuotas { get; }
        DateTime FechaInicioPrimeraCuota { get; }
        decimal TasaDeInteresPorPeriodo { get; }
        Periodo Periodo { get; }
    }
}

