using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class CodigosTiposDocumentosCxC
    {
        public static string Cuota => "CTA";
        public static string Pago => "PAGO";
        public static string CargoInternos => "CargoI";
        public static string NotaDeDebito => "ND";
        public static string NotaDeCredito => "NC";
    }

    public class CodigosCargosReservadosDebitos
    {
        public static string Capital => "CA";
        public static string Interes => "INT";
        public static string InteresDespuesDeVencido => "INTDV";
        public static string Moras => "MOR";
        public static string GastoDeCierreInteres => "GCINT";
        public static string GastoDeCierre => "GC";
        public string NombreCargo(string codigoCargo)
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


    public interface IMaestroDebitoSinDetallesCxC
    {
        int IdTransaccion { get; set; }
        int IdPrestamo { get; set; }
        string CodigoTransaccion { get; }
        string NombreTransaccion { get; }
        
        string IdReferencia { get; set; }
        DateTime Fecha { get; }
        
        decimal Monto { get; }
        decimal Balance { get; }
    }

    public interface IMaestroDebitoConDetallesCxC : IMaestroDebitoSinDetallesCxC
    {
        IList<IDetalleDebitoCxC> Detalles { get; }
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

    internal abstract class BaseMaestroCxC : BaseInsUpd, IMaestroDebitoConDetallesCxC
    {
        public int IdTransaccion { get; set; }
        public virtual string CodigoTransaccion { get; }
        public virtual string NombreTransaccion { get; }
        public virtual string IdReferencia { get; set; }
        public int IdPrestamo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public IList<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();
    }

    internal abstract class CuotaMaestroSinDetallesCxC : IMaestroDebitoSinDetallesCxC
    {
        public int IdTransaccion { get; set; }
        public int IdPrestamo { get; set; }
        //public string CodigoTransaccion => "Cta";
        //public string NombreTransaccion => "CuotaMaestro";

        public string CodigoTransaccion {get;set;}
        public string NombreTransaccion {get;set;}
        public string IdReferencia { get; set; }
        //public int Numero { get; internal set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public string OtrosDetalles { get; set; }

    }
    /// <summary>
    /// Nueva cuota
    /// </summary>
    internal class CuotaMaestro : CuotaMaestroSinDetallesCxC, IMaestroDebitoConDetallesCxC
    {
        public IList<IDetalleDebitoCxC> Detalles { get; set; }

        public override string ToString() => $"No {Numero} Fecha {Fecha} Monto {Monto} Balance {Balance}";
    }

    internal class NotaDeDebito : BaseMaestroCxC
    {
        public override string CodigoTransaccion => "ND";
        public override string NombreTransaccion => "Nota de debito";
        public string Concepto { get; set; }
    }

    internal class CargoPorAtraso : BaseMaestroCxC
    {
        public override string CodigoTransaccion => "CAT";
        public override string NombreTransaccion => "Cargos por atraso";

    }

    internal class CargoPorInteresDespuesDeVencido : BaseMaestroCxC
    {
        public override string CodigoTransaccion => "CINTDV";
        public override string NombreTransaccion => "Cargos por interes despues de vencido";
    }

    internal class DetalleCargoCxC : IDetalleDebitoCxC
    {
        public string CodigoCargo { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public override string ToString() => $"Codigo {CodigoCargo} Monto {Monto} Balance {Balance}";
    }

    public class CuotaCxC
    {
        decimal MontoTotal { get;  set; }
        IMaestroDebitoConDetallesCxC Cuota { get; set; }
        IList<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();

        DateTime FechaInicial { get; set; }

        int IdPrestamo { get; set; }

        TasaInteres TasaInteres { get; set; }
        Periodo Periodo { get; set; }

        decimal Capital { get; set; }
        decimal Interes { get; set; }
        decimal GastoDeCierre { get; set; }
        decimal InteresDelGastoDeCierre { get; set; }
        int CantidadDeCuota { get; set; }

        string IdReferencia { get; set; }

        internal IMaestroDebitoConDetallesCxC CreateCuotaAndDetalle(DateTime fecha, int numero, decimal capital, decimal interes, decimal gastoDeCierre, decimal interesDelGastoDeCierre)
        {

            this.IdReferencia = numero.ToString();
            AddCargo("CA",capital);
            AddCargo("INT", interes);
            AddCargo("GC", gastoDeCierre);
            AddCargo("INTGC", interesDelGastoDeCierre);

            var cuota = new CuotaMaestro
            {
                Fecha = fecha,
                Monto = MontoTotal,
                Numero = numero,
                Balance = MontoTotal,
                Detalles = this.Detalles,
            };
            return cuota;
        }

        private void AddCargo(string codigoCargo,decimal monto)
        {
            if (monto <= 0) return ;
            var cargo = new DetalleCargoCxC
            {
                CodigoCargo = codigoCargo,
                Monto = monto,
                Balance = monto,
            };
            this.Detalles.Add(cargo);
            MontoTotal += monto;
        }
    }
}
