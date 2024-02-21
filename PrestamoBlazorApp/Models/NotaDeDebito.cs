using System;
using System.Collections.Generic;

namespace PrestamoBlazorApp.Models
{
    
    public class NotaDeDebito
    {
        public string NumeroTransaccion { get; set; }
        public int idTransaccion { get; set; } = -1;
        public string NoPrestamo { get; set; }
        public int idPrestamo { get; set; }
        public DateTime? Fecha { get; set; } = DateTime.Today;
        public decimal? Monto { get; set; }
        /// <summary>
        /// espacio para poner comentario  o detalles aclaratorio
        /// </summary>
        public string Detalle { get; set; }
        public List<DetalleCargo> DetallesCargos { get; set; }
    }

    public class DetalleCargo
    {
        string CodigoCargo { get; set; }
        decimal Monto { get; set; }
    }

    public class CodigoCargos
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public NotaDeDebito NotaDebito { get; set;}
        
    }
    public class ListadoCodigosCargos
    {
        public static IEnumerable<CodigoCargos> Get() 
        {
            var codigos = new List<CodigoCargos>();
            codigos.Add(new CodigoCargos { Codigo = "INT", Nombre = "Interes" });
            codigos.Add(new CodigoCargos { Codigo="InAlg", Nombre="Intimacion de alguacil" });
            codigos.Add(new CodigoCargos { Codigo = "GCob", Nombre = "Gestion de Cobros localizador" });
            codigos.Add(new CodigoCargos { Codigo = "LLam", Nombre = "Llamadas telefonicas" });
            codigos.Add(new CodigoCargos { Codigo = "AcVta", Nombre = "Acto de venta" });
            return codigos;
        }
    }
}
