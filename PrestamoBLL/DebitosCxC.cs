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
        public string GetNombreCargo(string codigoCargo)
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


    public interface IMaestroDebitoSinDetallesCxC
    {
        int IdTransaccion { get; set; }
        int IdPrestamo { get; set; }
        char TipoDrCr { get; }
        string CodigoTipoTransaccion { get; }
        string NumeroTransaccion { get; }
        string IdReferencia { get; set; }
        DateTime Fecha { get; }
        decimal Monto { get; }
        decimal Balance { get; }
    }

    public interface IMaestroDebitoConDetallesCxC : IMaestroDebitoSinDetallesCxC
    {
        public string DetallesCargosJson { get; }
    }

    public interface IDetalleDebitoCxC
    {
        //int IdTransaccion { get; set; }
        //int IdDetalle { get; set; }
        //string IdReferenciaMaestro { get; set; }
        string CodigoCargo { get; set; }
        decimal Monto { get; set; }
        decimal Balance { get; set; }
    }

    internal abstract class BaseMaestroCxC : IMaestroDebitoConDetallesCxC
    {
        public int IdTransaccion { get; set; }
        public virtual string CodigoTipoTransaccion { get; }
        public string NumeroTransaccion { get; set; }
        public virtual string IdReferencia { get; set; }
        public int IdPrestamo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public char TipoDrCr { get; set; }
        public string DetallesCargosJson { get; set; }
        private List<DetalleCargoCxC> DetalleCargos { get; set; } = new List<DetalleCargoCxC>();

        public void ConvertJsonToDetallesCargos(string detallesText)
        {
            if (!detallesText.IsNullOrEmpty())
            {
                var detalles = JsonConvert.DeserializeObject<List<DetalleCargoCxC>>(detallesText);
                this.DetalleCargos = detalles;
            }
        }
        public IEnumerable<DetalleCargoCxC> GetDetallesCargos() => DetalleCargos;
    }

    internal abstract class CuotaMaestroSinDetallesCxC : IMaestroDebitoSinDetallesCxC
    {
        public int IdTransaccion { get; set; }
        public int IdPrestamo { get; set; }

        public string CodigoTransaccion { get; set; }

        public string IdReferencia { get; set; }
        //public int Numero { get; internal set; }
        public string NumeroTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; protected set; }
        public decimal Balance { get; protected set; }
        public string OtrosDetalles { get; set; }
        public char TipoDrCr => 'D';

        string IMaestroDebitoSinDetallesCxC.CodigoTipoTransaccion => throw new NotImplementedException();
    }
    /// <summary>
    /// Nueva cuota
    /// </summary>
    internal class CuotaMaestroConDetallesCxC : CuotaMaestroSinDetallesCxC, IMaestroDebitoConDetallesCxC
    {
        public string DetallesCargosJson { get; private set; }

        private List<IDetalleDebitoCxC> DetallesCargos { get; set; } = new List<IDetalleDebitoCxC>();
        public void SetDetallesCargos(IEnumerable<IDetalleDebitoCxC> detallesCargos)
        {
            this.DetallesCargos.AddRange(detallesCargos);
            this.Monto = this.DetallesCargos.Sum(item => item.Monto);
            this.Balance = this.DetallesCargos.Sum(item => item.Balance);
            this.DetallesCargosJson = JsonConvert.SerializeObject(this.DetallesCargos);
        }
        public IEnumerable<IDetalleDebitoCxC> GetDetallesCargos()
        {
            var detallesCargos = new List<DetalleCargoCxC>();
            if (!DetallesCargosJson.IsNullOrEmpty())
            {
                var detalles = JsonConvert.DeserializeObject<List<DetalleCargoCxC>>(DetallesCargosJson);
                detallesCargos = detalles;
            }
            return detallesCargos;
        }
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
        public string CodigoCargo { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public override string ToString() => $"Codigo {CodigoCargo} Monto {Monto} Balance {Balance}";
    }

    internal class CuotaCxC
    {

        IList<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();
        string IdReferencia { get; set; }

        internal IMaestroDebitoConDetallesCxC CreateCuotaAndDetalle(DateTime fecha, int numero, decimal capital, decimal interes, decimal gastoDeCierre, decimal interesDelGastoDeCierre)
        {

            this.IdReferencia = Guid.NewGuid().ToString();
            AddCargo(CodigosCargosDebitos.Capital, capital);
            AddCargo(CodigosCargosDebitos.Interes, interes);
            AddCargo(CodigosCargosDebitos.GastoDeCierre, gastoDeCierre);
            AddCargo(CodigosCargosDebitos.InteresDelGastoDeCierre, interesDelGastoDeCierre);

            var cuota = new CuotaMaestroConDetallesCxC
            {

                IdReferencia = this.IdReferencia,
                Fecha = fecha,
                NumeroTransaccion = numero.ToString(),
                CodigoTransaccion = CodigosTiposTransaccionCxC.Cuota
            };
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
                
            };
            this.Detalles.Add(cargo);

        }
    }
}
