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
                var cuota = new Cuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = i, OtrosCargosSinInteres = otrosCargosSinInteres };
                setGastoDeCierreFinaciadoEnCuotas(i, cuota);
                cuota.Fecha = getFecha(i);
                this.fechaCuotaAnterior = cuota.Fecha;
                cuotas.Add(cuota);
            }
            return cuotas;
        }

        private void GastoDeCierreSinFinanciamiento()
        {
            if ((this.fuente.MontoGastoDeCierre > 0) && (!this.fuente.FinanciarGastoDeCierre))
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

        private decimal getOtrosGastosSinInteresPorCuota()
        {
            return 0;
        }

        private void setGastoDeCierreFinaciadoEnCuotas(int index, Cuota cuota)
        {
            if ((this.fuente.MontoGastoDeCierre > 0) &&
               (this.fuente.FinanciarGastoDeCierre))
            {
                cuota.GastoDeCierre = this.fuente.MontoGastoDeCierre / this.fuente.CantidadDePeriodos;
                if (this.fuente.CargarInteresAlGastoDeCierre)
                {
                    cuota.InteresDelGastoDeCierre = this.fuente.MontoGastoDeCierre *(this.fuente.TasaDeInteresPorPeriodo/100);
                }
            }
        }

        private decimal getInteresPorCuota()
        {
            var tasaInteresPorPeriodo = fuente.TasaDeInteresPorPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal interesPorCuota = fuente.MontoCapital * (tasaInteresPorPeriodo / 100);
            return interesPorCuota;
        }
        private decimal getOtrosCargosSinInteresPorCuota()
        {
            var tasaInteresPorPeriodo = fuente.TasaDeInteresPorPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal otrosCargosSininteresPorCuota = fuente.OtrosCargosSinInteres / fuente.CantidadDePeriodos;
            return otrosCargosSininteresPorCuota;
        }
        private decimal getCapitalPorCuota()
        {
            var capitalPorCuota = fuente.MontoCapital / fuente.CantidadDePeriodos;
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

        public infoGeneradorDeCuotas(TiposAmortizacion tipoAMortizacion)
        {
            this.TipoAmortizacion = TipoAmortizacion;
        }
        public infoGeneradorDeCuotas() { }
    }
    public interface IInfoGeneradorCuotas
    {
        TiposAmortizacion TipoAmortizacion { get; }
        DateTime FechaEmisionReal { get; }
        decimal MontoCapital { get; }

        decimal MontoGastoDeCierre { get; }
        decimal OtrosCargosSinInteres { get; }
        bool CargarInteresAlGastoDeCierre { get; }

        bool FinanciarGastoDeCierre { get; }
        int CantidadDePeriodos { get; }
        bool AcomodarFechaALasCuotas { get; }
        DateTime FechaInicioPrimeraCuota { get; }
        decimal TasaDeInteresPorPeriodo { get; }
        Periodo Periodo { get; }
    }
}

