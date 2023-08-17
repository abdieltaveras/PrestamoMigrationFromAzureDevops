using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public partial class BLLPrestamo
    {


        public interface IGeneradorCuotasV2
        {
            List<CuotaPrestamo> GenerarCuotas();
        }

        //public class GeneradorCuotasBase : IGeneradorCuotasV2
        //{
        //    private readonly IInfoGeneradorCuotas InfoGenerarCuotas;
        //    private readonly Periodo Periodo;
        //    private readonly int IdPrestamo;
        //    List<CuotaPrestamo> CuotasPrestamo = new List<CuotaPrestamo>();
        //    DateTime fechaCuotaAnterior = InitValues._19000101;
        //    private decimal TasasInteresDelPeriodo => InfoGenerarCuotas.TasaDeInteresDelPeriodo;
        //    decimal CapitalPorCuota { get; set; }
        //    decimal InteresPorCuota { get; set; }
        //    decimal OtrosCargosPorCuota { get; set; }
        //    decimal InteresOtrosCargosPorCuota { get; set; }
        //    decimal GastoDeCierrePorCuota { get; set; }
        //    decimal InteresGastoDeCierrePorCuota { get; set; }

        //    public GeneradorCuotasBase(IInfoGeneradorCuotas info, int idPrestamo)
        //    {
        //        if (info.TipoAmortizacion != TiposAmortizacion.No_Amortizable_cuotas_fijas)
        //        { throw new InvalidOperationException("para generar este tipo de cuotas, solo se admite el tipo de amortizacion No Amortizable Cuota Fija"); }
        //        InfoGenerarCuotas = info;
        //        Periodo = info.IdPeriodo;
        //        IdPrestamo = idPrestamo;
        //    }
        //    public List<CuotaPrestamo> GenerarCuotas()
        //    {
        //        //var cuotaMaestra = new CuotaPrestamoMaestro(idPrestamo, )


        //        GastoDeCierreSinFinanciamiento();
        //        CapitalPorCuota = getCapitalPorCuota();
        //        InteresPorCuota = getInteresPorCuota();
        //        OtrosCargosPorCuota = getOtrosCargosPorCuota();
        //        InteresOtrosCargosPorCuota = getInteresOtrosCargosPorCuota();
        //        GastoDeCierrePorCuota = getGastoDeCierrrePorCuota();
        //        InteresGastoDeCierrePorCuota = getInteresGastoDeCierrePorCuota();
        //        this.fechaCuotaAnterior = InfoGenerarCuotas.FechaEmisionReal;
        //        // Como se manejara el gasto de cierre aqui
        //        // que seran las cuotas realmente en este sistema
        //        // sera algo mas abierto como cuentas por cobrar de monica que manejara lo que es un doc_cc
        //        // que permite cualquier cosa y que establezca un tipo que sea cuotaPrestamo
        //        // y para los demas casos otros tipos segun la necesidad. analizarlo, pero iniciar como ahora
        //        // el sistema con cuotas o si a lass cuotas se le pondra alguna informacion que detalle otros casos
        //        if (!InfoGenerarCuotas.ProyectarPrimeraYUltima)
        //        {
        //            CuotasPrestamo.Clear();
        //            for (int i = 1; i <= InfoGenerarCuotas.CantidadDeCuotas; i++)
        //            {
        //                var cuotaMaestra = SetCargosCuota(i);
        //                this.fechaCuotaAnterior = cuotaMaestra.Fecha;
        //                CuotasPrestamo.Add(cuotaMaestra);
        //            }
        //            ajustarValores();
        //        }
        //        else
        //        {
        //            var primera = SetCargosCuota(1);
        //            this.fechaCuotaAnterior = primera.Fecha;
        //            primera.Detalle = "Primera cuota";
        //            if (InfoGenerarCuotas.CantidadDeCuotas > 1)
        //            {
        //                var ultima = SetCargosCuota(InfoGenerarCuotas.CantidadDeCuotas);
        //                ultima.Fecha = PrestamoConCalculos.CalcularFechaVencimiento(this.InfoGenerarCuotas.FechaEmisionReal, InfoGenerarCuotas.Periodo, InfoGenerarCuotas.CantidadDeCuotas, InfoGenerarCuotas.FechaInicioPrimeraCuota);
        //                ultima.Detalle = "Ultima Cuota";
        //            }

        //            //ajustarValores();
        //        }
        //        return CuotasPrestamo;
        //    }

        //    private CuotaPrestamo SetCargosCuota(int numeroCuota)
        //    {
        //        var cuotaMaestra = new CuotaPrestamo(numeroCuota, IdPrestamo, getFecha(numeroCuota));
        //        cuotaMaestra.CapitalMonto = CapitalPorCuota;
        //        cuotaMaestra.InteresCapitalMonto = InteresPorCuota;
        //        cuotaMaestra.GastoDeCierreMonto =GastoDeCierrePorCuota;
        //        cuotaMaestra.InteresGastoDeCierreMonto = InteresGastoDeCierrePorCuota;
        //        cuotaMaestra.OtrosCargosMonto =OtrosCargosPorCuota;
        //        cuotaMaestra.InteresOtrosCargosMonto= InteresOtrosCargosPorCuota;
        //        return cuotaMaestra;
        //    }

        //    /// <summary>
        //    /// revisa si la sumatoria de los valores de las cuotas tienen alguna diferencia y lo ajustan
        //    /// ejemplo si de capital debe ser 1000 y la sumatoria de las cuotas da 998.75
        //    /// el sistema a la ultima cuota le hace un ajuste
        //    /// </summary>
        //    private void ajustarValores()
        //    {
        //        decimal totalCapital = 0;
        //        decimal totalCapital2 = 0;
        //        decimal totalOtrosCargos = 0;
        //        decimal totalGastoDeCierre = 0;

        //        foreach (var cuota in CuotasPrestamo)
        //        {
        //            var detalleCargos = cuota.ItemsDrCxC;
        //            totalCapital2 = cuota.CapitalMonto;
        //            totalOtrosCargos += cuota.OtrosCargosMonto;
        //            totalGastoDeCierre += cuota.GastoDeCierreMonto;

        //        }

        //        decimal ajusteCapital, ajusteOtrosCargos, ajusteGastoDeCierre;
        //        decimal diferenciaCapital = InfoGenerarCuotas.MontoCapital - totalCapital;
        //        decimal diferenciaOtrosCargos = InfoGenerarCuotas.OtrosCargos - totalOtrosCargos;
        //        decimal diferenciaGastoDeCierre=0;
        //        if (!InfoGenerarCuotas.GastoDeCierreEsDeducible)
        //        {
        //            diferenciaGastoDeCierre = InfoGenerarCuotas.MontoGastoDeCierre - totalGastoDeCierre;
        //        }

        //        var ultimaCuota = CuotasPrestamo.Last();
        //        var monto = ultimaCuota.Monto;
        //        ajusteCapital = ultimaCuota.CapitalMonto + diferenciaCapital;
        //        ajusteOtrosCargos = ultimaCuota.OtrosCargosMonto + diferenciaOtrosCargos;
        //        ajusteGastoDeCierre = ultimaCuota.GastoDeCierreMonto + diferenciaGastoDeCierre;

                
        //        //var ultimaCuotaAjustada = new CuotaPrestamoMaestro(InfoGenerarCuotas.CantidadDeCuotas, IdPrestamo, ultimaCuota.Fecha);

        //        //CuotasPrestamo.Remove(ultimaCuota);
        //        ultimaCuota.CapitalMonto=ajusteCapital;
        //        ultimaCuota.OtrosCargosMonto=ajusteOtrosCargos;
        //        ultimaCuota.GastoDeCierreMonto = ajusteGastoDeCierre;
        //        //CuotasPrestamo.Add(ultimaCuotaAjustada);
        //        //var monto2 = ultimaCuota.Monto;
        //    }

        //    private void GastoDeCierreSinFinanciamiento()
        //    {
        //        if ((this.InfoGenerarCuotas.MontoGastoDeCierre > 0) && (!this.InfoGenerarCuotas.FinanciarGastoDeCierre) && (!InfoGenerarCuotas.GastoDeCierreEsDeducible))
        //        {
        //            var cuota = new CuotaPrestamo(0, this.IdPrestamo, InfoGenerarCuotas.FechaEmisionReal);
        //            cuota.Detalle = "Gasto De Cierre a pagar";
        //            cuota.GastoDeCierreMonto =(this.InfoGenerarCuotas.MontoGastoDeCierre);
        //            this.CuotasPrestamo.Add(cuota);
        //        }
        //    }

        //    private DateTime getFecha(int i)
        //    {
        //        if (i == 1 && InfoGenerarCuotas.AcomodarFechaALasCuotas)
        //        {
        //            this.fechaCuotaAnterior = Convert.ToDateTime(InfoGenerarCuotas.FechaInicioPrimeraCuota);
        //            return this.fechaCuotaAnterior;
        //        }
        //        DateTime fechaCuota = calcFecha();
        //        return fechaCuota;
        //    }

        //    private DateTime calcFecha()
        //    {
        //        var fecha = new DateTime();
        //        switch (Periodo.PeriodoBase)
        //        {
        //            case PeriodoBase.Dia:
        //                fecha = this.fechaCuotaAnterior.AddDays(1 * (int)Periodo.MultiploPeriodoBase);
        //                break;
        //            case PeriodoBase.Semana:
        //                fecha = this.fechaCuotaAnterior.AddDays(7 * (int)Periodo.MultiploPeriodoBase);
        //                break;
        //            case PeriodoBase.Quincena:
        //                fecha = this.fechaCuotaAnterior;
        //                for (int periodoCuota = 1; periodoCuota <= (int)Periodo.MultiploPeriodoBase; periodoCuota++)
        //                {
        //                    if (periodoCuota % 2 == 0)
        //                    {
        //                        fecha = fecha.AddMonths(periodoCuota / 2);
        //                    }
        //                    else
        //                    {
        //                        fecha = fecha.AddDays(15);
        //                    }
        //                }
        //                break;
        //            case PeriodoBase.Mes:
        //                fecha = this.fechaCuotaAnterior.AddMonths(1 * (int)Periodo.MultiploPeriodoBase);
        //                break;
        //            case PeriodoBase.Ano:
        //                fecha = this.fechaCuotaAnterior.AddYears(1 * (int)Periodo.MultiploPeriodoBase);
        //                break;
        //            default:
        //                break;
        //        }

        //        return fecha;
        //    }

        //    private decimal getOtrosCargosPorCuota()
        //    {
        //        // empezaremos pensando en que no tiene interes el gasto de cierre
        //        // ni tampoco los otros gastos
        //        decimal otrosCargosPorCuota = Math.Round(InfoGenerarCuotas.OtrosCargos / InfoGenerarCuotas.CantidadDeCuotas, 2);
        //        return otrosCargosPorCuota;
        //    }

        //    private decimal getInteresOtrosCargosPorCuota()
        //    {

        //        var interesOtrosCargos = 0M;
        //        // empezaremos pensando en que no tiene interes el gasto de cierre
        //        // ni tampoco los otros gastos
        //        if (InfoGenerarCuotas.CargarInteresOtrosCargos)
        //        {
        //            interesOtrosCargos = Math.Round(InfoGenerarCuotas.OtrosCargos * (TasasInteresDelPeriodo / 100), 2);
        //        }
        //        return interesOtrosCargos;
        //    }

        //    private decimal getCapitalPorCuota()
        //    {
        //        var capitalPorCuota = Math.Round(InfoGenerarCuotas.MontoCapital / InfoGenerarCuotas.CantidadDeCuotas, 2);
        //        return capitalPorCuota;
        //    }
        //    private decimal getInteresPorCuota()
        //    {
        //        // empezaremos pensando en que no tiene interes el gasto de cierre
        //        // ni tampoco los otros gastos
        //        decimal interesPorCuota = Math.Round(InfoGenerarCuotas.MontoCapital * (TasasInteresDelPeriodo / 100), 2);
        //        return interesPorCuota;
        //    }

        //    private decimal getGastoDeCierrrePorCuota()
        //    {
        //        var gastoDeCierrePorCuota = 0M;
        //        if (this.InfoGenerarCuotas.GastoDeCierreEsDeducible)
        //        {
        //            return gastoDeCierrePorCuota;
        //        }

        //        if ((this.InfoGenerarCuotas.MontoGastoDeCierre > 0) &&
        //           (this.InfoGenerarCuotas.FinanciarGastoDeCierre))
        //        {
        //            gastoDeCierrePorCuota = Math.Round(this.InfoGenerarCuotas.MontoGastoDeCierre / this.InfoGenerarCuotas.CantidadDeCuotas,2);
        //        }
        //        else
        //        {
        //            gastoDeCierrePorCuota = this.InfoGenerarCuotas.MontoGastoDeCierre;
        //        }
        //        return gastoDeCierrePorCuota;

        //    }
        //    private decimal getInteresGastoDeCierrePorCuota()
        //    {
        //        var interesGastoDeCierrePorCuota = 0M;
        //        if (this.InfoGenerarCuotas.GastoDeCierreEsDeducible)
        //        {
        //            return interesGastoDeCierrePorCuota;
        //        }

        //        if ((this.InfoGenerarCuotas.MontoGastoDeCierre > 0) &&
        //           (this.InfoGenerarCuotas.FinanciarGastoDeCierre))
        //        {
        //            interesGastoDeCierrePorCuota = Math.Round(this.InfoGenerarCuotas.MontoGastoDeCierre * (this.InfoGenerarCuotas.TasaDeInteresDelPeriodo / 100), 2);
        //        }
        //        return interesGastoDeCierrePorCuota;
        //    }


        //    private void setGastoDeCierreFinaciadoEnCuotas(CuotaPrestamo cuota)
        //    {
        //        if (this.InfoGenerarCuotas.GastoDeCierreEsDeducible)
        //        {
        //            return;
        //        }


        //        if ((this.InfoGenerarCuotas.MontoGastoDeCierre > 0) &&
        //           (this.InfoGenerarCuotas.FinanciarGastoDeCierre))
        //        {
        //            cuota.GastoDeCierreMonto = (Math.Round(this.InfoGenerarCuotas.MontoGastoDeCierre / this.InfoGenerarCuotas.CantidadDeCuotas));
        //            // ahora calculamos el interes del gasto de cierre si debe cargarlo
        //            if (this.InfoGenerarCuotas.CargarInteresAlGastoDeCierre)
        //            {
        //                cuota.InteresGastoDeCierreMonto =(Math.Round(this.InfoGenerarCuotas.MontoGastoDeCierre * (this.InfoGenerarCuotas.TasaDeInteresDelPeriodo / 100), 2));
        //            }
        //        }
        //    }
        //}


        //public class GeneradorCuotasFijasNoAmortizable2 : GeneradorCuotasBase
        //{
        //    public GeneradorCuotasFijasNoAmortizable2(IInfoGeneradorCuotas info, int idPrestamo): base(info,idPrestamo)
        //    {

        //    }
        //}

    }
    
}
