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
        int IdTransaccion { get; set; }
        char TipoDrCr { get; }
        int IdPrestamo { get; set; }
        string CodigoTipoTransaccion { get; }
        string NumeroTransaccion { get; }
        Guid IdReferencia { get; }
        DateTime Fecha { get; }
        decimal Monto { get; }
        decimal Balance { get; }
        string OtrosDetallesJson { get; }
        public IEnumerable<IDetalleDebitoCxC> GetDetallesCargos();
    }
}
