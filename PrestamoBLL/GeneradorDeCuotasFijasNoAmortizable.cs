using PrestamoBLL.Models;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{


    internal class GeneradorCuotasFijasNoAmortizable  :IGeneradorCuotas
    {
        internal readonly IInfoGeneradorCuotas infoGenerarCuotas;
        private Periodo Periodo { get; set; }
        private TasaInteres TasaInteres { get; set; }
        private TasasInteresPorPeriodos TasasInteresDelPeriodo {get;set;}

        private DateTime FechaCuotaAnterior = InitValues._19000101;
        
        private IEnumerable<IMaestroDebitoConDetallesCxC> CuotasMaestroDetalle = new List<MaestroDrConDetalles>();
        public GeneradorCuotasFijasNoAmortizable(IInfoGeneradorCuotas info)
        {
            if (info.TipoAmortizacion != TiposAmortizacion.No_Amortizable_cuotas_fijas)
            { throw new InvalidOperationException("para generar este tipo de cuotas, solo se admite el tipo de amortizacion No Amortizable MaestroDrConDetalles Fija"); }
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

            //foreach (var cuota in cuotas)
            //{
            //    totalCapitalCuotas += cuota.Capital;
            //    totalGastoDeCierreCuotas += (decimal)cuota.GastoDeCierre;
            //}


            decimal ajusteCapital = infoGenerarCuotas.MontoCapital - totalCapitalCuotas;
            decimal ajusteOtrosCargosSinInteres = infoGenerarCuotas.OtrosCargos - totalOtrosCargosSinInteresCuotas;
            decimal ajusteGastoDeCierre = 0;
            if (!infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                ajusteGastoDeCierre = infoGenerarCuotas.MontoGastoDeCierre - totalGastoDeCierreCuotas;
            };
            //var ultimaCuotaAjustada = cuotas[cuotas.Count() - 1];

            //if (ajusteCapital != 0)
            //{
            //    ultimaCuotaAjustada.Capital += ajusteCapital;
            //}

            //if (ajusteGastoDeCierre != 0)
            //{
            //    ultimaCuotaAjustada.GastoDeCierre += ajusteGastoDeCierre;
            //}

            //cuotas[cuotas.Count() - 1] = ultimaCuotaAjustada;
        }

        //private void GastoDeCierreSinFinanciamiento()
        //{
        //    if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) && (!this.infoGenerarCuotas.FinanciarGastoDeCierre) && (!infoGenerarCuotas.GastoDeCierreEsDeducible))
        //    {
        //        var cuota = new CxCCuota { GastoDeCierre = this.infoGenerarCuotas.MontoGastoDeCierre, Fecha = this.infoGenerarCuotas.FechaEmisionReal, Numero = 0 };
        //        cuota.Comentario = "Gasto De Cierre a pagar";
        //        this.cuotas.Add(cuota);
        //    }
        //}

        

        private DateTime getFecha(int i)
        {
            if (i == 1 && infoGenerarCuotas.AcomodarFechaALasCuotas)
            {
                this.FechaCuotaAnterior = Convert.ToDateTime(infoGenerarCuotas.FechaInicioPrimeraCuota);
                return this.FechaCuotaAnterior;
            }
            DateTime fechaCuota = calcFecha();
            this.FechaCuotaAnterior = fechaCuota;
            return fechaCuota;
        }

        private DateTime calcFecha()
        {
            var fecha = new DateTime();
            switch (Periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fecha = this.FechaCuotaAnterior.AddDays(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Semana:
                    fecha = this.FechaCuotaAnterior.AddDays(7 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Quincena:
                    fecha = this.FechaCuotaAnterior;
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
                    fecha = this.FechaCuotaAnterior.AddMonths(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                case PeriodoBase.Ano:
                    fecha = this.FechaCuotaAnterior.AddYears(1 * (int)Periodo.MultiploPeriodoBase);
                    break;
                default:
                    break;
            }
            this.FechaCuotaAnterior = fecha;
            return fecha;
        }

        //private void setGastoDeCierreFinaciadoEnCuotas(CxCCuota cuota)
        //{
        //    if (this.infoGenerarCuotas.GastoDeCierreEsDeducible)
        //    {
        //        return;
        //    }

        //    if ((this.infoGenerarCuotas.MontoGastoDeCierre > 0) &&
        //       (this.infoGenerarCuotas.FinanciarGastoDeCierre))
        //    {
        //        cuota.GastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre / this.infoGenerarCuotas.CantidadDeCuotas);
        //        // ahora calculamos el interes del gasto de cierre si debe cargarlo
        //        if (this.infoGenerarCuotas.CargarInteresAlGastoDeCierre)
        //        {
        //            cuota.InteresDelGastoDeCierre = Math.Round(this.infoGenerarCuotas.MontoGastoDeCierre * (this.TasasInteresDelPeriodo.InteresDelPeriodo / 100), 2);
        //        }
        //    }
        //}

        
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

        public class GastoDeCierreEnCuotas

        {
            public decimal MontoSinInteres { get; set; }

            public decimal InteresGC { get; set; }
        }
        private GastoDeCierreEnCuotas GetGastoDeCierreAndInteresDelGastoDeCierreFinaciadoEnCuotas()
        {
            decimal gastoDeCierre = 0;
            decimal interesDelGastoDeCierre = 0;
            if (this.infoGenerarCuotas.GastoDeCierreEsDeducible)
            {
                return new GastoDeCierreEnCuotas();
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

            return new GastoDeCierreEnCuotas
            { MontoSinInteres = gastoDeCierre, InteresGC = interesDelGastoDeCierre };
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
            this.FechaCuotaAnterior = infoGenerarCuotas.FechaEmisionReal;
            if (!infoGenerarCuotas.ProyectarPrimeraYUltima)
            {
                
                if (!this.infoGenerarCuotas.FinanciarGastoDeCierre && infoGenerarCuotas.MontoGastoDeCierre>0)
                {
                    var cargo = new CargoGastoCierreSinFinanciamientoBuilder().CreateCargoAndDetalle(this.infoGenerarCuotas.FechaEmisionReal, this.infoGenerarCuotas.MontoGastoDeCierre);
                    cuotasMaestroDetalle.Add(cargo);
                }

                for (int i = 1; i <= infoGenerarCuotas.CantidadDeCuotas; i++)
                {
                    
                    var cuota = new CuotaPrestamoBuilder().CreateCuotaAndDetalle(getFecha(i) , i, capitalPorCuota, interesPorCuota, gastoDeCierreConFinanciamiento.MontoSinInteres, gastoDeCierreConFinanciamiento.InteresGC);
                     cuotasMaestroDetalle.Add(cuota);
                    
                }
                //ajustarValores();
            }
            else
            {
                var primeraCuota = new CuotaPrestamoBuilder().CreateCuotaAndDetalle(getFecha(1), 1, capitalPorCuota, interesPorCuota, gastoDeCierreConFinanciamiento.MontoSinInteres, gastoDeCierreConFinanciamiento.InteresGC);
                cuotasMaestroDetalle.Add(primeraCuota);
                if (infoGenerarCuotas.CantidadDeCuotas > 1)
                {
                    var ultima = infoGenerarCuotas.CantidadDeCuotas;
                    var ultimaCuota = new CuotaPrestamoBuilder().CreateCuotaAndDetalle(getFecha(ultima), ultima, capitalPorCuota, interesPorCuota, gastoDeCierreConFinanciamiento.MontoSinInteres, gastoDeCierreConFinanciamiento.InteresGC);
                    cuotasMaestroDetalle.Add(ultimaCuota);
                }
                //ajustarValores();
            }
            return cuotasMaestroDetalle;
        }
    }

    
}

