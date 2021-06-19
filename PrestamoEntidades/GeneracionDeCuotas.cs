using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
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
        decimal TasaDeInteresDelPeriodo { get; }
        Periodo Periodo { get; }
        bool ProyectarPrimeraYUltima { get; }
    }

    public class InfoGeneradorDeCuotas : IInfoGeneradorCuotas
    {
        public TiposAmortizacion TipoAmortizacion { get;  set; }

        public DateTime FechaEmisionReal { get; set; } = InitValues._19000101;


        public decimal MontoCapital { get; set; } = 0;

        public decimal MontoGastoDeCierre { get; set; } = 0;

        public bool CargarInteresAlGastoDeCierre { get; set; } = false;

        public int CantidadDePeriodos { get; set; } = 0;

        public bool AcomodarFechaALasCuotas { get; set; } = false;

        public DateTime FechaInicioPrimeraCuota { get; set; }

        public decimal TasaDeInteresDelPeriodo { get; set; } = 0;

        public Periodo Periodo { get; set; }

        public bool FinanciarGastoDeCierre { get; set; }
        public decimal OtrosCargosSinInteres { get; set; }
        public bool GastoDeCierreEsDeducible { get; set; }

        public bool ProyectarPrimeraYUltima { get; set; } = true;

        public InfoGeneradorDeCuotas(TiposAmortizacion tipoAMortizacion)
        {
            this.TipoAmortizacion = TipoAmortizacion;
        }

        public InfoGeneradorDeCuotas()
        {

        }
        public InfoGeneradorDeCuotas(Prestamo prestamo)
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
                TasaDeInteresDelPeriodo = prestamo.TasaDeInteresDelPeriodo;
                Periodo = prestamo.Periodo;
                //TipoAmortizacion = prestamo.TipoAmortizacion;
                TipoAmortizacion = (TiposAmortizacion)prestamo.IdTipoAmortizacion;
        }
    }

}
