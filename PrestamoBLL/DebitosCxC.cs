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

    internal class CodigosCargosDebitosV2 : Enumeration
    {
        public string Codigo { get; }
        public string Nombre => base.Name;

        public static CodigosCargosDebitosV2 Capital = new(1, nameof(Capital),"CA");
        public static CodigosCargosDebitosV2 Interes = new(2, nameof(Interes), "INT");

        public static CodigosCargosDebitosV2 InteresDespuesDeVencido = new(3, nameof(InteresDespuesDeVencido), "INTDV");
        public static CodigosCargosDebitosV2 Moras = new(3, nameof(Moras), "INTDV");
        public static CodigosCargosDebitosV2 GastoDeCierre = new(4, nameof(GastoDeCierre), "INTDV");

        public static CodigosCargosDebitosV2 InteresDelGastoDeCierre = new(5, nameof(InteresDelGastoDeCierre), "INTGC");

        private CodigosCargosDebitosV2(int id, string name, string codigo) : base(id, name)
        {
            this.Codigo = codigo;
        }
    }

    public class CodigosCargosDebitos
    {
        
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class CodigosCargosDebitosReservados
    {
        public const string Capital = "CA";
        public const string Interes = "INT";
        public const string InteresDespuesDeVencido = "INTDV";
        public const string Moras = "MOR";
        public const string InteresDelGastoDeCierre = "INTGC";
        public const string GastoDeCierre = "GC";
        public const string OtrosCargos = "OC";
        public const string InteresOtrosCargos = "INTOC";
        public static string GetNombreCargo(string codigoCargo)
        {
            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case Capital:  nombre = "Capital"; break;
                case Interes: nombre = "Interes"; break;
                case InteresDespuesDeVencido : nombre = "Interes despues de vencido"; break;
                case Moras : nombre = "Moras (Cargos por atraso)"; break;
                case GastoDeCierre : nombre = "Gasto de cierre"; break;
                case InteresDelGastoDeCierre: nombre = "Interes del gasto de cierre"; break;
                case OtrosCargos: nombre = "Otros  Cargos"; break;
                case InteresOtrosCargos: nombre = "Interes Otros cargos"; break;
                default: return string.Empty;
            }
            return nombre;
        }
        public override string ToString() => "Codigo para utilizar en los cargos";
        public static IEnumerable<string> CodigosCargosReservados()
        { 
            var codigosCargos = new List<string>()
            { 
                Capital, Interes, InteresDespuesDeVencido, Moras, GastoDeCierre, InteresDelGastoDeCierre, InteresOtrosCargos
            };

            return codigosCargos;
        }
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

    internal class DebitoPrestamoViewModel : ICxCDebitoPrestamo
    {
        //private MaestroDrConDetalles Debito { get; set; }

        private  DebitoPrestamoViewModel(MaestroDrConDetalles value)
        {
            this.Fecha = value.Fecha;
            this.NombreDocumento = CodigosTiposTransaccionCxC.GetNombreTipoDocumento(value.CodigoTipoTransaccion); 
             this.NumeroTransaccion = value.NumeroTransaccion;
            foreach (var item in value.GetDetallesCargos())
            {
                if (item.CodigoCargo ==  CodigosCargosDebitosReservados.Capital)
                {
                    this.Capital = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitosReservados.Interes)
                {
                    this.Interes = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitosReservados.GastoDeCierre)
                {
                    this.GastoDeCierre = item.Balance;
                    continue;
                }

                if (item.CodigoCargo == CodigosCargosDebitosReservados.InteresDelGastoDeCierre)
                {
                    this.InteresDelGastoDeCierre = item.Balance;
                    continue;
                }
                if (item.CodigoCargo == CodigosCargosDebitosReservados.Moras)
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

        public bool MenorOIgualALaFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public bool Vencida(DateTime fecha)
        {
            throw new NotImplementedException();
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
        public decimal InteresOtrosCargos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        

        public decimal TotalOrig => throw new NotImplementedException();
    }


    internal class NotaDeDebito : BaseMaestroCxC 
    {
        public override string CodigoTipoTransaccion => CodigosTiposTransaccionCxC.NotaDeDebito; 

        public string Concepto { get; set; }
    }

    internal class CargoPorAtraso : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosCargosDebitosReservados.Moras;
    }

    internal class CargoPorInteresDespuesDeVencido : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosCargosDebitosReservados.InteresDespuesDeVencido;
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
        public override string ToString() => $"{CodigosCargosDebitosReservados.GetNombreCargo(CodigoCargo)} Monto {Monto} Balance {Balance}";
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
            AddCargo(CodigosCargosDebitosReservados.Capital, capital);
            AddCargo(CodigosCargosDebitosReservados.Interes, interes);
            AddCargo(CodigosCargosDebitosReservados.GastoDeCierre, gastoDeCierre);
            AddCargo(CodigosCargosDebitosReservados.InteresDelGastoDeCierre, interesDelGastoDeCierre);
           
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
