using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Models
{
    public class CodigosTiposTransaccionCxC
    {
        public const string Cuota = "CT";
        public const string Pago = "PG";
        /// <summary>
        ///  se usara para generar moras, interes, todos los cargos que el sistema genera
        ///  de manera automatica
        /// </summary>
        public const string CargoInterno = "CI";
        public const string NotaDeDebito = "ND";
        public const string NotaDeCredito = "NC";

        public static string GetNombre(string codigoCargo)
        {
            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case "CT": nombre = "Cuota"; break;
                case "PG": nombre = "Pago"; break;
                case "CI": nombre = "Cargo Interno"; break;
                case "ND": nombre = "Nota de Debito"; break;
                case "NC": nombre = "Nota de Credito"; break;
                default: nombre = "el codigo indicado no esta registrado"; break;
            }
            return nombre;
        }
        public static IEnumerable<string> GetCodigos()
        {
            // no se incluye otros cargos porque ese reservados para agrupar los cargos
            // creados por el usuario
            var codigos = new List<string>()
            {
                Cuota, Pago, CargoInterno,NotaDeCredito, NotaDeDebito
            };
            return codigos;
        }
    }

    /// <summary>
    /// este objeto no se usara por ahora, dejaremos el que esta definido como constante
    /// </summary>
    internal class CodigosCargosDebitosV2 : Enumeration
    {
        public string Codigo { get; }
        public string Nombre => base.Name;

        public static CodigosCargosDebitosV2 Capital = new(1, nameof(Capital), "CA");
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

    /// <summary>
    /// Codigos para cargos Reservados
    /// </summary>
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
        public static string GetNombre(string codigoCargo)
        {

            string nombre = string.Empty;
            switch (codigoCargo)
            {
                case Capital: nombre = "Capital"; break;
                case Interes: nombre = "Interes"; break;
                case InteresDespuesDeVencido: nombre = "Interes despues de vencido"; break;
                case Moras: nombre = "Moras (Cargos por atraso)"; break;
                case GastoDeCierre: nombre = "Gasto de cierre"; break;
                case InteresDelGastoDeCierre: nombre = "Interes del gasto de cierre"; break;
                case OtrosCargos: nombre = "Otros  Cargos"; break;
                case InteresOtrosCargos: nombre = "Interes Otros cargos"; break;
                default: nombre = "el codigo indicado no esta registrado"; break;
            }
            return nombre;
        }
        public override string ToString() => "Codigos reservados para utilizar en los cargos";
        public static IEnumerable<string> GetCodigos()
        {
            // no se incluye otros cargos porque ese reservados para agrupar los cargos
            // creados por el usuario
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

        public virtual char TipoDrCr { get; protected set; }
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
        public MaestroDrConDetalles()
        {
            this.TipoDrCr = 'D';
        }

        public override string ToString() => $"No {NumeroTransaccion} Fecha {Fecha} Monto {Monto} Balance {Balance} {DetallesCargosText()}";

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

    internal class DebitoPrestamoConDetallesForBLL : DebitoPrestamoConDetallesViewModel, ICxCDebitoPrestamo
    {
        //private MaestroDrConDetalles Debito { get; set; }

        private DebitoPrestamoConDetallesForBLL(MaestroDrConDetalles value)
        {

            this.Fecha = value.Fecha;
            this.NombreDocumento = CodigosTiposTransaccionCxC.GetNombre(value.CodigoTipoTransaccion);
            this.NumeroTransaccion = value.NumeroTransaccion;

            foreach (var item in value.GetDetallesCargos())
            {
                switch (item.CodigoCargo)
                {
                    case CodigosCargosDebitosReservados.Capital:
                        this.Capital = item.Balance; break;
                    case CodigosCargosDebitosReservados.Interes:
                        this.Interes = item.Balance; break;
                    case CodigosCargosDebitosReservados.GastoDeCierre:
                        this.GastoDeCierre = item.Balance; break;
                    case CodigosCargosDebitosReservados.InteresDelGastoDeCierre:
                        this.InteresDelGastoDeCierre = item.Balance; break;
                    case CodigosCargosDebitosReservados.Moras:
                        this.Mora = item.Balance; break;
                    case CodigosCargosDebitosReservados.InteresDespuesDeVencido:
                        this.InteresDespuesDeVencido = item.Balance; break;
                    case CodigosCargosDebitosReservados.InteresOtrosCargos:
                        this.InteresOtrosCargos = item.Balance; break;
                    default:
                        this.OtrosCargos = item.Balance;
                        this.AddDetalleOtrosCargos(item);
                        break;
                }
            }
        }

        private void AddDetalleOtrosCargos(IDetalleDebitoCxC item)
        {
            this.DetallesOtrosCargos.Add(item);
        }

        internal static DebitoPrestamoConDetallesForBLL Create(MaestroDrConDetalles value)
        {
            return new DebitoPrestamoConDetallesForBLL(value);
        }
        public override string ToString() => $"Cuota numero {this.NumeroTransaccion} Total origina {Monto}  Balance ? ";
    }
    internal class NotaDeDebito : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosTiposTransaccionCxC.NotaDeDebito;


        public string Concepto { get; set; }

        public override char TipoDrCr => throw new NotImplementedException();
    }

    internal class CargoPorAtraso : BaseMaestroCxC
    {
        public override string CodigoTipoTransaccion => CodigosTiposTransaccionCxC.CargoInterno;
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

        public override string ToString() => $"{CodigosCargosDebitosReservados.GetNombre(CodigoCargo)} Monto {Monto} Balance {Balance}";
    }
    // este tipo es para devolver un parametro que contenga el maestros con sus detalles pero separados y poder accesar esas propiedades
    internal class DrMaestroDetalle
    {
        public IEnumerable<IMaestroDebitoConDetallesCxC> Maestros { get; private set; }

        public IEnumerable<IDetalleDebitoCxC> Detalles { get; private set; }

        public DrMaestroDetalle(IEnumerable<IMaestroDebitoConDetallesCxC> maestros, IEnumerable<IDetalleDebitoCxC> detalles)
        {
            this.Maestros = maestros;
            this.Detalles = detalles;
        }
    }

}
