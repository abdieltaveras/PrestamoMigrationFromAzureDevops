using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    // <summary>
    // la representacion de la cuota como va en la tabla
    // </summary>
    public class CxCCuotaForSqlType
    {
        public int idCuota { get; set; } = 0;
        public int IdPrestamo { get; set; } = 0;
        public decimal Numero { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Capital { get; set; } = 0;
        public decimal Interes { get; set; } = 0;
        public decimal GastoDeCierre { get; set; } = 0;
        public decimal InteresDelGastoDeCierre { get; set; } = 0;
        public int idTipoCargo { get; internal set; } = -1;
    }
    public class CxCCuota : CxCCuotaForSqlType
    {
        [IgnoreOnParams]
        public string FechaSt => Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        [IgnoreOnParams]
        public decimal TotalOrig
        {
            get
            {
                var valor = (Capital) + (Interes) + (GastoDeCierre) + (InteresDelGastoDeCierre);
                //var valor2 = Capital + Interes  + +GastoDeCierre  + InteresDelGastoDeCierre + OtrosCargos  + InteresOtrosCargos ;
                return valor;
            }
        }
        public virtual decimal BceGeneral => BceCapital + BceInteres + BceGastoDeCierre + BceInteresDelGastoDeCierre; // + BceOtrosCargos??0 + BceInteresOtrosCargos??0;
        public decimal BceCapital { get; internal set; } = 0;
        public decimal BceInteres { get; internal set; } = 0;
        public decimal BceGastoDeCierre { get; internal set; } = 0;
        public decimal BceInteresDelGastoDeCierre { get; internal set; } = 0;
        //public decimal? BceOtrosCargos { get; internal set; } = 0;
        //public decimal? BceInteresOtrosCargos { get; set; } = 0;
        [IgnoreOnParams]
        public DateTime? UltActFechaMora { get; set; } = InitValues._19000101;
        [IgnoreOnParams]
        public DateTime? UltActFechaInteres { get; set; } = InitValues._19000101;

        public bool Vencida(DateTime fecha) => this.Fecha.CompareTo(fecha) < 0;
        public bool MenorOIgualALaFecha(DateTime fecha) => this.Fecha.CompareTo(fecha) <= 0;


        [IgnoreOnParams]
        public string Comentario { get; set; } = String.Empty;
        public override string ToString()
        {
            return $"Valores Originales Cuota No {Numero} Fecha {Fecha} Total {TotalOrig} Capital {Capital} Interes {Interes} G/C {GastoDeCierre} Int G/C {InteresDelGastoDeCierre}";
        }

        public string BalanceToString()
        {
            return $"Balancess Cuota No {Numero} Fecha {Fecha} Balance {BceGeneral} Capital {BceCapital} Interes {BceInteres} G/C {BceGastoDeCierre} Int G/C {BceInteresDelGastoDeCierre}";
        }

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