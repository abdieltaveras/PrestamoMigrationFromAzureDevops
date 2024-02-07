using DevBox.Core.Classes.Utils;
using Newtonsoft.Json;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public class TipoDrCr
    {
        public static char Debito => 'D';
        public static char Credito => 'C';
    }
    public class CodigosTiposTransaccionCxC
    {
        public static string Cuota => "CT";
        public static string Pago => "PG";
        public static string CargoInterno => "CI";
        public static string NotaDeDebito => "ND";
        public static string NotaDeCredito => "NC";
        public string GetNombreTipoDocumento(string codigoCargo)
        {
            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case "CA": nombre = "Capital"; break;
                case "INT": nombre = "Interes"; break;
                case "INTDV": nombre = "Interes despues de vencido"; break;
                case "MOR": nombre = "Moras (Cargos por atraso)"; break;
                case "GCINT": nombre = "Interes del gasto de cierre"; break;
                case "GC": nombre = "Gasto de cierre"; break;
                default: return string.Empty;
            }
            return nombre;
        }

    }

    public class CodigosCargosDebitos
    {
        public static string Capital => "CA";
        public static string Interes => "INT";
        public static string InteresDespuesDeVencido => "INTDV";
        public static string Moras => "MOR";
        public static string InteresDelGastoDeCierre => "INTGC";
        public static string GastoDeCierre => "GC";
        public static string GetNombreCargo(string codigoCargo)
        {
            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case "CA": nombre = "Capital"; break;
                case "INT": nombre = "Interes"; break;
                case "INTDV": nombre = "Interes despues de vencido"; break;
                case "MOR": nombre = "Moras (Cargos por atraso)"; break;
                case "GCINT": nombre = "Interes del gasto de cierre"; break;
                case "GC": nombre = "Gasto de cierre"; break;
                default: return string.Empty;
            }
            return nombre;
        }
        public override string ToString() => "Codigo para utilizar en los cargos";
    }

    public interface IMaestroDebitoConDetallesCxC 
    {
        int IdTransaccion { get; set; }
        char TipoDrCr { get;  }
        int IdPrestamo { get; set; }
        string CodigoTipoTransaccion { get; }
        string NumeroTransaccion { get; }
        Guid IdReferencia { get;  }
        DateTime Fecha { get; }
        decimal Monto { get; }
        decimal Balance { get; }
        string OtrosDetallesJson { get; }
        public IEnumerable<IDetalleDebitoCxC> GetDetallesCargos(); 
    }

    public interface IDetalleDebitoCxC
    {
        public int IdTransaccion { get; set; }
        public int IdTransaccionMaestro { get; }
        public Guid IdReferenciaMaestro { get; }
        public Guid IdReferenciaDetalle { get; }
        string CodigoCargo { get; set; }
        decimal Monto { get; set; }
        decimal Balance { get; set; }
    }

    internal abstract class BaseMaestroCxC : IMaestroDebitoConDetallesCxC
    {
        public int IdTransaccion { get; set; }
        public char TipoDrCr { get; set; }
        public int IdPrestamo { get; set; }
        public virtual string CodigoTipoTransaccion { get; set; }
        public virtual Guid IdReferencia { get; internal set; } = Guid.NewGuid();
        public string NumeroTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public string OtrosDetallesJson { get; set; }
        private List<IDetalleDebitoCxC> DetallesCargos { get; set; } = new List<IDetalleDebitoCxC>();

        public IEnumerable<IDetalleDebitoCxC> GetDetallesCargos() => DetallesCargos;
        internal void SetDetallesCargos(IEnumerable<IDetalleDebitoCxC> detallesCargos)
        {
            this.DetallesCargos.AddRange(detallesCargos);
            this.Monto = this.DetallesCargos.Sum(item => item.Monto);
            this.Balance = this.DetallesCargos.Sum(item => item.Balance);
        }
        
    }

    /// <summary>
    /// Nueva cuota
    /// </summary>
    internal class MaestroDrConDetalles : BaseMaestroCxC
    {
        
        public override string ToString() => $"No {NumeroTransaccion} Fecha {Fecha} Monto {Monto} Balance {Balance}";
        
    }


    internal class NotaDeDebito : BaseMaestroCxC 
    {
        public override string CodigoTipoTransaccion => CodigosTiposTransaccionCxC.NotaDeDebito; 

        public string Concepto { get; set; }
    }

    internal class CargoPorAtraso : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosCargosDebitos.Moras;
    }

    internal class CargoPorInteresDespuesDeVencido : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosCargosDebitos.InteresDespuesDeVencido;
    }

    internal class DetalleCargoCxC : IDetalleDebitoCxC
    {
        public int IdTransaccion { get; set; }
        public int IdTransaccionMaestro { get; set; }
        public Guid IdReferenciaMaestro { get; internal set; }
        public Guid IdReferenciaDetalle { get; private set; } = Guid.NewGuid();
        public string CodigoCargo { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public override string ToString() => $"Codigo {CodigoCargo} Monto {Monto} Balance {Balance}";
    }

    internal class CuotaPrestamoBuilder
    {
        private List<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();
        private Guid IdReferenciaMaestro { get; set; }

        internal IMaestroDebitoConDetallesCxC CreateCuotaAndDetalle(DateTime fecha, int numero, decimal capital, decimal interes, decimal gastoDeCierre, decimal interesDelGastoDeCierre)
        {


            var cuota = new MaestroDrConDetalles
            {
                Fecha = fecha,
                NumeroTransaccion = numero.ToString(),
                CodigoTipoTransaccion = CodigosTiposTransaccionCxC.Cuota,

            };
            this.IdReferenciaMaestro = cuota.IdReferencia;
            AddCargo(CodigosCargosDebitos.Capital, capital);
            AddCargo(CodigosCargosDebitos.Interes, interes);
            AddCargo(CodigosCargosDebitos.GastoDeCierre, gastoDeCierre);
            AddCargo(CodigosCargosDebitos.InteresDelGastoDeCierre, interesDelGastoDeCierre);
           
            cuota.SetDetallesCargos(this.Detalles);
            return cuota;
        }

        private void AddCargo(string codigoCargo, decimal monto)
        {
            if (monto <= 0) return;
            var cargo = new DetalleCargoCxC
            {
                CodigoCargo = codigoCargo,
                Monto = monto,
                Balance = monto,
                IdReferenciaMaestro = this.IdReferenciaMaestro,
            };
            this.Detalles.Add(cargo);

        }
    }
}
