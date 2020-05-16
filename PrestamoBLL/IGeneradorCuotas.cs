using PrestamoBLL.Entidades;
using System.Collections.Generic;

namespace PrestamoBLL
{
    internal interface IGeneradorCuotas
    {
        List<Cuota> GenerarCuotas();
    }
}