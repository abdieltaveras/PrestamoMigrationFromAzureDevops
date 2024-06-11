using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public interface IInfoGeneradorCuotas
    {

        int IdNegocio { get; set; }
        int IdLocalidadNegocio { get; set; }

        
        string Usuario { get; set; }
        TiposAmortizacion TipoAmortizacion { get; }
        DateTime FechaEmisionReal { get; }
        decimal MontoCapital { get; }

        decimal MontoGastoDeCierre { get; }
        
        bool CargarInteresAlGastoDeCierre { get; }

        bool GastoDeCierreEsDeducible { get; }

        bool FinanciarGastoDeCierre { get; }

        decimal OtrosCargos { get; }
        bool CargarInteresOtrosCargos { get; }
        int CantidadDeCuotas { get; }
        bool AcomodarFechaALasCuotas { get; }
        DateTime FechaInicioPrimeraCuota { get; }
        int IdPeriodo { get; set; }
        int IdTasaInteres { get; set; }
        
        //decimal TasaDeInteresDelPeriodo { get; }
        //Periodo Periodo { get; }
        bool ProyectarPrimeraYUltima { get; }
    }

    public class InfoGeneradorDeCuotas : IInfoGeneradorCuotas
    {
        public InfoGeneradorDeCuotas()
        {

        }
        public int IdNegocio { get; set; }
        public int IdLocalidadNegocio { get; set; }

        public string Usuario { get; set; }
        public TiposAmortizacion TipoAmortizacion { get; set; }

        public DateTime FechaEmisionReal { get; set; } = InitValues._19000101;


        public decimal MontoCapital { get; set; } = 0;

        public decimal MontoGastoDeCierre { get; set; } = 0;

        /// <summary>
        /// indica que desea generar interes al gasto de cierre
        /// </summary>
        public bool CargarInteresAlGastoDeCierre { get; set; }
        /// <summary>
        /// para indicar si desea o no distribuir en cuotas el gasto de cierre
        /// </summary>
        public bool FinanciarGastoDeCierre { get; set; }

        public int CantidadDeCuotas { get; set; } = 0;

        public bool AcomodarFechaALasCuotas { get; set; } 

        public DateTime FechaInicioPrimeraCuota { get; set; }

        public int IdTasaInteres { get; set; }
        
        public int IdPeriodo { get; set; }
        //private Periodo Periodo { get; set; }

        public bool GastoDeCierreEsDeducible { get; set; }

        public bool ProyectarPrimeraYUltima { get; set; } 
        public decimal OtrosCargos { get; set; }

        public bool CargarInteresOtrosCargos {get;set;}

        public InfoGeneradorDeCuotas(Prestamo prestamo)
        {
                AcomodarFechaALasCuotas = prestamo.AcomodarFechaALasCuotas;
                CantidadDeCuotas = prestamo.CantidadDeCuotas;
                MontoCapital = prestamo.MontoCapital;
                FechaEmisionReal = prestamo.FechaEmisionReal;
                FechaInicioPrimeraCuota = prestamo.FechaInicioPrimeraCuota;
                CargarInteresAlGastoDeCierre = prestamo.CargarInteresAlGastoDeCierre;
                FinanciarGastoDeCierre = prestamo.FinanciarGastoDeCierre;
                MontoGastoDeCierre = prestamo.MontoGastoDeCierre;
                OtrosCargos = prestamo.OtrosCargos;
                GastoDeCierreEsDeducible = prestamo.GastoDeCierreEsDeducible;
                IdPeriodo = prestamo.IdPeriodo;
                IdTasaInteres = prestamo.IdTasaInteres;
                TipoAmortizacion = (TiposAmortizacion)prestamo.IdTipoAmortizacion;
        }
    }

}
