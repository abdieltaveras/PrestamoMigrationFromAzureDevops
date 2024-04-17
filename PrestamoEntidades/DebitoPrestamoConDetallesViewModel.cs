using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    //todo eliminar warw objeto y usar el DebitoViewModel
    // <summary>
    // la representacion de la cuota como va en la tabla
    // </summary>
    public class DebitoPrestamoTotalesViewModel
    {
        public string NombreDocumento { get; set; }
        public string NumeroTransaccion { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public virtual decimal Monto { get; set; }

        public virtual decimal Balance { get; set; }
    }

    public class DebitoPrestamoConDetallesViewModel : DebitoPrestamoTotalesViewModel
    {
        public decimal Capital { get; set; } = 0;
        public decimal Interes { get; set; } = 0;
        public decimal GastoDeCierre { get; set; } = 0;
        public decimal InteresDelGastoDeCierre { get; set; } = 0;
        public decimal InteresDespuesDeVencido { get; set; } = 0;
        public decimal Mora { get; set; }
        public decimal OtrosCargos { get; set; } = 0;
        public decimal InteresOtrosCargos { get; set; } = 0;
        public List<IDetalleDebitoCxC> DetallesOtrosCargos { get; set; }
        public bool Vencida(DateTime fecha) => this.Fecha.CompareTo(fecha) < 0;
        public bool MenorOIgualALaFecha(DateTime fecha) => this.Fecha.CompareTo(fecha) <= 0;
        //[IgnoreOnParams]
        //public string Comentario { get; set; } = String.Empty;
        public override string ToString() => DetallesCuotaText.ToString();
        private string DetallesCuotaText => $"Valores Originales Cuota No {NumeroTransaccion} Fecha {Fecha} Total {Monto} Capital {Capital} Interes {Interes} G/Credito {GastoDeCierre} Int G/Credito {InteresDelGastoDeCierre} Otros Cargos {OtrosCargos} Interes otros cargos {InteresOtrosCargos} ";
        
    }
    
}