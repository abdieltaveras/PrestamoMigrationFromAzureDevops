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
        public decimal GastoDeCierre { get; internal set; } = 0;
        public decimal InteresDelGastoDeCierre { get; internal set; } = 0;
        public decimal OtrosCargosSinInteres { get; internal set; } = 0;

        
        //public decimal CapitalOrig { get; internal set; } = 0;
        //public decimal CapitalBce { get; internal set; } = 0;
        //public decimal InteresOrig { get; internal set; } = 0;
        //public decimal InteresBce { get; internal set; } = 0;

        /// attention: revisar con ernesto si esto tiene otra forma de abordarlo
        /// <summary>
        /// se decidio hacerlo un metodo para no entrar en conflicto con los parametros del tipo
        /// </summary>
        /// <returns></returns>
    }
    public class Cuota : CuotaForSqlType
    {
        public decimal OtrosCargos { get; internal set; }
        public int IdOtrosCargos { get; internal set; }
        [IgnorarEnParam]
        public string FechaSt  => Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        [IgnorarEnParam]
        public decimal Total => Capital + Interes + +GastoDeCierre + InteresDelGastoDeCierre + OtrosCargosSinInteres; 

        //public decimal CapitalOrig { get; internal set; } = 0;
        //public decimal CapitalBce { get; internal set; } = 0;
        //public decimal InteresOrig { get; internal set; } = 0;
        //public decimal InteresBce { get; internal set; } = 0;

        /// attention: revisar con ernesto si esto tiene otra forma de abordarlo
        /// <summary>
        /// se decidio hacerlo un metodo para no entrar en conflicto con los parametros del tipo
        /// </summary>
        /// <returns></returns>
    }
    //
    public class CuotaAmpliada 
    {

        public DateTime Fecha { get; internal set; } = InitValues._19000101;
        public decimal BalanceTotal => BceCapital + BceInteres + BceMora + BceOtrosCargos + BceInteresDespuesDeVencido;
        public bool Atrasada(DateTime fecha) => this.Fecha.CompareTo(fecha) < 0;
        public bool MenorOIgualALaFecha(DateTime fecha) => this.Fecha.CompareTo(fecha) <= 0;
        public decimal BceCapital { get; internal set; } = 0;
        public decimal BceInteres { get; internal set; } = 0;
        public decimal BceMora { get; internal set; } = 0;
        public decimal BceOtrosCargos { get; internal set; } = 0;
        public decimal BceInteresDespuesDeVencido { get; internal set; } = 0;
    }

}

