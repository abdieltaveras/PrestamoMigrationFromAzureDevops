using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{

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

    public interface IMaestroDebitoConDetallesCxC
    {
        string DetallesCargosJson { get; set; }
        int IdTransaccion { get; set; }
        
        int IdPrestamo { get; set; }
        string CodigoTipoTransaccion { get; }
        Guid IdReferencia { get; }
        string NumeroTransaccion { get; }
        DateTime Fecha { get; }
        decimal Monto { get; }
        decimal Balance { get; }
        string OtrosDetallesJson { get; }

        char TipoDrCr { get; }
        IEnumerable<IDetalleDebitoCxC> GetDetallesCargos();
    }
}
