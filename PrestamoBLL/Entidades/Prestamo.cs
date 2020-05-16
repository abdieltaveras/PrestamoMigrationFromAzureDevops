using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public enum TiposAmortizacion { Cuotas_fijas_No_amortizable=1, Abierto_amortizable_por_dia, Abierto_Amortizable_por_periodo_abierto, Cuotas_fijas_amortizable, Abierto_No_Amortizable }
    public class Prestamo : BaseInsUpd, IPrestamoForGeneradorCuotas
    {
        public int IdPrestamo { get; set; } 

        [IgnorarEnParam]
        public string PrestamoNumero { get; internal set; } = string.Empty;

        public int IdPrestamoARenovar { get; set; } 
        [IgnorarEnParam]
        /// attention analizar poner un objeto InfoPrestamoForView que permita poner todos los campos que uno pudiera necesitar como este NumeroPrestamoARenovar, etc
        public string NumeroPrestamoARenovar { get; internal set; }

        public int IdClasificacion { get; set; }

        public TiposAmortizacion TipoAmortizacion { get; set; } = TiposAmortizacion.Cuotas_fijas_No_amortizable;

        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        [IgnorarEnParam]
        public bool TieneGarantias { get { return IdGarantias.Count() > 0; }  }
        /// <summary>
        /// La lista de los clientes involucrados en la transaccion
        /// </summary>
        [IgnorarEnParam]
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        [IgnorarEnParam]
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        public List<int> IdClientes { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Garantia> Garantias { get; set; } = new List<Garantia>();
        [IgnorarEnParam]
        public List<int> IdGarantias { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Codeudor> Codeudores { get; set; }
        [IgnorarEnParam]
        public List<int> IdCodeudores { get; set; }

        public DateTime FechaEmisionReal { get; set; } = DateTime.Now;

        public DateTime FechaEmisionParaCalculos { get; internal set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; internal set; }
        public int IdTasaInteres { get; set; }
        [IgnorarEnParam]
        public decimal TasaDeInteresPorPeriodo { get; set; }

        public int IdTipoMora { get; set; }

        public int IdPeriodo { get; set; }
        [IgnorarEnParam]
        public Periodo Periodo { get; internal set; }

        public int CantidadDePeriodos { get; set; }

        public decimal MontoPrestado { get; set; }
        public decimal DeudaRenovacion { get; set; }

        [IgnorarEnParam]
        public decimal TotalPrestado { get; internal set; }

        public int IdDivisa { get; set; }
        [IgnorarEnParam]
        public bool LlevaGastoDeCierre => InteresGastoDeCierre > 0;
        public decimal InteresGastoDeCierre { get; set; }

        public decimal MontoGastoDeCierre { get; internal set; }

        public bool GastoDeCierreEsDeducible { get; set; }

        public bool SumarGastoDeCierreALasCuotas { get; set; }

        public bool CargarInteresAlGastoDeCierre { get; set; }
        
        public bool AcomodarFechaALasCuotas { get { return FechaInicioPrimeraCuota != InitValues._19000101; } }
        /// <summary>
        ///  si se acomoda el prestamo se debe indicar cual es la fecha en que desea que la primera cuota sea generada
        /// </summary>

        public DateTime FechaInicioPrimeraCuota { get; internal set; } = InitValues._19000101;

        /// <summary>
        /// este campo es el que tendra la fecha real de donde partira a generar las cuotas y sus fechas de vencimientos, es necesario para cuando al prestamo se le acomode las cuotas
        /// </summary>

        public Prestamo()
        {

        }

        public Prestamo(Periodo periodo)
        {
            var periodoDest = new Periodo();
            _type.CopyPropertiesTo(periodo, periodoDest);
            this.Periodo = periodoDest;
        }
    }

    public class PrestamoConCuotas
    {
        public Prestamo Prestamo { get; private set; }

        private List<Cuota> Cuotas { get; set; }

        public PrestamoConCuotas(Prestamo prestamo, List<Cuota> cuotas)
        {
            this.Prestamo = prestamo;
            this.Cuotas = cuotas;
        }

        public List<Cuota> GetCuotas() => this.Cuotas;

    }
}


