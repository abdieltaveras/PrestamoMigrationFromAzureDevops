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
        public static string GetNombreTipoDocumento(string codigoCargo)
        {
            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case "CT": nombre = "Cuota"; break;
                case "PG": nombre = "Pago"; break;
                case "CI": nombre = "Cargo Interno"; break;
                case "ND": nombre = "Nota de Debito"; break;
                case "NC": nombre = "Nota de Credito"; break;
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
                case "GC": nombre = "Gasto de cierre"; break;
                case "INTGC": nombre = "Interes del gasto de cierre"; break;
                default: return string.Empty;
            }
            return nombre;
        }
        public override string ToString() => "Codigo para utilizar en los cargos";
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
        
        public override string ToString() => $"No {NumeroTransaccion} Fecha {Fecha} Monto {Monto} Balance {Balance} {DetallesCargosText()}"  ;

        public string DetallesCargosText()
        {
            string texto = string.Empty;
            foreach (var item in GetDetallesCargos())
            {
                texto = texto + item + ',';
            }
            return texto;
        }
    }

    internal class DebitoPrestamoViewModel : ICxCCuota
    {
        //private MaestroDrConDetalles Debito { get; set; }

        private  DebitoPrestamoViewModel(MaestroDrConDetalles value)
        {
            this.Fecha = value.Fecha;
            this.NombreDocumento = CodigosTiposTransaccionCxC.GetNombreTipoDocumento(value.CodigoTipoTransaccion); 
             this.NumeroTransaccion = value.NumeroTransaccion;
            foreach (var item in value.GetDetallesCargos())
            {
                if (item.CodigoCargo == CodigosCargosDebitos.Capital)
                {
                    this.Capital = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitos.Interes)
                {
                    this.Interes = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitos.GastoDeCierre)
                {
                    this.GastoDeCierre = item.Balance;
                    continue;
                }

                if (item.CodigoCargo == CodigosCargosDebitos.InteresDelGastoDeCierre)
                {
                    this.InteresDelGastoDeCierre = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitos.Moras)
                {
                    this.Mora = item.Balance;
                    continue;
                }
                this.OtrosCargos = item.Balance;
            }
        }

        internal static DebitoPrestamoViewModel Create(MaestroDrConDetalles value)
        { 
            return new DebitoPrestamoViewModel(value);
        }
        public string NombreDocumento { get; set; }

        public string NumeroTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Capital { get; set; }
             
        public Decimal Interes { get; set; }
                     
        public Decimal GastoDeCierre { get; set; }
        public decimal InteresDelGastoDeCierre { get;  set; }
        public decimal Mora { get;  set; }
        public decimal OtrosCargos { get; set; }
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
        public override string ToString() => $"{CodigosCargosDebitos.GetNombreCargo(CodigoCargo)} Monto {Monto} Balance {Balance}";
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
