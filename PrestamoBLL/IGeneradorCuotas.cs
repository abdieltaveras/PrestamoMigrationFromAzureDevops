using PrestamoEntidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public interface IGeneradorCuotas
    {
        List<CxCCuota> GenerarCuotas();
    }
}