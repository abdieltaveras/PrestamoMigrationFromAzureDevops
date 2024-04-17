using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public interface IGeneradorCuotas
    {
        public IEnumerable<IMaestroDebitoConDetallesCxC> GenerarCuotasMaestroYDetalle();
    }
}