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
        decimal TasaDeInteresPorPeriodo { get; }
        Periodo Periodo { get; }
        bool ProyectarPrimeraYUltima { get; }
    }
    
}
