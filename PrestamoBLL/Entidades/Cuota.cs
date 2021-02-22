using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class CuotaForSqlType
    {
        public int idCuota { get; internal set; } = 0;
        public int IdPrestamo { get; internal set; } = 0;
        public decimal Numero { get; internal set; } = 0;
        public DateTime Fecha { get; internal set; } = DateTime.Now;
        public decimal Capital { get; internal set; } = 0;
        public decimal Interes { get; internal set; } = 0;
        public decimal? GastoDeCierre { get; internal set; } = 0;
        public decimal? InteresDelGastoDeCierre { get; internal set; } = 0;
        public decimal? OtrosCargos { get; internal set; } = 0;

        public decimal? InteresOtrosCargos { get; set; }
    }
    public class Cuota : CuotaForSqlType
    {
        [IgnorarEnParam]
        public string FechaSt => Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        [IgnorarEnParam]
        public decimal TotalOrig => Capital + Interes + +(decimal)GastoDeCierre + (decimal)InteresDelGastoDeCierre + (decimal)OtrosCargos + (decimal)InteresOtrosCargos;
        public decimal BceGeneral => BceCapital + BceInteres + (decimal)BceGastoDeCierre + (decimal)BceInteresDelGastoDeCierre + (decimal)BceOtrosCargos + (decimal)BceInteresOtrosCargos;
        public decimal BceCapital { get; internal set; } = 0;
        public decimal BceInteres { get; internal set; } = 0;
        public decimal? BceGastoDeCierre { get; internal set; } = 0;
        public decimal? BceInteresDelGastoDeCierre { get; internal set; } = 0;
        public decimal? BceOtrosCargos { get; internal set; } = 0;
        public decimal? BceInteresOtrosCargos { get; set; }
        public DateTime? UltActFechaMora { get; set; } = null;
        public DateTime? UltActFechaInteres { get; set; } = null;

        public bool Atrasada(DateTime fecha) => this.Fecha.CompareTo(fecha) < 0;
        public bool MenorOIgualALaFecha(DateTime fecha) => this.Fecha.CompareTo(fecha) <= 0;

    }

    public class OtrosCargosPrestamo
    {
        public int idOtrosCargosCuotas { get; set; }
        public int idCuenta { get; set; }
        public int idTipoCargo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public string Detalle { get; set; } = null;
        public string OtrosDetalle { get; set; } = null;
        public bool? esInteresOMoraPeriodica { get; set; }
        public bool? EsDefinitivo { get; set; }
        // los cargos deberan decir sin son definitivos o no,  los NODEFINITIVO, 
        // como los que se procesan a fin de mes Y DEFINITIVO los que se cargan cuando se realiza el pago
        // o el credito, ya que ese es el momento real cuando esos cargos ya se hacen definitivos
        // y para fines de calcularlos debera iniciar a partir del ultimo cargo definitivo 
        // que sea de interesOMora y rebajar al total que produzca, los cargos NODEFINITIVO
        // que indican que son cargos tentativos mientras se hace ese calculo al momento del pago
    }

    
}