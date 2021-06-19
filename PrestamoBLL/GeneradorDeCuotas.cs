using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class GeneradorCuotasFijasNoAmortizable : IGeneradorCuotas
    {
        internal readonly IInfoGeneradorCuotas infoGenerarCuotas;
        internal readonly Periodo periodo;

        DateTime fechaCuotaAnterior = InitValues._19000101;
        List<Cuota> cuotas = new List<Cuota>();
        public GeneradorCuotasFijasNoAmortizable(IInfoGeneradorCuotas info)
        {
            if (info.TipoAmortizacion != TiposAmortizacion.No_Amortizable_cuotas_fijas)
            { throw new InvalidOperationException("para generar este tipo de cuotas, solo se admite el tipo de amortizacion No Amortizable Cuota Fija"); }
            infoGenerarCuotas = info;
            periodo = info.Periodo;
        }

        public List<Cuota> GenerarCuotas()
        {
            this.cuotas.Clear();
            GastoDeCierreSinFinanciamiento();
            decimal capitalPorCuota = getCapitalPorCuota();
            decimal interesPorCuota = getInteresPorCuota();
            decimal otrosCargosSinInteres = getOtrosCargosSinInteresPorCuota();
            this.fechaCuotaAnterior = infoGenerarCuotas.FechaEmisionReal;
            // Como se manejara el gasto de cierre aqui
            // que seran las cuotas realmente en este sistema
            // sera algo mas abierto como cuentas por cobrar de monica que manejara lo que es un doc_cc
            // que permite cualquier cosa y que establezca un tipo que sea cuotaPrestamo
            // y para los demas casos otros tipos segun la necesidad. analizarlo, pero iniciar como ahora
            // el sistema con cuotas o si a lass cuotas se le pondra alguna informacion que detalle otros casos
            if (!infoGenerarCuotas.ProyectarPrimeraYUltima)
            {
                for (int i = 1; i <= infoGenerarCuotas.CantidadDePeriodos; i++)
                {
                    var cuota = new Cuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = i, OtrosCargos = otrosCargosSinInteres };
                    setGastoDeCierreFinaciadoEnCuotas(cuota);
                    cuota.Fecha = getFecha(i);
                    this.fechaCuotaAnterior = cuota.Fecha;
                    cuotas.Add(cuota);
                }
                ajustarValores();
            }
            else
            {

                var cuota = new Cuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = 1, OtrosCargos = otrosCargosSinInteres };
                setGastoDeCierreFinaciadoEnCuotas(cuota);
                cuota.Fecha = getFecha(1);
                this.fechaCuotaAnterior = cuota.Fecha;
                cuota.Comentario = "Primera cuota";
                cuotas.Add(cuota);
                if (infoGenerarCuotas.CantidadDePeriodos > 1)
                {
                    cuota = new Cuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = infoGenerarCuotas.CantidadDePeriodos, OtrosCargos = otrosCargosSinInteres };
                    setGastoDeCierreFinaciadoEnCuotas(cuota);
                    cuota.Fecha = PrestamoConCalculos.CalcularFechaVencimiento(this.infoGenerarCuotas.FechaEmisionReal, infoGenerarCuotas.Periodo, infoGenerarCuotas.CantidadDePeriodos, infoGenerarCuotas.FechaInicioPrimeraCuota);
                    cuota.Comentario = "Ultima Cuota";
                    cuotas.Add(cuota);
                }
                //ajustarValores();
            }
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


            decimal ajusteCapital = infoGenerarCuotas.MontoCapital - totalCapitalCuotas;
            decimal ajusteOtrosCargosSinInteres = infoGenerarCuotas.OtrosCargosSinInteres - totalOtrosCargosSinInteresCuotas;
            decimal ajusteGastoDeCierre = 0;
            if (!infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                ajusteGastoDeCierre = infoGenerarCuotas.MontoGastoDeCierre - totalGastoDeCierreCuotas;
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
            if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) && (!this.infoGenerarCuotas.FinanciarGastoDeCierre) && (!infoGenerarCuotas.GastoDeCierreEsDeducible))
            {
                var cuota = new Cuota { GastoDeCierre = this.infoGenerarCuotas.MontoGastoDeCierre, Fecha = this.infoGenerarCuotas.FechaEmisionReal, Numero = 0 };
                cuota.Comentario = "Gasto De Cierre a pagar";
                this.cuotas.Add(cuota);
            }
        }

        private DateTime getFecha(int i)
        {
            if (i == 1 && infoGenerarCuotas.AcomodarFechaALasCuotas)
            {
                this.fechaCuotaAnterior = Convert.ToDateTime(infoGenerarCuotas.FechaInicioPrimeraCuota);
                return this.fechaCuotaAnterior;
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

        private void setGastoDeCierreFinaciadoEnCuotas(Cuota cuota)
        {
            if (this.infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                return;
            }

            if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) &&
               (this.infoGenerarCuotas.FinanciarGastoDeCierre))
            {
                cuota.GastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre / this.infoGenerarCuotas.CantidadDePeriodos);
                // ahora calculamos el interes del gasto de cierre si debe cargarlo
                if (this.infoGenerarCuotas.CargarInteresAlGastoDeCierre)
                {
                    cuota.InteresDelGastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre * (this.infoGenerarCuotas.TasaDeInteresDelPeriodo / 100), 2);
                }
            }
        }

        private decimal getInteresPorCuota()
        {
            var tasaInteresPorPeriodo = infoGenerarCuotas.TasaDeInteresDelPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal interesPorCuota = Math.Round(infoGenerarCuotas.MontoCapital * (tasaInteresPorPeriodo / 100), 2);
            return interesPorCuota;
        }
        private decimal getOtrosCargosSinInteresPorCuota()
        {
            var tasaInteresPorPeriodo = infoGenerarCuotas.TasaDeInteresDelPeriodo;
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal otrosCargosSininteresPorCuota = Math.Round(infoGenerarCuotas.OtrosCargosSinInteres / infoGenerarCuotas.CantidadDePeriodos, 2);
            return otrosCargosSininteresPorCuota;
        }
        private decimal getCapitalPorCuota()
        {
            var capitalPorCuota = Math.Round(infoGenerarCuotas.MontoCapital / infoGenerarCuotas.CantidadDePeriodos, 2);
            return capitalPorCuota;
        }

    }

    
}

