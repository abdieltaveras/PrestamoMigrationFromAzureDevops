using PrestamoBLL.Entidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public interface IGeneradorCuotas
    {
        List<Cuota> GenerarCuotas();
    }
}