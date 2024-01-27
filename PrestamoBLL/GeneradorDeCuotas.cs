using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{


    public class GeneradorCuotasFijasNoAmortizable : IGeneradorCuotas
    {
        internal readonly IInfoGeneradorCuotas infoGenerarCuotas;
        private Periodo Periodo { get; set; }
        private TasaInteres TasaInteres { get; set; }
        private TasasInteresPorPeriodos TasasInteresDelPeriodo {get;set;}

        DateTime fechaCuotaAnterior = InitValues._19000101;
        List<CxCCuota> cuotas = new List<CxCCuota>();
        IEnumerable<IMaestroDebitoSinDetallesCxC> CuotasMaestroDetalle = new List<CuotaMaestro>();
        public GeneradorCuotasFijasNoAmortizable(IInfoGeneradorCuotas info)
        {
            if (info.TipoAmortizacion != TiposAmortizacion.No_Amortizable_cuotas_fijas)
            { throw new InvalidOperationException("para generar este tipo de cuotas, solo se admite el tipo de amortizacion No Amortizable CuotaMaestro Fija"); }
            BLLValidations.ValueGreaterThanZero(info.IdPeriodo, "IdPeriodo");
            BLLValidations.ValueGreaterThanZero(info.IdTasaInteres, "IdTasaInteres");
            infoGenerarCuotas = info;
        }

        private Periodo GetPeriodo()
        {
                BLLValidations.ValueGreaterThanZero(infoGenerarCuotas.IdPeriodo, "IdPeriodo");
                BLLValidations.ValueGreaterThanZero(infoGenerarCuotas.CantidadDeCuotas, "cantidad de cuotas");
                var periodo = new PeriodoBLL(infoGenerarCuotas.IdLocalidadNegocio, infoGenerarCuotas.Usuario).GetPeriodos(new PeriodoGetParams { idPeriodo = infoGenerarCuotas.IdPeriodo }).FirstOrDefault();
                if (!periodo.Activo)
                {
                    throw new Exception("El Periodo indicado no es valido porque no esta activo");
                }
                if (periodo.Anulado())
                {
                    throw new Exception("El Periodo indicado no es valido porque ha sido anulado");
                }
                this.Periodo = periodo;
                return periodo;
        }

        private void GetTasaInteresPorPeriodo()
        {
            this.TasasInteresDelPeriodo = new TasaInteresBLL(infoGenerarCuotas.IdLocalidadNegocio, infoGenerarCuotas.Usuario).CalcularTasaInteresPorPeriodos(this.TasaInteres.InteresMensual, this.Periodo);
        }

        private void GetTasaDeInteres()
        {
            BLLValidations.ValueGreaterThanZero(infoGenerarCuotas.IdTasaInteres, "IdTasaInteres");
            this.TasaInteres = new TasaInteresBLL(infoGenerarCuotas.IdLocalidadNegocio, infoGenerarCuotas.Usuario).GetTasasDeInteres(new TasaInteresGetParams { idTasaInteres = infoGenerarCuotas.IdTasaInteres }).FirstOrDefault();
        }

        public List<CxCCuota> GenerarCuotas()
        {
            this.cuotas.Clear();
            GastoDeCierreSinFinanciamiento();
            GetPeriodo();
            GetTasaDeInteres();
            GetTasaInteresPorPeriodo();
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
                for (int i = 1; i <= infoGenerarCuotas.CantidadDeCuotas; i++)
                {
                    var cuota = new CxCCuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = i };
                    setGastoDeCierreFinaciadoEnCuotas(cuota);
                    cuota.Fecha = getFecha(i);
                    this.fechaCuotaAnterior = cuota.Fecha;
                    cuotas.Add(cuota);
                }
                ajustarValores();
            }
            else
            {

                var cuota = new CxCCuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = 1 };
                setGastoDeCierreFinaciadoEnCuotas(cuota);
                cuota.Fecha = getFecha(1);
                this.fechaCuotaAnterior = cuota.Fecha;
                cuota.Comentario = "Primera cuota";
                cuotas.Add(cuota);
                if (infoGenerarCuotas.CantidadDeCuotas > 1)
                {
                    cuota = new CxCCuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = infoGenerarCuotas.CantidadDeCuotas };
                    setGastoDeCierreFinaciadoEnCuotas(cuota);
                    var fechaParaCuotas = infoGenerarCuotas.AcomodarFechaALasCuotas ? infoGenerarCuotas.FechaInicioPrimeraCuota : infoGenerarCuotas.FechaEmisionReal;
                    cuota.Fecha = PrestamoConCalculos.CalcularFechaVencimiento(infoGenerarCuotas.AcomodarFechaALasCuotas,Periodo, fechaParaCuotas,  infoGenerarCuotas.CantidadDeCuotas);
                    cuota.Comentario = "Ultima CuotaMaestro";
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
                totalCapitalCuotas += cuota.Capital;
                totalGastoDeCierreCuotas += (decimal)cuota.GastoDeCierre;
            }


            decimal ajusteCapital = infoGenerarCuotas.MontoCapital - totalCapitalCuotas;
            decimal ajusteOtrosCargosSinInteres = infoGenerarCuotas.OtrosCargos - totalOtrosCargosSinInteresCuotas;
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
                var cuota = new CxCCuota { GastoDeCierre = this.infoGenerarCuotas.MontoGastoDeCierre, Fecha = this.infoGenerarCuotas.FechaEmisionReal, Numero = 0 };
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
            switch (Periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fecha = this.fechaCuotaAnterior.AddDays(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Semana:
                    fecha = this.fechaCuotaAnterior.AddDays(7 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Quincena:
                    fecha = this.fechaCuotaAnterior;
                    for (int periodoCuota = 1; periodoCuota <= (int)Periodo.MultiploPeriodoBase; periodoCuota++)
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
                    fecha = this.fechaCuotaAnterior.AddMonths(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Ano:
                    fecha = this.fechaCuotaAnterior.AddYears(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                default:
                    break;
            }

            return fecha;
        }

        private void setGastoDeCierreFinaciadoEnCuotas(CxCCuota cuota)
        {
            if (this.infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                return;
            }

            if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) &&
               (this.infoGenerarCuotas.FinanciarGastoDeCierre))
            {
                cuota.GastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre / this.infoGenerarCuotas.CantidadDeCuotas);
                // ahora calculamos el interes del gasto de cierre si debe cargarlo
                if (this.infoGenerarCuotas.CargarInteresAlGastoDeCierre)
                {
                    cuota.InteresDelGastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre * (this.TasasInteresDelPeriodo.InteresDelPeriodo / 100), 2);
                }
            }
        }

        
        private decimal getInteresPorCuota()
        {
            
            
            decimal interesPorCuota = Math.Round(infoGenerarCuotas.MontoCapital * (this.TasasInteresDelPeriodo.InteresDelPeriodo / 100), 2);
            return interesPorCuota;
        }
        private decimal getOtrosCargosSinInteresPorCuota()
        {
            
            // empezaremos pensando en que no tiene interes el gasto de cierre
            // ni tampoco los otros gastos
            decimal otrosCargosSininteresPorCuota = Math.Round(infoGenerarCuotas.OtrosCargos / infoGenerarCuotas.CantidadDeCuotas, 2);
            return otrosCargosSininteresPorCuota;
        }
        private decimal getCapitalPorCuota()
        {
            var capitalPorCuota = Math.Round(infoGenerarCuotas.MontoCapital / infoGenerarCuotas.CantidadDeCuotas, 2);
            return capitalPorCuota;
        }
        // seccion para nuevos metodos aplicando la nueva forma de generar cuotas

        private decimal GetGastoDeCierreSinFinanciamiento()
        {
            decimal gastoDeCierre = 0;
            if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) && (!this.infoGenerarCuotas.FinanciarGastoDeCierre) && (!infoGenerarCuotas.GastoDeCierreEsDeducible))
            {
                gastoDeCierre = this.infoGenerarCuotas.MontoGastoDeCierre;
            }
            return gastoDeCierre;
        }
        private Tuple<decimal, decimal> GetGastoDeCierreAndInteresDelGastoDeCierreFinaciadoEnCuotas()
        {
            decimal gastoDeCierre = 0;
            decimal interesDelGastoDeCierre = 0;
            if (this.infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                return new Tuple<decimal, decimal>(0, 0);
            }

            if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) &&
               (this.infoGenerarCuotas.FinanciarGastoDeCierre))
            {
                gastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre / this.infoGenerarCuotas.CantidadDeCuotas);
                // ahora calculamos el interes del gasto de cierre si debe cargarlo
                if (this.infoGenerarCuotas.CargarInteresAlGastoDeCierre)
                {
                    interesDelGastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre * (this.TasasInteresDelPeriodo.InteresDelPeriodo / 100), 2);
                }
            }

            return new Tuple<decimal, decimal>(gastoDeCierre, interesDelGastoDeCierre);
        }
        public IEnumerable<IMaestroDebitoConDetallesCxC> GenerarCuotasMaestroYDetalle()
        {
            var cuotasMaestroDetalle = new List<IMaestroDebitoConDetallesCxC>();
            GetPeriodo();
            GetTasaDeInteres();
            GetTasaInteresPorPeriodo();
            var gastoDeCierreSinFinanciamiento = GetGastoDeCierreSinFinanciamiento();
            var gastoDeCierreConFinanciamiento = GetGastoDeCierreAndInteresDelGastoDeCierreFinaciadoEnCuotas();
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
                for (int i = 1; i <= infoGenerarCuotas.CantidadDeCuotas; i++)
                {
                    var cuota = new CuotaCxC().CreateCuotaAndDetalle(getFecha(i), i, capitalPorCuota, interesPorCuota, gastoDeCierreConFinanciamiento.Item1, gastoDeCierreConFinanciamiento.Item2);
                     cuotasMaestroDetalle.Add(cuota);
                }

                //ajustarValores();
            }
            else
            {
                var cuota = new CxCCuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = 1 };
                setGastoDeCierreFinaciadoEnCuotas(cuota);
                cuota.Fecha = getFecha(1);
                this.fechaCuotaAnterior = cuota.Fecha;
                cuota.Comentario = "Primera cuota";
                cuotas.Add(cuota);
                if (infoGenerarCuotas.CantidadDeCuotas > 1)
                {
                    cuota = new CxCCuota { Capital = capitalPorCuota, Interes = interesPorCuota, Numero = infoGenerarCuotas.CantidadDeCuotas };
                    setGastoDeCierreFinaciadoEnCuotas(cuota);
                    var fechaParaCuotas = infoGenerarCuotas.AcomodarFechaALasCuotas ? infoGenerarCuotas.FechaInicioPrimeraCuota : infoGenerarCuotas.FechaEmisionReal;
                    cuota.Fecha = PrestamoConCalculos.CalcularFechaVencimiento(infoGenerarCuotas.AcomodarFechaALasCuotas, Periodo, fechaParaCuotas, infoGenerarCuotas.CantidadDeCuotas);
                    cuota.Comentario = "Ultima CuotaMaestro";
                    cuotas.Add(cuota);
                }
                //ajustarValores();
            }
            return cuotasMaestroDetalle;
        }
    }

    
}

