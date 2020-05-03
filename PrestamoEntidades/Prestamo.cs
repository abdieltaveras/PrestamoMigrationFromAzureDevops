using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Prestamo : BaseInsUpd
    {
        public int IdPrestamo { get; set; }

        public string PrestamoNo { get; set; } = string.Empty;

        public int IdPrestamoARenovar { get; set; }

        public string NoPrestamoARenovar { get; set; }

        public int IdClasificacion { get; set; }

        public int IdAmortizacion { get; set; }

        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        public bool TieneGarantias { get { return IdGarantias.Count() > 0; }  }
        /// <summary>
        /// La lista de los clientes involucrados en la transaccion
        /// </summary>
        [IgnorarEnParam]
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        public List<int> IdClientes { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Garantia> Garantias { get; set; } = new List<Garantia>();

        public List<int> IdGarantias { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Codeudor> Codeudores { get; set; }

        public List<int> IdCodeudores { get; set; }


        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get;  }

        public int IdTasaDeInteres { get; set; }
        
        [IgnorarEnParam]
        public double TasaDeInteresPorPeriodo { get; set; }

        public int IdTipoMora { get; set; }

        public int IdPeriodo { get; set; }

        public int CantidadDePeriodo { get; set; }

        public Decimal DineroPrestado { get; set; }

        public int IdDivisa { get; set; }

        public Decimal CantidadDineroPrestado { get; set; }

        public bool LlevaGastoDeCiere => TasaGastoDeCierre > 0;
        public float TasaGastoDeCierre { get; set; }

        public float MontoGastoDeCierre { get; internal set; }

        public bool GastoDeCierreEsDeducible { get; set; }

        public bool SumarGastoDeCierreALasCuotas { get; set; }

        public bool CargarInteresAlGastoDeCierre { get; set; }
        
        public bool AcomodarFechaCuotas { get { return FechaInicioPrimeraCuota != InitValues._19000101; } }
        /// <summary>
        ///  si se acomoda el prestamo se debe indicar cual es la fecha en que desea que la primera cuota sea generada
        /// </summary>

        public DateTime? FechaInicioPrimeraCuota { get; set; } = InitValues._19000101;

        /// <summary>
        /// este campo es el que tendra la fecha real de donde partira a generar las cuotas y sus fechas de vencimientos, es necesario para cuando al prestamo se le acomode las cuotas
        /// </summary>
        public DateTime FechaInicioCalculoPrestamo { get; set; }
    }

    public class PrestamoConCuota
    {
        public int IdPrestamo { get; set; }

        public Prestamo Prestamo { get; set; }

        public List<Cuota> Cuotas { get; set; }
    }

}


