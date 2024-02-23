using System;

namespace PrestamoEntidades
{
    public interface ICxCCuota
    {
        decimal NumeroTransaccion { get; set; }
        DateTime Fecha { get; set; }
        decimal Capital { get; set; }
        decimal Interes { get; set; }
        decimal GastoDeCierre { get; set; }
        decimal InteresDelGastoDeCierre { get; set; }
        decimal OtrosCargos { get; set; }
        decimal InteresOtrosCargos { get; set; }
        string FechaSt { get; }
        decimal TotalOrig { get; }
        bool MenorOIgualALaFecha(DateTime fecha);
        string ToString();
        bool Vencida(DateTime fecha);
    }
}